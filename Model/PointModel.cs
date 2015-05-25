using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolygonDemo.Model
{
    public class PointModel
    {
        public PointModel()
        {}

        /// <summary>
        /// the id of point
        /// </summary>
        private int _id = 0;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
 
        /// <summary>
        /// the timestamp of point
        ///// </summary>
        //private int _time;

        //public int time
        //{
        //    get { return _time; }
        //    set { _time = value; }
        //}

        private double _x;

        public double x
        {
            get { return _x; }
            set { _x = value; }
        }

        private double _y;

        public double y
        {
            get { return _y; }
            set { _y = value; }
        }

    }
}
