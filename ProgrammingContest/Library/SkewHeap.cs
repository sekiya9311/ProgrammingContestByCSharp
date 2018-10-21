using System;

namespace ProgrammingContest.Library
{
    class SkewHeap<T> where T : IComparable<T>
    {
        private class HeapNode
        {
            public HeapNode l, r;
            public T val;
            public static Func<T, T, int> CompareTo;

            public HeapNode(T val)
            {
                this.val = val;
                this.l = this.r = null;
            }
            public static HeapNode Meld(HeapNode leftNode, HeapNode rightNode)
            {
                if (leftNode == null)
                {
                    return rightNode;
                }
                if (rightNode == null)
                {
                    return leftNode;
                }
                if (HeapNode.CompareTo(leftNode.val, rightNode.val) > 0)
                {
                    var t = rightNode;
                    rightNode = leftNode;
                    leftNode = t;
                }
                leftNode.r = HeapNode.Meld(leftNode.r, rightNode);
                var t2 = leftNode.l;
                leftNode.l = leftNode.r;
                leftNode.r = t2;
                return leftNode;
            }
        }
        private HeapNode topNode;
        private int count_;

        public int Count { get { return this.count_; } }

        public SkewHeap(Func<T, T, int> comp = null)
        {
            HeapNode.CompareTo = (comp ?? ((T l, T r) => l.CompareTo(r)));
            this.topNode = null;
            this.count_ = 0;
        }
        /// <summary>
        /// 捨てる
        /// </summary>
        public void Pop()
        {
            this.topNode = HeapNode.Meld(this.topNode.l, this.topNode.r);
            this.count_--;
        }
        /// <summary>
        /// 見る
        /// </summary>
        public T Top()
        {
            return this.topNode.val;
        }
        /// <summary>
        /// 入れる
        /// </summary>
        public void Push(T val)
        {
            this.topNode = HeapNode.Meld(this.topNode, new HeapNode(val));
            this.count_++;
        }
        /// <summary>
        /// 出す
        /// </summary>
        public T Poll()
        {
            T retVal = this.Top();
            this.Pop();
            return retVal;
        }
        public bool IsEmpty()
        {
            return this.topNode == null;
        }
        public void Merge(SkewHeap<T> otherHeap)
        {
            this.count_ += otherHeap.Count;
            this.topNode = HeapNode.Meld(this.topNode, otherHeap.topNode);
        }
    }
}
