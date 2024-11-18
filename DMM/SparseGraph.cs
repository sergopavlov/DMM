using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMM;
public class SparseGraph(int N, (int vertNum, double weight)[][] Edges)
{
    public int N { get; set; } = N;
    public (int vertNum,double weight)[][] Edges = Edges;
    public double[] Dejkstra(int a)
    {
        HashSet<int> notVisited = Enumerable.Range(0, N).ToHashSet();
        double[] d = Enumerable.Repeat(double.MaxValue, N).ToArray();

        d[a] = 0;

        int current = a;

        do
        {
            foreach (var edge in Edges[current])
            {
                if (d[edge.vertNum] > d[current] + edge.weight)
                {
                    d[edge.vertNum] = d[current] + edge.weight;
                }
            }
            notVisited.Remove(current);
            if (notVisited.Count == 0)
                break;
            current = notVisited.MinBy(t => d[t]);
        } while (true);

        return d;
    }

    public double[] Dejkstra2(int a)
    {
        BinaryHeap notVisited = new BinaryHeap(N,a);

        while(notVisited.Count>0)
        {
            var current = notVisited.ExtractMin();
            foreach(var edge in Edges[current.vertnum])
            {
                if (notVisited[edge.vertNum].weight > current.weight + edge.weight)
                {
                    notVisited.SetValue(edge.vertNum, current.weight + edge.weight);
                }
            }
        }

        return notVisited.visited;
    }
}
