using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.Layers;
using System.Drawing;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;
using SharpMap.Geometries;
using Point = SharpMap.Geometries.Point;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using BruTile.Cache;
using BruTile;
using System.Windows.Forms;
namespace PolygonDemo
{
    static class LayerTools
    {
        private static readonly string MAP_CACHE_NAME = "..\\..\\MapCache.dat";
        private static ICoordinateTransformation wgs84toGoogle;
        private static ICoordinateTransformation googletowgs84;

        /// <summary>
        /// Wgs84 to Google Mercator Coordinate Transformation
        /// </summary>
        public static ICoordinateTransformation Wgs84toGoogleMercator
        {
            get
            {
                if (wgs84toGoogle == null)
                {
                    CoordinateSystemFactory csFac = new ProjNet.CoordinateSystems.CoordinateSystemFactory();
                    CoordinateTransformationFactory ctFac = new CoordinateTransformationFactory();

                    IGeographicCoordinateSystem wgs84 = csFac.CreateGeographicCoordinateSystem(
                      "WGS 84", AngularUnit.Degrees, HorizontalDatum.WGS84, PrimeMeridian.Greenwich,
                      new AxisInfo("north", AxisOrientationEnum.North), new AxisInfo("east", AxisOrientationEnum.East));

                    List<ProjectionParameter> parameters = new List<ProjectionParameter>();
                    parameters.Add(new ProjectionParameter("semi_major", 6378137.0));
                    parameters.Add(new ProjectionParameter("semi_minor", 6378137.0));
                    parameters.Add(new ProjectionParameter("latitude_of_origin", 0.0));
                    parameters.Add(new ProjectionParameter("central_meridian", 0.0));
                    parameters.Add(new ProjectionParameter("scale_factor", 1.0));
                    parameters.Add(new ProjectionParameter("false_easting", 0.0));
                    parameters.Add(new ProjectionParameter("false_northing", 0.0));
                    IProjection projection = csFac.CreateProjection("Google Mercator", "mercator_1sp", parameters);

                    IProjectedCoordinateSystem epsg900913 = csFac.CreateProjectedCoordinateSystem(
                      "Google Mercator", wgs84, projection, LinearUnit.Metre, new AxisInfo("East", AxisOrientationEnum.East),
                      new AxisInfo("North", AxisOrientationEnum.North));

                    wgs84toGoogle = ctFac.CreateFromCoordinateSystems(wgs84, epsg900913);
                }
                return wgs84toGoogle;
            }
        }


        public static ICoordinateTransformation GoogleMercatorToWgs84
        {
            get
            {
                if (googletowgs84 == null)
                {
                    CoordinateSystemFactory csFac = new ProjNet.CoordinateSystems.CoordinateSystemFactory();
                    CoordinateTransformationFactory ctFac = new CoordinateTransformationFactory();
                    IGeographicCoordinateSystem wgs84 = csFac.CreateGeographicCoordinateSystem(
                      "WGS 84", AngularUnit.Degrees, HorizontalDatum.WGS84, PrimeMeridian.Greenwich,
                      new AxisInfo("north", AxisOrientationEnum.North), new AxisInfo("east", AxisOrientationEnum.East));

                    List<ProjectionParameter> parameters = new List<ProjectionParameter>();
                    parameters.Add(new ProjectionParameter("semi_major", 6378137.0));
                    parameters.Add(new ProjectionParameter("semi_minor", 6378137.0));
                    parameters.Add(new ProjectionParameter("latitude_of_origin", 0.0));
                    parameters.Add(new ProjectionParameter("central_meridian", 0.0));
                    parameters.Add(new ProjectionParameter("scale_factor", 1.0));
                    parameters.Add(new ProjectionParameter("false_easting", 0.0));
                    parameters.Add(new ProjectionParameter("false_northing", 0.0));
                    IProjection projection = csFac.CreateProjection("Google Mercator", "mercator_1sp", parameters);

                    IProjectedCoordinateSystem epsg900913 = csFac.CreateProjectedCoordinateSystem(
                      "Google Mercator", wgs84, projection, LinearUnit.Metre, new AxisInfo("East", AxisOrientationEnum.East),
                      new AxisInfo("North", AxisOrientationEnum.North));

                    googletowgs84 = ctFac.CreateFromCoordinateSystems(epsg900913, wgs84);
                }

                return googletowgs84;

            }
        }

        public static LabelLayer CreateLabelLayer(VectorLayer originalLayer, string labelColumnName)
        {
            SharpMap.Layers.LabelLayer labelLayer = new SharpMap.Layers.LabelLayer(originalLayer.LayerName + ":Labels");
            labelLayer.DataSource = originalLayer.DataSource;
            labelLayer.LabelColumn = labelColumnName;
            labelLayer.Style.CollisionDetection = true;
            labelLayer.Style.CollisionBuffer = new SizeF(10F, 10F);
            labelLayer.LabelFilter = SharpMap.Rendering.LabelCollisionDetection.ThoroughCollisionDetection;
            labelLayer.Style.Offset = new PointF(0, -5F);
            labelLayer.MultipartGeometryBehaviour = SharpMap.Layers.LabelLayer.MultipartGeometryBehaviourEnum.CommonCenter;
            labelLayer.Style.Font = new Font(FontFamily.GenericSansSerif, 12);
            labelLayer.MaxVisible = originalLayer.MaxVisible;
            labelLayer.MinVisible = originalLayer.MinVisible;
            labelLayer.Style.Halo = new Pen(Color.White, 2);
            labelLayer.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            labelLayer.CoordinateTransformation = originalLayer.CoordinateTransformation;
            return labelLayer;
        }

