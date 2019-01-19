
namespace ProgrammingContest.Library
{
    using System.Linq;

    class FenwickTreeByMod
    {
        private readonly long[] bit;
        private readonly int MOD;
        public FenwickTreeByMod(int n, int MOD = (int)1e9 + 7)
        {
            this.bit = Enumerable.Repeat(0L, n).ToArray();
            this.MOD = MOD;
        }
        public void Add(int idx, long val)
        {
            val = ((val % MOD) + MOD) % MOD;
            idx++;
            while (idx - 1 < this.bit.Length)
            {
                bit[idx - 1] += val;
                bit[idx - 1] %= MOD;
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
                ret %= MOD;
                r -= r & -r;
            }
            return ret;
        }
        // [l, r)
        public long Get(int l, int r)
            => (this.Get(r) - this.Get(l) + MOD) % MOD;
    }
}
