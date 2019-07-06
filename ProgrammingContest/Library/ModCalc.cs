using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingContest.Library
{
    /// <summary>
    /// 法modでの計算を行う
    /// </summary>
    class ModCalc
    {
        private readonly int maxVal;
        private readonly int mod;
        private readonly long[] memoFact;
        private readonly long[] memoInvFact;

        public ModCalc(int maxVal, int mod)
        {
            this.maxVal = maxVal;
            this.mod = mod;
            this.memoFact = new long[maxVal + 1];
            this.memoFact[0] = 1;
            for (int i = 0; i < maxVal; i++)
            {
                this.memoFact[i + 1] = Mul(i + 1, this.memoFact[i]);
            }
            this.memoInvFact = new long[maxVal + 1];
            for (int i = 0; i < maxVal + 1; i++)
            {
                this.memoInvFact[i] = Inverse(this.memoFact[i]);
            }
        }

        /// <summary>
        /// 法modで加算を行う
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>引数を加算した値</returns>
        public long Add(long left, long right)
            => (left % mod + right % mod) % mod;

        /// <summary>
        /// 法modで減算を行う
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>引数を減算した値</returns>
        public long Sub(long left, long right)
            => Add(left, -right % mod + mod);

        /// <summary>
        /// 法modで乗算を行う
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>引数を乗算した値</returns>
        public long Mul(long left, long right)
            => (left % mod) * (right % mod) % mod;

        /// <summary>
        /// 法modで除算を行う
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>引数を除算した値</returns>
        public long Div(long left, long right)
            => Mul(left, Inverse(right));

        /// <summary>
        /// 法modで逆数を求める
        /// </summary>
        /// <param name="value">もとの値</param>
        /// <returns>引数の値の逆数</returns>
        public long Inverse(long value)
            => PowMod(value, mod - 2);

        /// <summary>
        /// 法modでバイナリ法により累乗を求める
        /// </summary>
        /// <param name="a">底</param>
        /// <param name="p">指数</param>
        /// <returns>法modでのa^p</returns>
        public long PowMod(long a, long p)
        {
            if (p == 0)
            {
                return 1;
            }
            else if (p % 2 == 1)
            {
                return Mul(a, PowMod(a, p - 1));
            }
            else
            {
                var tmp = PowMod(a, p / 2);
                return Mul(tmp, tmp);
            }
        }

        /// <summary>
        /// 法modで階乗を求める
        /// </summary>
        /// <param name="N">基準値</param>
        /// <returns>法modでのN!</returns>
        public long Fact(int N) => memoFact[N];

        /// <summary>
        /// 法modで階乗値の逆数を求める
        /// </summary>
        /// <param name="N">基準</param>
        /// <returns>法modでのNの逆数</returns>
        public long InvFact(int N) => memoInvFact[N];

        /// <summary>
        /// 法modで順列数を求める
        /// </summary>
        /// <param name="n">元の数</param>
        /// <param name="r">選ぶ数</param>
        /// <returns>法modでのnPr</returns>
        public long Permutation(int n, int r)
            => Fact(n) * InvFact(n - r);

        /// <summary>
        /// 法modで組み合わせ数を求める
        /// </summary>
        /// <param name="n">元の数</param>
        /// <param name="r">選ぶ数</param>
        /// <returns>法modでのnCr</returns>
        public long Combination(int n, int r)
            => Mul(Mul(Fact(n), InvFact(r)), InvFact(n - r));
    }
}
