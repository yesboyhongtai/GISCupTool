using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
using BruTile.Cache;
using BruTile;
using System.Drawing;
using System.Reflection;
using SharpMap;
using Point = SharpMap.Geometries.Point;
using SharpMap.Geometries;
namespace PolygonDemo
{
    /// <summary>
    /// Format an object to byte array
    /// </summary>
    public static class ExtMethods
    {
        /// <summary>
        /// Format an object to byte array
        /// </summary>
        /// <param name="o"></param>
        /// <returns>byte array</returns>
        public static byte[] ToByteArray(this object o)
        {
            int size = Marshal.SizeOf(o);
            byte[] buffer = new byte[size];
            IntPtr p = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(o, p, false);
                Marshal.Copy(p, buffer, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(p);
            }
            return buffer;
        }
        /// <summary>
        /// Peform a iteration and do action at each item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
        /// <summary>
        /// Get controls that match the filter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetControls<T>(this Control control, Func<T, bool> filter) where T : Control
        {
            foreach (Control c in control.Controls)
            {
                if (c is T && (filter == null || filter(c as T)))
                {
                    yield return c as T;
                }
                foreach (T _t in GetControls<T>(c, filter))
                {
                    yield return _t;
                }
            }
        }
        public static Dictionary<TileIndex, Bitmap> GetBitMaps(this MemoryCache<Bitmap> mc)
        {
            Dictionary<TileIndex, Bitmap> dict = null;
            Type t = mc.GetType();
            FieldInfo f = t.GetField("_bitmaps", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
            if (f != null)
            {
                dict = f.GetValue(mc) as Dictionary<TileIndex, Bitmap>;
            }
            return dict;
        }
        /// <summary>
        /// 合并两box
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static BoundingBox Merge(this BoundingBox b1, BoundingBox b2)
        {
            BoundingBox result;
            double minX, minY, maxX, maxY;
            minX = Math.Min(b1.Left, b2.Left);
            minY = Math.Min(b1.Bottom, b2.Bottom);
            maxX = Math.Max(b1.Right, b2.Right);
            maxY = Math.Max(b1.Top, b2.Top);
            result = new BoundingBox(minX, minY, maxX, maxY);
            return result;
        }

        /// <summary>
        /// 扩充box使其包容点
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static BoundingBox Merge(this BoundingBox b, Point p)
        {
            BoundingBox result;
            double minX, minY, maxX, maxY;
            minX = Math.Min(b.Left, p.X);
            minY = Math.Min(b.Bottom, p.Y);
            maxX = Math.Max(b.Right, p.X);
            maxY = Math.Max(b.Top, p.Y);
            result = new BoundingBox(minX, minY, maxX, maxY);
            return result;
        }
        /// <summary>
        /// 检测b是否包含p
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Contains2(this BoundingBox b, Point p)
        {
            bool result = false;
            result = (b.Top >= p.Y) && (b.Bottom <= p.Y) && (b.Left <= p.X) && (b.Right >= p.X);
            return result;
        }
        /// <summary>
        /// 检测b1是否包含b2
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static bool Contains2(this BoundingBox b1, BoundingBox b2)
        {
            bool result = false;
            result = (b1.Top >= b2.Top) && (b1.Bottom <= b2.Bottom) && (b1.Left <= b2.Left) && (b1.Right >= b2.Right);
            return result;
        }

        public static BoundingBox Magnify(this BoundingBox b, double rate)
        {
            BoundingBox result;
            double minX, minY, maxX, maxY;
            double width, height;
            double r = (rate - 1) / 2.0;
            width = b.Right - b.Left;
            height = b.Top - b.Bottom;

            minX = b.Left - r * width;
            maxX = b.Right + r * width;
            minY = b.Bottom - r * height;
            maxY = b.Top + r * height;
            
            result = new BoundingBox(minX, minY, maxX, maxY);
            //b = result;
            return result;
        }
    }
}
