using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ProgrammingContest
{
    class Writer : IDisposable
    {
        private readonly System.IO.TextWriter _out;
        private readonly StringBuilder _outCache;
        private readonly bool _isReactice;

        public Writer(string path)
            : this(new System.IO.StreamWriter(path))
        {
        }
        public Writer(bool isReactive)
            : this(null, isReactive)
        {
        }
        public Writer(System.IO.TextWriter writer = null, bool isReactive = false)
        {
            this._out = (writer ?? Console.Out);
            this._isReactice = isReactive;
            if (!this._isReactice)
            {
                this._outCache = new StringBuilder();
            }
        }
        public void Dispose()
        {
            if (!this._isReactice)
            {
                this._out.Write(_outCache.ToString());
            }
            if (!this._out.Equals(Console.Out))
            {
                this._out.Dispose();
            }
        }
        public void Write(object val)
        {
            if (this._isReactice)
            {
                this._out.Write(val.ToString());
                this._out.Flush();
            }
            else
            {
                this._outCache.Append(val.ToString());
            }
        }
        public void WriteFormat(string format, params object[] vals)
        {
            if (this._isReactice)
            {
                this._out.Write(format, vals);
                this._out.Flush();
            }
            else
            {
                this._outCache.AppendFormat(format, vals);
            }
        }

        public void WriteLine(object val = null)
            => this.WriteFormat((val?.ToString() ?? string.Empty) + Environment.NewLine);

        public void WriteLine(int val)
            => this.WriteLine(val.ToString());

        public void WriteLine(long val)
            => this.WriteLine(val.ToString());

        public void WriteLine(string val)
            => this.WriteLine((object)val);

        public void WriteLine(string format, params object[] vals)
            => this.WriteFormat(format + Environment.NewLine, vals);
    }

    class Scanner : IDisposable
    {
        private readonly Queue<string> _buffer;
        private readonly char[] _sep;
        private readonly System.IO.TextReader _reader;

        public Scanner(string path, char[] sep = null)
            : this(new System.IO.StreamReader(path), sep)
        {
        }
        public Scanner(
            System.IO.TextReader reader = null,
            char[] sep = null)
        {
            this._buffer = new Queue<string>();
            this._sep = (sep ?? new char[] { ' ' });
            this._reader = (reader ?? Console.In);
        }
        private void CheckBuffer()
        {
            if (this._buffer.Count == 0 && this._reader.Peek() != -1)
            {
                string str = string.Empty;
                for (; string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
                str = this._reader.ReadLine()) ;

                var strs = str.Split(this._sep)
                    .Where(el => !(string.IsNullOrEmpty(el) || string.IsNullOrWhiteSpace(el)));
                foreach (var el in strs)
                {
                    this._buffer.Enqueue(el);
                }
            }
        }

        public void Dispose()
        {
            if (!this._reader.Equals(Console.In))
            {
                this._reader.Dispose();
            }
        }

        public string Next()
        {
            this.CheckBuffer();
            return this._buffer.Dequeue();
        }

        public string[] GetStringArray(int N)
            => Enumerable.Range(0, N)
                .Select(e => this.Next())
                .ToArray();

        public int NextInt()
            => int.Parse(this.Next());

        public int[] GetIntArray(int N)
            => Enumerable.Range(0, N)
                .Select(e => this.NextInt())
                .ToArray();

        public double NextDouble()
            => double.Parse(this.Next());

        public double[] GetdoubleArray(int N)
            => Enumerable.Range(0, N)
                .Select(e => this.NextDouble())
                .ToArray();

        public long NextLong()
            => long.Parse(this.Next());

        public long[] GetLongArray(int N)
            => Enumerable.Range(0, N)
                .Select(e => this.NextLong())
                .ToArray();

        public BigInteger NextBigInt()
            => BigInteger.Parse(this.Next());

        public BigInteger[] GetBigIntArray(int N)
            => Enumerable.Range(0, N)
                .Select(e => this.NextBigInt())
                .ToArray();

        public bool IsEnd
        {
            get
            {
                this.CheckBuffer();
                return this._buffer.Count == 0;
            }
        }
    }

    class MainClass : IDisposable
    {
        private Scanner Sc { get; }
        private Writer Wr { get; }
        private string InFilePath => "in.txt";
        private string OutFilePath => "out.txt";
        public MainClass()
        {
            this.Wr = new Writer(this.IsReactive);
            //this.Wr = new Writer(this.OutFilePath);
#if DEBUG
            if (!this.IsReactive)
            {
                this.Sc = new Scanner(this.InFilePath);
            }
            else
            {
                this.Sc = new Scanner();
            }
#else
            this.Sc = new Scanner();
#endif
            this.Solve();
        }
        static void Main(string[] args)
        {
            using (new MainClass()) { }
        }

        public void Dispose()
        {
            this.Sc?.Dispose();
            this.Wr?.Dispose();
#if DEBUG
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
#endif
        }

        void Solve()
        {
            
        }

        private bool IsReactive => false; // TODO: true, if reactive problem
    }
}
