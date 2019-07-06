using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingContest.Library
{
    class Utility
    {
        /// <summary>
        /// 配列一覧表示
        /// </summary>
        string EnumerableToString<T>(IEnumerable<T> enumerable, string sep = " ") 
            => string.Join(sep, enumerable.Select(el => el.ToString()));

        /// <summary>
        /// バイナリ法による法mod のべき乗計算 O(log p)
        /// </summary>
        long PowMod(long a, long p, long mod)
        {
            if (p == 0) return 1;
            if (p % 2 == 1) return a * PowMod(a, p - 1, mod) % mod;
            else
            {
                long t = PowMod(a, p / 2, mod);
                return t * t % mod;
            }
        }

        /// <summary>
        /// エラトステネスの篩 による素数列挙 O(N loglogN)
        /// </summary>
        IEnumerable<int> SieveOfEratosthenes(int maxVal)
        {
            bool[] isPrime = Enumerable.Repeat(true, maxVal + 1).ToArray();
            if (isPrime.Length < 2)
            {
                yield break;
            }
            isPrime[0] = isPrime[1] = false;
            for (int i = 2; i < maxVal + 1; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * 2; j < maxVal + 1; j += i)
                    {
                        isPrime[j] = false;
                    }
                    yield return i;
                }
            }
        }

        /// <summary>
        /// 素数判定 O(√N)
        /// </summary>
        bool IsPrime(long val)
        {
            if (val < 2)
            {
                return false;
            }
            for (long i = 2; i * i <= val; i++)
            {
                if (val % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 文字列反転 {多分 O(N)}
        /// </summary>
        string ReverseStr(string str) 
            => string.Concat(str.Reverse());

        /// <summary>
        /// 配列初期化 O(N)
        /// </summary>
        void Fill<T>(ref T[] ar, T val)
        {
            for (int i = 0; i < ar.Length; i++)
            {
                ar[i] = val;
            }
        }

        /// <summary>
        /// スワップ O(1)
        /// </summary>
        void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        /// <summary>
        /// 配列全列挙 O(N(N!))? (ソートされていること!!)
        /// </summary>
        bool NextPermutation<T>(T[] ar) where T : IComparable<T>
        {
            int left = ar.Length - 1;
            int CompForCalcNextPermutation(int i, int j) 
                => ar[i].CompareTo(ar[j]);
            while (0 < left && CompForCalcNextPermutation(left - 1, left) >= 0)
            {
                left--;
            }
            if (left == 0)
            {
                return false;
            }
            int right = ar.Length - 1;
            while (CompForCalcNextPermutation(right, left - 1) <= 0)
            {
                right--;
            }
            T tmp = ar[left - 1];
            ar[left - 1] = ar[right];
            ar[right] = tmp;
            Array.Reverse(ar, left, ar.Length - left);
            return true;
        }

        /// <summary>
        /// 最大公約数 O(log max(a, b))
        /// </summary>
        long Gcd(long a, long b)
        {
            if (a < b)
            {
                var tmp = b;
                b = a;
                a = tmp;
            }
            var ret = 1L;
            while (b != 0)
            {
                ret = b;
                b = a % b;
                a = ret;
            }
            return ret;
        }
        /// <summary>
        /// 最小公倍数 O(log max(a, b))
        /// Gcd使用
        /// </summary>
        long Lcm(long a, long b) => a / Gcd(a, b) * b;

        /// <summary>
        /// 約数列挙 O(√N)
        /// </summary>
        IEnumerable<long> CalcDivisor(long num)
        {
            for (long i = 1; i * i <= num; i++)
            {
                if (num % i == 0)
                {
                    yield return i;
                    if (num / i != i)
                    {
                        yield return num / i;
                    }
                }
            }
        }

        int BitCount(long val)
        {
            val = (val & 0x55555555) + (val >> 1 & 0x55555555);
            val = (val & 0x33333333) + (val >> 2 & 0x33333333);
            val = (val & 0x0f0f0f0f) + (val >> 4 & 0x0f0f0f0f);
            val = (val & 0x00ff00ff) + (val >> 8 & 0x00ff00ff);
            return (int)((val & 0x0000ffff) + (val >> 16 & 0x0000ffff));
        }

        IDictionary<TKey, TValue> Merge<TKey, TValue>(
            IEnumerable<IDictionary<TKey, TValue>> source,
            Func<IEnumerable<TValue>, TValue> aggregate)
            => source
                .SelectMany(e => e.AsEnumerable())
                .ToLookup(e => e.Key)
                .ToDictionary(e => e.Key, e => aggregate(e.Select(el => el.Value)));
    }
}
