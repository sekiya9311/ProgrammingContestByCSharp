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
            if (First is IComparable<T1>)
            {
                ret = (First as IComparable<T1>).CompareTo(other.First);
            }
            if (ret == 0 && Second is IComparable<T2>)
            {
                ret = (Second as IComparable<T2>).CompareTo(other.Second);
            }
            return ret;
        }
        public override bool Equals(object obj)
        {
            if (obj is Pair<T1, T2>)
            {
                return this.Equals(obj as Pair<T1, T2>);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return this.First.GetHashCode() ^ this.Second.GetHashCode();
        }
        public Pair<T1, T2> Clone()
        {
            return new Pair<T1, T2>(this.First, this.Second);
        }

        public bool Equals(Pair<T1, T2> other)
        {
            return this.First.Equals(other.First) && this.Second.Equals(other.Second);
        }

        public void Dispose()
        {
            if (this.First is IDisposable)
            {
                (this.First as IDisposable).Dispose();
            }
            if (this.Second is IDisposable)
            {
                (this.Second as IDisposable).Dispose();
            }
        }

        public static bool operator ==(Pair<T1, T2> left, Pair<T1, T2> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Pair<T1, T2> left, Pair<T1, T2> right)
        {
            return !left.Equals(right);
        }
    }
}
