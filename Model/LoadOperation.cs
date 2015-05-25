using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace PolygonDemo.Model
{
    class LoadOperation
    {
        public String path;
        public LoadOperation(String path) {
            this.path = path;
        }

        //public List<Point> loadPointsFromXML(){
        //    List<Point> result = new List<Point>();

        //    XmlDocument xmlDoc = new XmlDocument();
        //    XmlReaderSettings settings = new XmlReaderSettings();
        //    settings.IgnoreComments = true;//忽略文档里面的注释
        //    XmlReader reader = XmlReader.Create(this.path, settings); 
        //    xmlDoc.Load(reader);

        //    //XmlNode root = xmlDoc.SelectSingleNode();

        //    reader.Close();
        //    return result;
        //}

        public List<PointModel> loadPointsFromFile() 
        {
            List<PointModel> res = new List<PointModel>();
            String fileName = this.path;
            
            //test
            //fileName = "D:\\GISCup2\\TrainingDataSet\\TrainingDataSet\\points500.txt";
            if (String.IsNullOrEmpty(fileName))
                return null;

            StreamReader sr = new StreamReader(fileName);
            
            int count = 0;
            while (!sr.EndOfStream)
            {
                count++;

                String line = sr.ReadLine();
                //Console.WriteLine("报告帅气的亚光,第{0}行是：{1}", count, line);

                PointModel point = new PointModel();
                //String pre_line = line.Substring(6, line.IndexOf("<gml:Point")-6-1);//-1去掉最后的：
                String post_line = line.Substring(line.IndexOf("<gml:Point"));
                post_line = post_line.Substring(post_line.IndexOf("ts=\" \">") + 7);
                post_line = post_line.Substring(0, post_line.IndexOf(" "));

                point.id = int.Parse(line.Substring(0, line.IndexOf(":")));
                //point.time = int.Parse(pre_line.Substring(pre_line.IndexOf(":") + 1));

                point.x = double.Parse(post_line.Substring(0,post_line.IndexOf(",")));
                point.y = double.Parse(post_line.Substring(post_line.IndexOf(",") + 1));

                res.Add(point);
            }
            Console.WriteLine("报告帅气的亚光,在point文件中一共有{0}行", count);
            sr.Close();
            return res;
        }

        public List<LineModel> loadLinesFromFile()
        {
            List<LineModel> res = new List<LineModel>();
            String fileName = this.path;

            StreamReader sr = new StreamReader(fileName);

            int count = 0;

            while (!sr.EndOfStream)
            {
                count++;
                String input = sr.ReadLine();
                
                LineModel line = new LineModel();
                //String pattern = @"POLYGON:(?<id>\d+):(?<time>\d+):<gml:Polygon[^>]+><gml:outerBoundaryIs><gml:LinearRing><gml:coordinates[^>]+>" +
                //    @"(?<outerBoundary>[^<]+)</gml:coordinates></gml:LinearRing></gml:outerBoundaryIs>"+
                //    @"(<gml:innerBoundaryIs><gml:LinearRing><gml:coordinates[^>]+>(?<innerBoundary>[^<]+)</gml:coordinates></gml:LinearRing></gml:innerBoundaryIs>)?</gml:Polygon>";
                //String pattern = @"(?<id>\d+):<gml:LineString[^>]+>" +
                //    @"(?<lineGeometries>[^<]+)</gml:coordinates></gml:LineString>";
                String pattern = @"(?<id>\d+):[^>]*>[^>]*>(?<lineGeometries>[ \d\-\.,]*)";
                Regex regex = new Regex(pattern);
                MatchCollection matches = regex.Matches(input);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        // for test
                        foreach (String k in regex.GetGroupNames())
                        {
                           // Console.WriteLine("{0}\t{1}", k, match.Groups[k].Value);
                        }
                        line.id = int.Parse(match.Groups["id"].Value);
                        line.lineGeometries = str2ListOfPointModel(match.Groups["lineGeometries"].Value);
                        line.num = line.lineGeometries.Count;
                        //polygon.innterBoundary = str2ListOfPointModel(match.Groups["innerBoundary"].Value);
                    }
                }
                res.Add(line);
                
            }
            Console.WriteLine("在line文件中一共有{0}行", count);
            sr.Close();
            return res;
        }

        private List<PointModel> str2ListOfPointModel(String lines) {
            List<PointModel> res = new List<PointModel>();
            String[] line = Regex.Split(lines, " ", RegexOptions.IgnoreCase);
            foreach(String str in line){
                if (String.IsNullOrEmpty(str))
                {
                    continue;
                }
                PointModel point = new PointModel();
                point.x = Double.Parse(str.Substring(0, str.IndexOf(",")));
                point.y = Double.Parse(str.Substring(str.IndexOf(",") + 1));
                res.Add(point);
            }
            return res;
        }

    }
}
