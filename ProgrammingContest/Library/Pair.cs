using System;

namespace ProgrammingContest.Library
{
    class Pair<T1, T2> 
        : IComparable<Pair<T1, T2>>, 
          IEquatable<Pair<T1, T2>>, 
          IDisposable
    {
        public T1 First { get; set; }
        public T2 Second { get; set; }

        public Pair() { }

        public Pair(T1 f, T2 s)
        {
            this.First = f;
            this.Second = s;
        }

        public int CompareTo(Pair<T1, T2> other)
        {
            int ret = 0;
            if (First is IComparable<T1> tmp)
            {
                ret = tmp.CompareTo(other.First);
            }
            if (ret == 0 && Second is IComparable<T2> tmp2)
            {
                ret = tmp2.CompareTo(other.Second);
            }
            return ret;
        }

        public bool Equals(Pair<T1, T2> other)
            => this == other;

        public override bool Equals(object obj) 
            => obj is Pair<T1, T2> p && this == p;

        public override int GetHashCode() 
            => this.First.GetHashCode() ^ this.Second.GetHashCode();

        public Pair<T1, T2> Clone() 
            => new Pair<T1, T2>(this.First, this.Second);

        public void Dispose()
        {
            if (this.First is IDisposable tmp)
            {
                tmp.Dispose();
            }
            if (this.Second is IDisposable tmp2)
            {
                tmp2.Dispose();
            }
        }

        public static bool operator ==(Pair<T1, T2> left, Pair<T1, T2> right) 
            => left.First.Equals(right.First) && left.Second.Equals(right.Second);

        public static bool operator !=(Pair<T1, T2> left, Pair<T1, T2> right) 
            => !(left == right);
    }
}
