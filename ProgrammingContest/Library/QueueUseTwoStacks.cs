using System.Collections.Generic;

namespace ProgrammingContest.Library
{
    /// <summary>
    /// Stackを2つ内部で使用したQueueの実装
    /// </summary>
    /// <typeparam name="T">格納するオブジェクトの型</typeparam>
    class QueueUseTwoStacks<T>
    {
        /// <summary>
        /// 1つ目のStack
        /// </summary>
        public Stack<T> FirstStack { get; }

        /// <summary>
        /// 2つめのStack
        /// </summary>
        public Stack<T> SecondStack { get; }

        /// <summary>
        /// 新しいインスタンスの初期化を行う
        /// </summary>
        public QueueUseTwoStacks()
        {
            this.FirstStack = new Stack<T>();
            this.SecondStack = new Stack<T>();
        }

        /// <summary>
        /// 1つ目のStackから2つ目のStackへの移動処理を行う
        /// </summary>
        private void MoveCheck()
        {
            if (SecondStack.Count == 0)
            {
                while (FirstStack.Count > 0)
                {
                    SecondStack.Push(FirstStack.Pop());
                }
            }
        }

        /// <summary>
        /// 先頭にあるオブジェクトを削除せずに返す
        /// </summary>
        /// <returns>Queueの先頭にあるオブジェクト</returns>
        public T Peek()
        {
            MoveCheck();
            return SecondStack.Peek();
        }

        /// <summary>
        /// 先頭にあるオブジェクトを削除し，返す
        /// </summary>
        /// <returns>Queueの先頭にあったオブジェクト</returns>
        public T Dequeue()
        {
            MoveCheck();
            return SecondStack.Pop();
        }

        /// <summary>
        /// 末尾にオブジェクトを挿入する
        /// </summary>
        /// <param name="val">挿入するオブジェクト</param>
        public void Enqueue(T val)
        {
            this.FirstStack.Push(val);
        }

        /// <summary>
        /// 格納されている要素の数を返す
        /// </summary>
        public int Count
        {
            get
            {
                return FirstStack.Count + SecondStack.Count;
            }
        }
    }
}