        public static Point WgsToGoogle(Point p)
        {
            Point result = GeometryTransform.TransformGeometry(p, LayerTools.wgs84toGoogle.MathTransform) as Point;
            return result;
        }
        public static Point GoogleToWgs(Point p)
        {
            Point result = GeometryTransform.TransformGeometry(p, LayerTools.GoogleMercatorToWgs84.MathTransform) as Point;
            return result;
        }
        private static double Rad(double d)
        {
            return d * Math.PI / 180.0;
        }
        /// <summary>
        /// Get the distance between two points marked by (lat,lon)
        /// </summary>
        /// <param name="latX"></param>
        /// <param name="lonX"></param>
        /// <param name="latY"></param>
        /// <param name="lonY"></param>
        /// <returns></returns>
        public static double GetDistance(double lonA, double latA, double lonB, double latB)
        {
            //double radLatA = Rad(latA);
            //double radLatB = Rad(latB);
            //double a = radLatA - radLatB;
            //double b = Rad(lonA) - Rad(lonB);
            //double s = 2 * Math.Asin(Math.Sqrt(
            //    Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLatA) * Math.Cos(radLatB) * Math.Pow(Math.Sin(b / 2), 2)));
            //s = s * 6378137.0;
            //s = Math.Round(s * 10000) / 10000;
            return Math.Sqrt((lonA - lonB) * (lonA - lonB) + (latA - latB) * (latA - latB));
        }
        public static double GetDistance(Point p1, Point p2)
        {
            return GetDistance(p1.X, p1.Y, p2.X, p2.Y);
        }
        public static double GetLength(LineString line)
        {
            double length = 0;
            for (int i = 1; i < line.Vertices.Count; i++)
            {
                length += GetDistance(line.Vertices[i - 1].X, line.Vertices[i - 1].Y, line.Vertices[i].X, line.Vertices[i].Y);
            }
            return length;
        }
        public static void SaveMapCache(MemoryCache<Bitmap> bitMaps)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = null;
            try
            {
                string tempFileName = "..\\..\\tempMapFile.dat";
                fs = new FileStream(tempFileName,FileMode.OpenOrCreate);
                //Dictionary<TileIndex, Bitmap> dict = bitMaps.GetBitMaps();
                Dictionary<TileIndex, Bitmap> dict = new Dictionary<TileIndex, Bitmap>(bitMaps.GetBitMaps());
                StreamHelper sh = new StreamHelper(fs);
                sh.WriteInt(dict.Count);
                foreach (var item in dict)
                {
                    sh.WriteInt(item.Key.Col);
                    sh.WriteInt(item.Key.Row);
                    sh.WriteString(item.Key.LevelId);
                    bf.Serialize(fs, item.Value);
                }
                fs.Close();
                fs = null;
                //Rename the tempMapFile
                FileInfo fileInfo = new FileInfo(MAP_CACHE_NAME);
                fileInfo.Delete();
                fileInfo = new FileInfo(tempFileName);
                fileInfo.MoveTo(MAP_CACHE_NAME);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        public static Dictionary<TileIndex, Bitmap> GetMapCache()
        {
            BinaryFormatter bf = new BinaryFormatter();
            Dictionary<TileIndex, Bitmap> dict = new Dictionary<TileIndex, Bitmap>();
            FileStream fs = null;
            try
            {
                fs = new FileStream(MAP_CACHE_NAME, FileMode.Open);
                StreamHelper sh = new StreamHelper(fs);
                int count = sh.ReadInt();
                int col, row;
                string levelId;
                Bitmap bitmap = null;
                for (int i = 0; i < count; i++)
                {
                    col = sh.ReadInt();
                    row = sh.ReadInt();
                    levelId = sh.ReadString();
                    bitmap = (Bitmap)bf.Deserialize(fs);
                    dict.Add(new TileIndex(col, row, levelId), bitmap);
                }
            }
            catch (System.Exception ex)
            {
                dict = new Dictionary<TileIndex, Bitmap>();
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return dict;
        }

        public static Point BeijingToWgs(Point p)
        {
            return BeijingToWgs(p.X, p.Y);
        }
        public static Point BeijingToWgs(double xx,double yy)
        {
            int base_x = 1161250000;
	        int base_y = 397500000;
            int base_m=10000000;
            double x, y;
            x = (xx + base_x) / base_m;
            y = (yy + base_y) / base_m;
            return new Point(x, y);
        }
    }
}
