// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DMM;
using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;


//(int vertNum, double weight)[][] edges =
//    [
//        [(1,4),(2,2),(3,6)],
//        [(2,2),(4,2),(5,1)],
//        [(1,1),(5,3)],
//        [(1,3),(4,5)],
//        [(5,1)],
//        [],
//    ];

//SparseGraph graph = new SparseGraph(6, edges);
//var asd = graph.Dejkstra2(0);

//BenchmarkRunner.Run<DejkstraBenchmarks>();
BenchmarkRunner.Run<DejkstraTreeBenchmarks>();



Console.WriteLine("Hello, World!");


public class DejkstraBenchmarks
{
    static Random random = new Random(Environment.TickCount);
    private SparseGraph graph;
    [Params(0.2, 0.4)]
    public double Sparciness;
    [Params(10, 100, 1000, 10000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        (int, double)[][] edges = new (int, double)[N][];
        for (int i = 0; i < N; i++)
        {
            var current = new List<(int, double)>();
            for (int j = 0; j < N; j++)
            {
                if (random.NextDouble() <= Sparciness && i != j)
                {
                    current.Add((j, random.NextDouble() + 1));
                }
            }
            edges[i] = current.ToArray();
        }
        graph = new SparseGraph(N, edges);
    }
    [Benchmark]
    public void Solve()
    {
        var rezzzzzzultat = graph.Dejkstra2(random.Next(N));
    }

}

public class DejkstraTreeBenchmarks
{
    static Random random = new Random(Environment.TickCount);
    private SparseGraph graph;
    [Params(10, 50, 100, 500, 1000, 5000, 10000, 50000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        List<(int, double)>[] edges = new List<(int, double)>[N];
        for (int i = 0; i < N; i++)
        {
            edges[i] = new();
            if (i != 0)
            {
                edges[random.Next(i - 1)].Add((i, random.NextDouble()));
            }
        }
        graph = new SparseGraph(N, edges.Select(t => t.ToArray()).ToArray());
    }
    [Benchmark]
    public void Solve()
    {
        var rezzzzzzultat = graph.Dejkstra2(random.Next(N));
    }

}