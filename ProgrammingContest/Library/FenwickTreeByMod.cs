
namespace ProgrammingContest.Library
{
    class FenwickTreeByMod
    {
        private readonly long[] bit;
        private readonly int MOD;
        public FenwickTreeByMod(int n, int MOD)
        {
            this.bit = new long[n];
            this.MOD = MOD;
        }
        public void Add(int idx, long val)
        {
            val += MOD;
            val %= MOD;
            for (int i = idx; i < this.bit.Length; i |= i + 1)
            {
                this.bit[i] += val;
                this.bit[i] %= MOD;
            }
        }
        // [0, n)
        public long Get(int r)
        {
            long ret = 0;
            for (int i = r - 1; i >= 0; i = (i & (i + 1)) - 1)
            {
                ret += this.bit[i];
                ret %= MOD;
            }
            return ret;
        }
        // [l, r)
        public long Get(int l, int r)
        {
            return (this.Get(r) - this.Get(l - 1) + MOD) % MOD;
        }
    }
}
