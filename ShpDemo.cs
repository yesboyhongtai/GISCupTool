using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpMap.Layers;
//using SharpMap.Data;
using SharpMap.Styles;
using SharpMap.Rendering.Thematics;
using SharpMap.Data.Providers;
using SharpMap.Forms;
using SharpMap.Geometries;
using ProjNet.CoordinateSystems.Transformations;
using System.IO;
using PolygonDemo.Model;
using Point = SharpMap.Geometries.Point;
using Polygon = SharpMap.Geometries.Polygon;
using LineString = SharpMap.Geometries.LineString;
using GeoAPI.Geometries;
using GeoAPI.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;

namespace PolygonDemo
{
    public partial class ShpDemo: Form
    {
        #region variables
        private List<Geometry> axis = new List<Geometry>();
        private List<Geometry> lines = new List<Geometry>();
        private List<Geometry> simplines = new List<Geometry>();
        private List<Geometry> points = new List<Geometry>();

        private List<Geometry> choose_points = new List<Geometry>();
        private List<Geometry> choose_lines = new List<Geometry>();
        private List<Geometry> choose_simplines = new List<Geometry>();

        private List<Geometry> marker = new List<Geometry>();
        private List<Geometry> greenpoint = new List<Geometry>();
        private List<Geometry> endpoint = new List<Geometry>();

        private bool ruler = false;
        private int max_number_scaler = 1000000;

