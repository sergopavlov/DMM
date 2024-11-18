using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMM;
public class BinaryHeap
{
    (int vertice, double weight)[] heap;
    int[] index;
    public double[] visited;
    int _count;
    public int Count => _count;
    public (int vertice, double weight) this[int i]
    {
        get
        {
            return heap[index[i]];
        }
    }

    public BinaryHeap(int N, int start)
    {

        heap = Enumerable.Range(0, N).Select(t => (t, double.PositiveInfinity)).ToArray();
        index = Enumerable.Range(0, N).ToArray();

        heap[start] = (0, double.PositiveInfinity);
        index[0] = start;

        heap[0] = (start, 0);
        index[start] = 0;

        visited = Enumerable.Repeat(double.PositiveInfinity, N).ToArray();

        _count = N;


    }
    private void siftDown(int i)
    {
        while (2 * i + 2 < _count)
        {
            var left = 2 * i + 1;
            var right = 2 * i + 2;
            var j = left;
            if (right < _count && heap[right].weight < heap[left].weight)
                j = right;
            if (heap[i].weight <= heap[j].weight)
                break;
            (heap[i], heap[j]) = (heap[j], heap[i]);
            (index[heap[i].vertice], index[heap[j].vertice]) = (index[heap[j].vertice], index[heap[i].vertice]);
            i = j;
        }
    }

    private void siftUp(int i)
    {
        while (heap[i].weight < heap[(i - 1) / 2].weight)
        {
            (heap[i], heap[(i - 1) / 2]) = (heap[(i - 1) / 2], heap[i]);
            (index[heap[i].vertice], index[heap[(i - 1) / 2].vertice]) = (index[heap[(i - 1) / 2].vertice], index[heap[i].vertice]);
            i = (i - 1) / 2;
        }
    }

    public (int vertnum, double weight) ExtractMin()
    {
        var min = heap[0];
        heap[0] = heap[_count - 1];
        index[heap[_count - 1].vertice] = 0;
        _count--;
        siftDown(0);
        visited[min.vertice] = min.weight;
        return min;
    }
    public void SetValue(int vertnum, double weight)
    {
        var current = heap[index[vertnum]];
        bool becameBigger = current.weight < weight;
        heap[index[vertnum]].weight = weight;
        if (becameBigger)
            siftDown(index[vertnum]);
        else
            siftUp(index[vertnum]);
    }


}
