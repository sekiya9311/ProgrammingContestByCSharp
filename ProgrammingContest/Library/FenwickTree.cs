
namespace ProgrammingContest.Library
{
    class FenwickTree
    {
        private readonly long[] bit;
        public FenwickTree(int n)
        {
            this.bit = new long[n];
        }

        public void Add(int idx, long val)
        {
            for (int i = idx; i < this.bit.Length; i |= i + 1)
            {
                this.bit[i] += val;
            }
        }

        // [0, n)
        public long Get(int r)
        {
            long ret = 0;
            for (int i = r - 1; i >= 0; i = (i & (i + 1)) - 1)
            {
                ret += this.bit[i];
            }
            return ret;
        }
        // [l, r)
        public long Get(int l, int r)
        {
            return this.Get(r) - this.Get(l - 1);
        }
    }
}
