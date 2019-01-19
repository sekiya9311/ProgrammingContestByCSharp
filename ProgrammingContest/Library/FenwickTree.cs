
namespace ProgrammingContest.Library
{
    using System.Linq;

    class FenwickTree
    {
        private readonly long[] bit;
        public FenwickTree(int n)
        {
            this.bit = Enumerable.Repeat(0L, n).ToArray();
        }

        public void Add(int idx, long val)
        {
            idx++;
            while (idx - 1 < this.bit.Length)
            {
                bit[idx - 1] += val;
                idx += idx & -idx;
            }
        }

        // [0, n)
        public long Get(int r)
        {
            long ret = 0;
            while (r > 0)
            {
                ret += bit[r - 1];
                r -= r & -r;
            }
            return ret;
        }
        // [l, r)
        public long Get(int l, int r)
            => this.Get(r) - this.Get(l);
    }
}
