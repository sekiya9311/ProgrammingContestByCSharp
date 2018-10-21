using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingContest.Library
{
    class Graph
    {
        public const long InfCost = (long)1e18;
        public struct Edge : IComparable<Edge>
        {
            public int To { get; }
            public long Cost { get; }
            public Edge(int to, long cost)
            {
                this.To = to;
                this.Cost = cost;
            }

            public int CompareTo(Edge other)
            {
                return Math.Sign(this.Cost - other.Cost);
            }
        }
        public List<Edge>[] ListGraph { get; }
        public Graph(int N)
        {
            this.ListGraph =
                new List<Edge>[N].Select(e => new List<Edge>())
                                 .ToArray();
        }
        public void AddEdge(int from, int to, long cost)
        {
            this.ListGraph[from].Add(new Edge(to, cost));
        }
        public long[,] MatrixGraph()
        {
            int N = this.ListGraph.Length;
            var ret = new long[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    ret[i, j] = InfCost;
                }
                ret[i, i] = 0;
            }
            for (int from = 0; from < N; from++)
            {
                foreach (var e in this.ListGraph[from])
                {
                    ret[from, e.To] = e.Cost;
                }
            }
            return ret;
        }
        public long[,] WarshallFloyd()
        {
            int N = this.ListGraph.Length;
            var mat = this.MatrixGraph();
            for (int k = 0; k < N; k++)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        mat[i, j] = Math.Min(mat[i, j], mat[i, k] + mat[k, j]);
                    }
                }
            }
            return mat;
        }
        public long[] DijkstraByMatrix(int st)
        {
            int N = this.ListGraph.Length;
            var mat = this.MatrixGraph();
            var ret = new long[N];
            for (int i = 0; i < N; i++)
            {
                ret[i] = InfCost;
            }
            var vis = new bool[N];
            ret[st] = 0;
            while (true)
            {
                int idx = -1;
                for (int i = 0; i < N; i++)
                {
                    if (vis[i])
                    {
                        continue;
                    }
                    if (idx == -1)
                    {
                        idx = i;
                    }
                    if (ret[i] < ret[idx])
                    {
                        idx = i;
                    }
                }
                if (idx == -1)
                {
                    break;
                }
                vis[idx] = true;
                for (int i = 0; i < N; i++)
                {
                    ret[i] = Math.Min(ret[i], ret[idx] + mat[idx, i]);
                }
            }
            return ret;
        }
        public int[] TopologicalSort()
        {
            int N = this.ListGraph.Length;
            var tsort = new List<int>();
            var inCnt = new int[N];
            for (int from = 0; from < N; from++)
            {
                foreach (var e in this.ListGraph[from])
                {
                    inCnt[e.To]++;
                }
            }
            var q = new Queue<int>();
            for (int i = 0; i < N; i++)
            {
                if (inCnt[i] == 0)
                {
                    q.Enqueue(i);
                }
            }
            while (q.Count > 0)
            {
                int now = q.Dequeue();
                tsort.Add(now);
                foreach (var e in this.ListGraph[now])
                {
                    inCnt[e.To]--;
                    if (inCnt[e.To] == 0)
                    {
                        q.Enqueue(e.To);
                    }
                }
            }
            return tsort.Count == N ? tsort.ToArray() 
                                    : null;
        }

    }
}