        private PointDetails pd;
        private LineDetails ld;
        private SimplifiedLineDetails sld;
        #endregion variables
        public ShpDemo()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void ShpDemo_Load(object sender, EventArgs e)
        {
            //Adds the output layer
            initLayers();
            initAxis();
            //BoundingBox box = GeometryTransform.TransformBox(new BoundingBox(116.125, 39.738, 116.625, 40.0947), LayerTools.Wgs84toGoogleMercator.MathTransform);
            //BoundingBox box = new BoundingBox(116.125, 39.738, 116.625, 40.0947);
            fitWindow(-10,-10,10,10);
            //MessageBox.Show(this.mbMap.Map.Envelope.ToString());
        }
        /// <summary>
        /// 初始化图层：axis为坐标轴图层，polygons为多边形图层，points为点图层,choose_points为被选中的点；
        ///             图层间相互独立
        /// </summary>
        private void initLayers()
        {
            //坐标轴
            VectorLayer axisLayer = new VectorLayer("Axis");
            //axisLayer.Style.Symbol = PolygonDemo.Properties.Resources.g_arrow;
            axisLayer.Style.Line.Color = Color.FromArgb(255, Color.Black);
            axisLayer.Style.Line.Width = 0.5F;
            //Color tRed = Color.FromArgb(128, Color.Red);
            //outputLayer.Style.Fill = new SolidBrush(tRed);
            axisLayer.DataSource = new GeometryProvider(axis);
            axisLayer.Style.Line.DashStyle = DashStyle.DashDot;
            //axisLayer.Style.Line.DashStyle = DashStyle.Dash;

            mbMap.Map.Layers.Add(axisLayer);

            ////lines
            VectorLayer polygonLayer = new VectorLayer("Lines");
            //polygonLayer.Style.Symbol = PolygonDemo.Properties.Resources.g_arrow;
            polygonLayer.Style.Line.Color = Color.FromArgb(200, Color.LightGreen);
            polygonLayer.Style.Line.Width = 3.0F;
            Color tRed = Color.FromArgb(100, Color.Green);
            polygonLayer.Style.Fill = new SolidBrush(tRed);
            polygonLayer.DataSource = new GeometryProvider(lines);

            mbMap.Map.VariableLayers.Add(polygonLayer);

            //Points
            VectorLayer pointLayer = new VectorLayer("Points");
            //pointLayer.Style.Symbol = PolygonDemo.Properties.Resources.g_arrow;
            pointLayer.Style.Line.Color = Color.FromArgb(100, Color.LightGreen);
            pointLayer.Style.Line.Width = 1.0F;
            pointLayer.Style.PointSize = 4;
            pointLayer.Style.PointColor = new SolidBrush(Color.Navy);
            //Color tRed = Color.FromArgb(128, Color.Red);
            //outputLayer.Style.Fill = new SolidBrush(tRed);
            pointLayer.DataSource = new GeometryProvider(points);

            mbMap.Map.VariableLayers.Add(pointLayer);
            
            //Choose Points
            VectorLayer ChoosedPointLayer = new VectorLayer("ChoosedPoint");
            ChoosedPointLayer.Style.Symbol = PolygonDemo.Properties.Resources.PumpSmall;
            ChoosedPointLayer.Style.PointSize = 8;
            ChoosedPointLayer.Style.PointColor = new SolidBrush(Color.Red);
            ChoosedPointLayer.DataSource = new GeometryProvider(choose_points);

            mbMap.Map.VariableLayers.Add(ChoosedPointLayer);

            //Choose Lines
            VectorLayer ChoosedPolygonLayer = new VectorLayer("ChoosedLines");
            ChoosedPolygonLayer.Style.Line.Color = Color.FromArgb(200, Color.Black);
            ChoosedPolygonLayer.Style.Line.Width = 5.0F;
            ChoosedPolygonLayer.Style.Fill = new SolidBrush(Color.FromArgb(100, Color.Black));
            ChoosedPolygonLayer.DataSource = new GeometryProvider(choose_lines);

            mbMap.Map.VariableLayers.Add(ChoosedPolygonLayer);

            //Choose Simplified Lines
            VectorLayer ChoosedSimplifiedLineLayer = new VectorLayer("ChoosedSimplifiedLines");
            ChoosedSimplifiedLineLayer.Style.Line.Color = Color.FromArgb(200, Color.Black);
            ChoosedSimplifiedLineLayer.Style.Line.Width = 5.0F;
            ChoosedSimplifiedLineLayer.Style.Fill = new SolidBrush(Color.FromArgb(100, Color.Black));
            ChoosedSimplifiedLineLayer.DataSource = new GeometryProvider(choose_simplines);

            mbMap.Map.VariableLayers.Add(ChoosedSimplifiedLineLayer);

            //Simplified Lines
            VectorLayer SimplifiedLineLayer = new VectorLayer("SimplifiedLines");
            SimplifiedLineLayer.Style.Line.Color = Color.FromArgb(200, Color.Brown);
            SimplifiedLineLayer.Style.Line.Width = 1.0F;
            SimplifiedLineLayer.Style.Fill = new SolidBrush(Color.FromArgb(100, Color.Red));
            SimplifiedLineLayer.DataSource = new GeometryProvider(simplines);

            mbMap.Map.VariableLayers.Add(SimplifiedLineLayer);

            //Marker Layer
            VectorLayer markerLayer = new VectorLayer("Fixed Marker");
            markerLayer.Style.Symbol = PolygonDemo.Properties.Resources.OutfallSmall;
            markerLayer.DataSource = new GeometryProvider(marker);
            Color cRed = Color.FromArgb(50, Color.Red);
            markerLayer.Style.Fill = new SolidBrush(cRed);

            mbMap.Map.VariableLayers.Add(markerLayer);

            //Green point for start 
            VectorLayer greenLayer = new VectorLayer("GreenPoint");
            greenLayer.Style.Symbol = PolygonDemo.Properties.Resources.g_arrow;
            greenLayer.DataSource = new GeometryProvider(greenpoint);
            Color cGreen = Color.FromArgb(50, Color.Green);
            greenLayer.Style.Fill = new SolidBrush(cGreen);

            mbMap.Map.VariableLayers.Add(greenLayer);

            //End point for every line
            VectorLayer endPointLayer = new VectorLayer("EndPoint");
            endPointLayer.Style.Symbol = PolygonDemo.Properties.Resources.r_arrow;
            endPointLayer.DataSource = new GeometryProvider(endpoint);
            Color cRedEnd = Color.FromArgb(50, Color.Red);
            endPointLayer.Style.Fill = new SolidBrush(cRedEnd);

            mbMap.Map.VariableLayers.Add(endPointLayer);

        }
        /// <summary>
        /// 初始化坐标轴
        /// </summary>
        private void initAxis()
        {
            int max = 1000;
            LineString xAxis = new LineString();
            xAxis.Vertices.Add(new Point(-max, 0));
            xAxis.Vertices.Add(new Point(max, 0));
            axis.Add(xAxis);
            LineString yAxis = new LineString();
            yAxis.Vertices.Add(new Point(0, -max));
            yAxis.Vertices.Add(new Point(0, max));
            axis.Add(yAxis);

        }

        /// <summary>
        /// 缩放的合适大小
        /// </summary>
        private void fitWindow(double minX, double minY, double maxX, double maxY)
        {
            BoundingBox box = new BoundingBox(minX, minY, maxX, maxY);
            this.mbMap.Map.ZoomToBox(box);
            Point p = new Point((minX + maxX) / 2, (minY + maxY) / 2);
            this.mbMap.Map.Center = p;
            //this.mbMap.Map.ZoomToExtents(); //(geom);
            this.mbMap.Refresh();
        }

