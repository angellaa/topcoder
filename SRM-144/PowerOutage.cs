// BEGIN CUT HERE

// END CUT HERE
using System;
using System.Collections.Generic;
using System.Linq;

public class PowerOutage 
{
    public class Edge
    {
        public int From;
        public int To;
        public int Weight;

        public Edge(int from, int to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }

    public class Graph
    {
        public readonly HashSet<Edge>[] Adj;

        public Graph(int v)
        {
            Adj = new HashSet<Edge>[v];
            for (int i = 0; i < v; i++) 
                Adj[i] = new HashSet<Edge>();
        }

        public void AddEdge(Edge edge)
        {
            Adj[edge.From].Add(edge);
        }
    }

    private int weight = 0;
    private int bestWeight = int.MaxValue;
    private int[] visitedNodes;
    private HashSet<Edge> visitedEdges;

    public int estimateTimeOut(int[] fromJunction, int[] toJunction, int[] ductLength)
    {
        var graph = new Graph(50);

        for (int i = 0; i < fromJunction.Length; i++)
        {
            graph.AddEdge(new Edge(fromJunction[i], toJunction[i], ductLength[i]));
            graph.AddEdge(new Edge(toJunction[i], fromJunction[i], ductLength[i]));
        }

        visitedEdges = new HashSet<Edge>();
        visitedNodes = new int[50];

        for (int i = 0; i < 50; i++)
        {
            visitedNodes[i] = fromJunction.Union(toJunction).Contains(i) ? 0 : 2;
        }

        Traverse(graph, 0);

        return bestWeight;
    }

    private void Traverse(Graph graph, int p)
    {
        if (weight > bestWeight)
            return;

        if (visitedNodes.All(x => x > 0))
        {
            if (weight < bestWeight) bestWeight = weight;
            return;
        }

        foreach (var edge in graph.Adj[p].Except(visitedEdges))
        {
            weight += edge.Weight;
            visitedEdges.Add(edge);
            visitedNodes[edge.From]++;
            visitedNodes[edge.To]++;

            Traverse(graph, edge.To);

            weight -= edge.Weight;
            visitedEdges.Remove(edge);
            visitedNodes[edge.From]--;
            visitedNodes[edge.To]--;
        }
    }

// BEGIN CUT HERE
    public static void Main(String[] args) {
        try  {
            eq(0,(new PowerOutage()).estimateTimeOut(new int[] {0}, new int[] {1}, new int[] {10}),10);
            eq(1,(new PowerOutage()).estimateTimeOut(new int[] {0,1,0}, new int[] {1,2,3}, new int[] {10,10,10}),40);
            eq(2,(new PowerOutage()).estimateTimeOut(new int[] {0,0,0,1,4}, new int[] {1,3,4,2,5}, new int[] {10,10,100,10,5}),165);
            eq(3,(new PowerOutage()).estimateTimeOut(new int[] {0,0,0,1,4,4,6,7,7,7,20}, new int[] {1,3,4,2,5,6,7,20,9,10,31}, new int[] {10,10,100,10,5,1,1,100,1,1,5}),281);
            eq(4,(new PowerOutage()).estimateTimeOut(new int[] {0,0,0,0,0}, new int[] {1,2,3,4,5}, new int[] {100,200,300,400,500}),2500);
            eq(5, (new PowerOutage()).estimateTimeOut(
                new int[] {0, 0, 1, 1, 0, 2, 3, 5, 0, 8, 4, 0, 7, 11, 0, 7, 4, 1, 10, 0, 14, 1, 14, 2, 5, 22, 17, 20, 11, 4, 9, 17, 22, 22, 11, 34}, 
                new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36},
                new int[] { 1018364, 366729, 38720, 309940, 930370, 1180695, 184916, 1682446, 1464885, 1419914, 627577, 1694249, 3555, 1141976, 1605618, 354404, 1442970, 1889613, 1017314, 1745357, 799406, 549771, 1861235, 592722, 930547, 1314662, 1026768, 271675, 781098, 170104, 1424080, 324735, 394783, 1118990, 351154, 1637251 }),
                62287142);
        } 
        catch( Exception exx)  {
            System.Console.WriteLine(exx);
            System.Console.WriteLine( exx.StackTrace);
        }
    }
    private static void eq( int n, object have, object need) {
        if( eq( have, need ) ) {
            Console.WriteLine( "Case "+n+" passed." );
        } else {
            Console.Write( "Case "+n+" failed: expected " );
            print( need );
            Console.Write( ", received " );
            print( have );
            Console.WriteLine();
        }
    }
    private static void eq( int n, Array have, Array need) {
        if( have == null || have.Length != need.Length ) {
            Console.WriteLine("Case "+n+" failed: returned "+have.Length+" elements; expected "+need.Length+" elements.");
            print( have );
            print( need );
            return;
        }
        for( int i= 0; i < have.Length; i++ ) {
            if( ! eq( have.GetValue(i), need.GetValue(i) ) ) {
                Console.WriteLine( "Case "+n+" failed. Expected and returned array differ in position "+i );
                print( have );
                print( need );
                return;
            }
        }
        Console.WriteLine("Case "+n+" passed.");
    }
    private static bool eq( object a, object b ) {
        if ( a is double && b is double ) {
            return Math.Abs((double)a-(double)b) < 1E-9;
        } else {
            return a!=null && b!=null && a.Equals(b);
        }
    }
    private static void print( object a ) {
        if ( a is string ) {
            Console.Write("\"{0}\"", a);
        } else if ( a is long ) {
            Console.Write("{0}L", a);
        } else {
            Console.Write(a);
        }
    }
    private static void print( Array a ) {
        if ( a == null) {
            Console.WriteLine("<NULL>");
        }
        Console.Write('{');
        for ( int i= 0; i < a.Length; i++ ) {
            print( a.GetValue(i) );
            if( i != a.Length-1 ) {
                Console.Write(", ");
            }
        }
        Console.WriteLine( '}' );
    }
// END CUT HERE
}

