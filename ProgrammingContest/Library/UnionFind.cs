using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingContest.Library
{
    class UnionFind
    {
        private int[] Uni { get; }
        public int Count { get; private set; }

        public UnionFind(int n)
        {
            this.Uni = Enumerable.Repeat(-1, n).ToArray();
            this.Count = n;
        }

        public int Find(int n)
            => (this.Uni[n] < 0 ? 
                n : this.Uni[n] = this.Find(this.Uni[n]));

        public bool Unite(int a, int b)
        {
            if ((a = this.Find(a)) == (b = this.Find(b)))
            {
                return false;
            }
            if (this.Uni[a] > this.Uni[b])
            {
                int t = a;
                a = b;
                b = t;
            }
            this.Uni[a] += this.Uni[b];
            this.Uni[b] = a;
            this.Count--;
            return true;
        }

        public bool Same(int a, int b)
            => this.Find(a) == this.Find(b);

        public int GroupCount(int n)
            => -this.Uni[this.Find(n)];
    }
}