        //public void focusLine(LineString line)
        //{
        //    BoundingBox box = mbMap.Map.Envelope;
        //    foreach (MotionVector mv in line)
        //    {
        //        Point p = (Point)GeometryTransform.TransformGeometry(new Point(mv.lng, mv.lat),
        //            LayerTools.Wgs84toGoogleMercator.MathTransform, geofactory);
        //        box = box.Merge(p);
        //    }
        //    this.mbMap.Map.ZoomToBox(box);
        //    this.mbMap.Refresh();
        //}

        /// <summary>
        ///     Support call from PointDetails
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void fitWindowFromPoint(double x, double y)
        {
            x /= max_number_scaler;
            y /= max_number_scaler;
            double offset = 0.1;
            BoundingBox box = new BoundingBox(x - offset, y - offset, x + offset, y + offset);
            this.mbMap.Map.ZoomToBox(box);
            Point p = new Point(x, y);
            this.mbMap.Map.Center = p;
            this.choose_points.Clear();
            this.choose_points.Add(p);
            this.mbMap.Refresh();
        }

        /// <summary>
        ///     Support call from PolygonDetails
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void fitWindowFromLine(int index)
        {
            LineString line = (LineString)this.lines[index];
            BoundingBox box = mbMap.Map.Envelope;
            this.marker.Clear();
            this.greenpoint.Clear();
            int i = 0;
            foreach (Point p in line.Vertices.ToList())
            {
                if (0 == i)
                {
                    this.greenpoint.Add(p);
                }
                box = box.Merge(p);
                this.marker.Add(p);
                i++;
            }
            this.mbMap.Map.ZoomToBox(box);

            this.choose_lines.Clear();
            this.choose_lines.Add(line);
            this.mbMap.Refresh();
        }
        public void fitWindowFromSimplifiedLine(int index)
        {
            LineString line = (LineString)this.simplines[index];
            BoundingBox box = mbMap.Map.Envelope;
            foreach (Point p in line.Vertices.ToList())
            {
                box = box.Merge(p);
            }
            this.mbMap.Map.ZoomToBox(box);

            this.choose_simplines.Clear();
            this.choose_simplines.Add(line);
            this.mbMap.Refresh();
        }

        private void btnBuildShp_Click(object sender, EventArgs e)
        {
        }

        private void btnPan_Click(object sender, EventArgs e)
        {
            mbMap.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        private void btnZoomWin_Click(object sender, EventArgs e)
        {
            mbMap.ActiveTool = MapBox.Tools.ZoomWindow;
        }

        private void ShpDemo_SizeChanged(object sender, EventArgs e)
        {
            mbMap.Refresh();
        }

        /// <summary>
        /// 从文件中读取轨迹数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadTrj_Click(object sender, EventArgs e)
        {

        }
        private List<LineString> build_route()
        {

            return null;
        }
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            this.mbMap.ActiveTool = MapBox.Tools.ZoomIn;
        }

        private void btnRuler_Click(object sender, EventArgs e)
        {
            mbMap.ActiveTool = MapBox.Tools.DrawLine;
            ruler = true;
        }

        private void mbMap_GeometryDefined(Geometry geometry)
        {
            double length = 0;
            if (geometry.GetType() == typeof(LineString) && ruler)
            {
                //geometry = GeometryTransform.TransformGeometry(geometry, LayerTools.GoogleMercatorToWgs84.MathTransform);
                length = LayerTools.GetLength(geometry as LineString);
                MessageBox.Show("Length:" + length + "m.");
                ruler = false;
            }
            else
            {
                axis.Add(geometry);
            }
            mbMap.ActiveTool = MapBox.Tools.Pan;
            //VariableLayerCollection.TouchTimer();
            //this.mbMap.Refresh();
            mbMap.Refresh();
        }

        //private void btnAddPolygon_Click(object sender, EventArgs e)
        //{
        //    //Polygon polygon = new Polygon();
        //    LineString line = new LineString();
        //    int number = 6;
        //    int max = 10;
        //    int b = 5;
        //    //Point start = new Point(0, 0);
        //    //polygon.ExteriorRing.Vertices.Add(start);
        //    Random rand = new Random();
        //    for (int i = 0; i < number; i++)
        //    {
        //        Point p = new Point(rand.Next() % max - b, rand.Next() % max - b);
        //        polygon.ExteriorRing.Vertices.Add(p);
        //    }
        //    //polygon.ExteriorRing.Vertices.Add(start);
        //    polygons.Add(polygon);
        //    mbMap.Refresh();
        //}

        private void mbMap_MouseMove(Point worldPos, MouseEventArgs imagePos)
        {
            lbX.Text = (worldPos.X * max_number_scaler).ToString("0.###");
            lbY.Text = (worldPos.Y * max_number_scaler).ToString("0.###");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lines.Clear();
            points.Clear();
            choose_lines.Clear();
            choose_points.Clear();
            simplines.Clear();
            marker.Clear();
            greenpoint.Clear();
            endpoint.Clear();
            pd.Close();
            ld.Close();
            sld.Close();
            mbMap.Refresh();
        }

