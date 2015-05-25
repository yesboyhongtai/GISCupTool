using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PolygonDemo.Model;

namespace PolygonDemo.Model
{
    public class LineModel
    {
        public LineModel()
        {}

        /// <summary>
        /// the id of polygon
        /// </summary>
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
 
        ///// <summary>
        ///// the timestamp of polygon
        ///// </summary>
        //private int _time;

        //public int time
        //{
        //    get { return _time; }
        //    set { _time = value; }
        //}

        private int _num;
        public int num
        {
            get { return _num; }
            set { _num = value; }
        }

        public List<PointModel> lineGeometries;

        public String LineGeometries
        {
            get { return listToString(); }
            //set { _outerBoundary = value; }
        }

        public String listToString()
        {
            String rst = "";
            foreach (PointModel pm in lineGeometries)
            {
                rst += pm.x.ToString() + "," + pm.y.ToString() + " ";
            }
            return rst;
        }
        //public List<PointModel> innterBoundary;

        //public String InnterBoundary
        //{
        //    get {
        //        if (innterBoundary.Count == 0)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            return innterBoundary.ToString();
        //        }
        //    }
        //    //set { _innterBoundary = value; }
        //}

    }
}
