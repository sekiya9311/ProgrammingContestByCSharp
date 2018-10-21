using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingContest.Library
{
    class SegmentTree<T>
    {
        int length_;
        T initVal;
        T[] data, lazy;
        bool[] lazyFlag;
        Func<T, T, T> op;
        public SegmentTree(int length_, T initVal, Func<T, T, T> func)
        {
            this.initVal = initVal;
            this.length_ = length_ << 2;
            this.data = new T[this.Length];
            this.lazy = new T[this.Length];
            this.lazyFlag = new bool[this.Length];
            this.length_ >>= 1;
            this.op = func;
            for (int i = 0; i < this.Length; i++)
            {
                this.data[i] = this.lazy[i] = this.initVal;
            }
        }
        private void Push(int k, int l, int r)
        {
            if (this.lazyFlag[k])
            {

            }
        }
        private void Update(int l, int r, int L, int R, int k, T val)
        {

        }
        private T Get(int l, int r, int L, int R, int k)
        {
            return default(T);
        }
        public void Update(int l, int r, T val)
        {
            this.Update(0, this.Length, l, r, 0, val);
        }
        public T Get(int l, int r)
        {
            return this.Get(0, this.Length, l, r, 0);
        }
        public int Length
        {
            get
            {
                return this.length_;
            }
        }
    }
}
