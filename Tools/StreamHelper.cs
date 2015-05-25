using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;

namespace PolygonDemo
{
    /// <summary>
    /// Enable a pipe stream to read and write various kinds of data like int, double, string as well as complex structure.
    /// </summary>
    public class StreamHelper
    {
        #region field
        protected Stream m_stream = null;
        /// <summary>
        /// Get the inner stream
        /// </summary>
        public Stream Stream
        {
          get { return m_stream; }
        }
        /// <summary>
        /// Stream encoder, default ascii
        /// </summary>
        protected Encoding streamEncoding = null; 
        #endregion field

        #region method

        /// <summary>
        /// Read data from the pipe
        /// </summary>
        /// <param name="buffer">
        ///   When this method returns, this parameter contains the specified character
        ///    array with the values between index and (index + count -1) replaced by the
        ///    characters read from the current source.
        /// </param>
        /// <param name="offset">
        ///  The byte offset in the buffer array at which the bytes that are read will
        ///  be placed.
        /// </param>
        /// <param name="count"> The maximum number of characters to read.</param>
        /// <returns> 
        /// The position of the underlying stream is advanced by the number of characters
        /// that were read into buffer.The number of characters that have been read.
        /// The number will be less than or equal to count, depending on whether all
        ///  input characters have been read.
        /// </returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            //return sr.Read(buffer, index, count);
            return m_stream.Read(buffer, offset, count);
        }
        /// <summary>
        /// Read a data with type T from the pipe and assigned to data
        /// </summary>
        /// <typeparam name="T">DataType</typeparam>
        /// <param name="data"></param>
        /// <returns>how many bytes are read</returns>
        public int Read<T>(out T data)
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[size];
            IntPtr p = Marshal.AllocHGlobal(size);
            try
            {
                Read(buffer, 0, size);
                //Copy the data to the structure
                Marshal.Copy(buffer, 0, p, size);
                data = (T)Marshal.PtrToStructure(p, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(p);
            }
            return size;
        }
        /// <summary>
        /// Read an integer from  the pipe
        /// </summary>
        /// <returns></returns>
        public int ReadInt()
        {
            int i = 0;
            byte[] buffer = new byte[sizeof(int)];
            Read(buffer, 0, sizeof(int));
            i = BitConverter.ToInt32(buffer, 0);
            return i;
        }
        /// <summary>
        /// Read an long integer from  the pipe
        /// </summary>
        /// <returns></returns>
        public long ReadLong()
        {
            long l = 0;
            byte[] buffer = new byte[sizeof(long)];
            Read(buffer, 0, sizeof(long));
            l = BitConverter.ToInt64(buffer, 0);
            return l;
        }
        /// <summary>
        /// Read a float frome the pipe
        /// </summary>
        /// <returns></returns>
        public float ReadFloat()
        {
            float f;
            byte[] buffer = new byte[sizeof(float)];
            Read(buffer, 0, sizeof(float));
            f = BitConverter.ToSingle(buffer, 0);
            return f;
        }
        /// <summary>
        /// Read a double frome the pipe
        /// </summary>
        /// <returns></returns>
        public double ReadDouble()
        {
            double d;
            byte[] buffer = new byte[sizeof(double)];
            Read(buffer, 0, sizeof(double));
            d = BitConverter.ToDouble(buffer, 0);
            return d;
        }
        /// <summary>
        /// Read a string from the pipe<br/>
        /// Read an integer as the size of the string
        /// Read the data, and store them in the buffer, than convert to string
        /// </summary>
        /// <returns></returns>
        public string ReadString()
        {
            int size;
            Read(out size);
            byte[] buffer = new byte[size];
            string str = string.Empty;
            if (size > 0)
            {
                Read(buffer, 0, size);
                str = streamEncoding.GetString(buffer);
            }
            return str;
        } 
        /// <summary>
        /// Writes a block of bytes to the current stream using data from a buffer.
        /// </summary>
        /// <param name="buffer">The buffer that contains data to write to the pipe.</param>
        /// <param name="offset">
        ///     The zero-based byte offset in buffer at which to begin copying bytes to the
        ///     current stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to write to the current stream.</param>
        public void Write(byte[] buffer, int offset, int count)
        {
            m_stream.Write(buffer, offset, count);
        }
        /// <summary>
        /// Write data to the pipe.
        /// </summary>
        /// <typeparam name="T">The specified type</typeparam>
        /// <param name="data"></param>
        public void Write<T>(T data) 
        {
            int size = Marshal.SizeOf(data);
            byte[] buffer = data.ToByteArray();
            Write(buffer, 0, size);
        }
        /// <summary>
        /// Write an integer
        /// </summary>
        /// <param name="i"></param>
        public void WriteInt(int i)
        {
            int size = sizeof(int);
            byte[] buffer = BitConverter.GetBytes(i);
            Write(buffer, 0, size);
        }
        /// <summary>
        /// Write a long integer
        /// </summary>
        /// <param name="i"></param>
        public void WriteLong(long i)
        {
            int size = sizeof(long);
            byte[] buffer = BitConverter.GetBytes(i);
            Write(buffer, 0, size);
        }
        /// <summary>
        /// Write a float
        /// </summary>
        /// <param name="i"></param>
        public void WriteFloat(float f)
        {
            int size = sizeof(float);
            byte[] buffer = BitConverter.GetBytes(f);
            Write(buffer, 0, size);
        }
        /// <summary>
        /// Write an integer
        /// </summary>
        /// <param name="d"></param>
        public void WriteDouble(double d)
        {
            int size = sizeof(double);
            byte[] buffer = BitConverter.GetBytes(d);
            Write(buffer, 0, size);
        }
        /// <summary>
        /// Read a string from the pipe<br/>
        /// Read an integer as the size of the string
        /// Read the data, and store them in the buffer, than convert to string
        /// </summary>
        /// <returns></returns>
        public void WriteString(string str)
        {
            byte[] buffer = streamEncoding.GetBytes(str);
            int size = buffer.Length;
            if (size > 0)
            {
                WriteInt(size);
                Write(buffer, 0, buffer.Length);
            }
        }
        #endregion method
        /// <summary>
        ///  Encapsulate the named pipe
        /// </summary>
        /// <param name="serverName">Server name</param>
        /// <param name="pipeName">the pipe's name</param>
        /// <param name="direction">In,Out,InOut</param>
        public StreamHelper(Stream stream)
        {
            //streamEncoding = new UnicodeEncoding();
            this.m_stream = stream;
            streamEncoding = Encoding.Default;
        }
        ~StreamHelper()
        {
            m_stream.Close();
        }
    }
}