        private void btn_loadPoints_Click(object sender, EventArgs e)
        {
            String fileName = getFileName();
            if (fileName == null)
            {
                MessageBox.Show(fileName + " File name cannot be empty!");
                return;
            }
            LoadOperation lo = new LoadOperation(fileName);
            List<PointModel> pointList = lo.loadPointsFromFile();
            showPoints(pointList);

            this.pd = new PointDetails(this); 
            pd.Show(this);
            pd.setDateSource(pointList);
        }

        private String getFileName(){
            OpenFileDialog f2 = new OpenFileDialog();
            if (f2.ShowDialog() == DialogResult.OK)
            {
                String fileName = f2.FileName;
                //MessageBox.Show(fileName);
                return fileName;
            }
            return null;
        }

        private void showPoints(List<PointModel> pointList)
        {
            if(null == pointList){
                MessageBox.Show("No points in the file!");
            }

            double minX = Double.MaxValue;
            double minY = Double.MaxValue;
            double maxX = Double.MinValue;
            double maxY = Double.MinValue;

            foreach (PointModel pm in pointList)
            {
                double x = pm.x / max_number_scaler;
                double y = pm.y / max_number_scaler;
                Point p = new Point(x, y);
                points.Add(p);

                if (x < minX) minX = x;
                if (x > maxX) maxX = x;
                if (y < minY) minY = y;
                if (y > maxY) maxY = y;
            }

            fitWindow(minX, minY, maxX, maxY);
            mbMap.Refresh();
        }

        private void btn_loadLines_Click(object sender, EventArgs e)
        {
            String fileName = getFileName();
            if (fileName == null)
            {
                MessageBox.Show(fileName + " File name cannot be empty!");
                return;
            }
            LoadOperation lo = new LoadOperation(fileName);
            List<LineModel> listOfLine = lo.loadLinesFromFile();
            showLines(listOfLine);

            this.ld = new LineDetails(this);
            ld.Show(this);
            ld.setDateSource(listOfLine);
        }

        private void showLines(List<LineModel> lineList)
        {
            if (null == lineList)
            {
                MessageBox.Show("No polygons in the file!");
            }
            double minX = Double.MaxValue;
            double minY = Double.MaxValue;
            double maxX = Double.MinValue;
            double maxY = Double.MinValue;

            foreach (LineModel lm in lineList)
            {
                LineString line = new LineString();
                foreach (PointModel p in lm.lineGeometries)
                {
                    double x = p.x / max_number_scaler;
                    double y = p.y / max_number_scaler;

                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                    Point point = new Point(x, y);
                    line.Vertices.Add(point);
                }

                this.lines.Add(line);
                this.endpoint.Add(line.Vertices.First());
                this.endpoint.Add(line.Vertices.Last());
            }
            fitWindow(minX, minY, maxX, maxY);
            mbMap.Refresh();
        }

        private void showSimpLines(List<LineModel> lineList)
        {
            if (null == lineList)
            {
                MessageBox.Show("No polygons in the file!");
            }
            double minX = Double.MaxValue;
            double minY = Double.MaxValue;
            double maxX = Double.MinValue;
            double maxY = Double.MinValue;

            foreach (LineModel lm in lineList)
            {
                LineString line = new LineString();
                foreach (PointModel p in lm.lineGeometries)
                {
                    double x = p.x / max_number_scaler;
                    double y = p.y / max_number_scaler;

                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                    Point point = new Point(x, y);
                    line.Vertices.Add(point);
                }

                this.simplines.Add(line);
            }
            fitWindow(minX, minY, maxX, maxY);
            mbMap.Refresh();
        }

        private void btn_loadSimpLines_Click(object sender, EventArgs e)
        {
            String fileName = getFileName();
            if (fileName == null)
            {
                MessageBox.Show(fileName + " File name cannot be empty! ");
                return;
            }
            LoadOperation lo = new LoadOperation(fileName);
            List<LineModel> listOfLine = lo.loadLinesFromFile();
            showSimpLines(listOfLine);

            this.sld = new SimplifiedLineDetails(this);
            sld.Show(this);
            sld.setDateSource(listOfLine);
        }

        private void btn_clearsimplines_Click(object sender, EventArgs e)
        {
            simplines.Clear();
            mbMap.Refresh();
            sld.Close();
        }

        private void btn_clrChoosed_Click(object sender, EventArgs e)
        {
            choose_lines.Clear();
            choose_points.Clear();
            choose_simplines.Clear();
            marker.Clear();
            greenpoint.Clear();
            mbMap.Refresh();
        }

    }
}
