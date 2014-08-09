// BEGIN CUT HERE

// END CUT HERE
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class PenLift 
{
    private class Point : IEquatable<Point>
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x; 
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        public bool Equals(Point other)
        {
            return x == other.x && y == other.y;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    }

    private class Segment : IEquatable<Segment>
    {
        public int x1 { get { return p1.x; } }
        public int y1 { get { return p1.y; } }

        public int x2 { get { return p2.x; } }
        public int y2 { get { return p2.y; } }

        public Point p1;
        public Point p2;

        public Segment(Point p1, Point p2) { this.p1 = p1; this.p2 = p2; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Segment);
        }

        public bool Equals(Segment other)
        {
            return p1.Equals(other.p1) && p2.Equals(other.p2) ||
                   p1.Equals(other.p2) && p2.Equals(other.p1);
        }

        public override int GetHashCode()
        {
            return p1.GetHashCode() ^ p2.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", x1, y1, x2, y2);
        }
    }

    public int numTimes(string[] segments, int n)
    {
        var segs = ParseSegments(segments);
        segs = BuildGraph(segs);

        // Calculate the numer of connected components
        var nodes = segs.Select(x => x.p1).Union(segs.Select(x => x.p2)).Distinct().ToList();
        var qu = new UnionFind(nodes.Count);
        foreach (var seg in segs)
        {
            qu.Connect(nodes.IndexOf(seg.p1), nodes.IndexOf(seg.p2));
        }

        int cc = qu.CC();

        // Groups segments in components
        var components = new Dictionary<int, List<Segment>>();

        foreach (var seg in segs)
        {
            int root = qu.Root(nodes.IndexOf(seg.p1));

            if (!components.ContainsKey(root))
                components[root] = new List<Segment> { seg };
            else
                components[root].Add(seg);
        }

        // Calculate degrees
        int result = cc - 1;

        foreach (var component in components)
        {
            var degrees = Degrees(n, nodes, component.Value);

            var odd = degrees.Count(x => x.Value % 2 == 1);

            if (odd >= 2)
            {
                result += (odd - 2) / 2;
            }            
        }

        return result;
    }

    private static List<Segment> ParseSegments(string[] segments)
    {
        var segs = new List<Segment>();

        foreach (var segment in segments)
        {
            var splits = segment.Split();
            var seg = new Segment(
                new Point(int.Parse(splits[0]), int.Parse(splits[1])),
                new Point(int.Parse(splits[2]), int.Parse(splits[3])));

            segs.Add(seg);
        }
        return segs;
    }

    private List<Segment> BuildGraph(List<Segment> segs)
    {
        bool intersactionFound;

        do
        {
            //Draw(segs, id++);

            intersactionFound = false;

            for (int i = 0; i < segs.Count; i++)
                for (int j = i + 1; j < segs.Count; j++)
                {
                    var seg1 = segs[i];
                    var seg2 = segs[j];

                    if (seg1.x1 == seg1.x2 && seg2.x1 == seg2.x2 && seg1.x1 == seg2.x1)
                    {
                        var points = new[] {seg1.p1, seg1.p2, seg2.p1, seg2.p2}.OrderBy(x => x.y).Distinct().ToList();

                        if (In(seg1.y1, seg2.y1, seg2.y2) || In(seg1.y2, seg2.y1, seg2.y2) ||
                            In(seg2.y1, seg1.y1, seg1.y2) || In(seg2.y2, seg1.y1, seg1.y2))
                        {
                            segs.Remove(seg1);
                            segs.Remove(seg2);

                            for (int k = 0; k < points.Count - 1; k++)
                            {
                                segs.Add(new Segment(points[k], points[k + 1]));
                            }

                            intersactionFound = true;
                            goto end;
                        }
                    }

                    if (seg1.y1 == seg1.y2 && seg2.y1 == seg2.y2 && seg1.y1 == seg2.y1)
                    {
                        var points = new[] {seg1.p1, seg1.p2, seg2.p1, seg2.p2}.OrderBy(x => x.x).Distinct().ToList();

                        if (In(seg1.x1, seg2.x1, seg2.x2) || In(seg1.x2, seg2.x1, seg2.x2) ||
                            In(seg2.x1, seg1.x1, seg1.x2) || In(seg2.x2, seg1.x1, seg1.x2))
                        {
                            segs.Remove(seg1);
                            segs.Remove(seg2);

                            for (int k = 0; k < points.Count - 1; k++)
                            {
                                segs.Add(new Segment(points[k], points[k + 1]));
                            }

                            intersactionFound = true;
                            goto end;
                        }
                    }

                    var p = Intersection(seg1, seg2);

                    if (p != null)
                    {
                        var seg11 = new Segment(seg1.p1, p);
                        var seg12 = new Segment(seg1.p2, p);
                        var seg21 = new Segment(seg2.p1, p);
                        var seg22 = new Segment(seg2.p2, p);

                        segs.Remove(seg1);
                        segs.Remove(seg2);

                        if (!seg11.p1.Equals(seg11.p2)) segs.Add(seg11);
                        if (!seg12.p1.Equals(seg12.p2)) segs.Add(seg12);
                        if (!seg21.p1.Equals(seg21.p2)) segs.Add(seg21);
                        if (!seg22.p1.Equals(seg22.p2)) segs.Add(seg22);

                        segs = segs.Distinct().ToList();

                        intersactionFound = true;
                        goto end;
                    }
                }

            end:
            ;
        } 
        while (intersactionFound);
        
        return segs;
    }

    private static Dictionary<Point, int> Degrees(int n, List<Point> nodes, List<Segment> segs)
    {
        var degrees = new Dictionary<Point, int>();
        foreach (var point in nodes)
        {
            degrees[point] = 0;

            foreach (var seg in segs)
            {
                if (Equals(point, seg.p1) || Equals(point, seg.p2)) degrees[point]++;
            }

            degrees[point] *= n;
        }
        return degrees;
    }
    
    private Point Intersection(Segment seg1, Segment seg2)
    {
        var p = IntersectionTo(seg1, seg2);
        
        return p ?? IntersectionTo(seg2, seg1);
    }

    private Point IntersectionTo(Segment seg1, Segment seg2)
    {
        if (seg1.x1 == seg1.x2 && seg2.y1 == seg2.y2)
        {
            var p = new Point(seg1.x1, seg2.y1);

            if (InInclusive(p.x, seg2.x1, seg2.x2) && InInclusive(p.y, seg1.y1, seg1.y2))
            {
                int count = 0;
                if (p.Equals(seg1.p1)) count++;
                if (p.Equals(seg1.p2)) count++;
                if (p.Equals(seg2.p1)) count++;
                if (p.Equals(seg2.p2)) count++;

                if (count < 2)
                {
                    return p;
                }
            }
        }

        return null;
    }

    private bool InInclusive(int x, int x1, int x2)
    {
        int min = Math.Min(x1, x2);
        int max = Math.Max(x1, x2);

        return min <= x && x <= max;
    }

    private bool In(int x, int x1, int x2)
    {
        int min = Math.Min(x1, x2);
        int max = Math.Max(x1, x2);

        return min < x && x < max;
    }

    public class UnionFind
    {
        private readonly int[] parent;

        public UnionFind(int n) { parent = Enumerable.Range(0, n).ToArray(); }

        public bool AreConnected(int p, int q) { return Root(p) == Root(q); }

        public void Connect(int p, int q)
        {
            var rootP = Root(p); var rootQ = Root(q); if (rootP == rootQ) return; parent[rootP] = Root(rootQ);
        }

        public int CC()
        {
            var roots = new HashSet<int>();
            for (int i = 0; i < parent.Length; i++)
            {
                roots.Add(Root(i));
            }
            return roots.Count;
        }

        public int Root(int p)
        {
            while (parent[p] != p) { parent[p] = parent[parent[p]]; p = parent[p]; } return p;
        }
    }
    
    private long P(int n) { long result = 1; for (var i = 1; i <= n; i++) result *= i; return result; }
    private long D(int n, int k) { long result = 1; for (var i = 0; i < k; i++) result *= n--; return result; }
    private long Dr(int n, int k) { long result = 1; for (var i = 0; i < k; i++) result *= n; return result; }
    private long C(int n, int k) { return D(n, k) / P(k); }
    private long Cr(int n, int k) { return C(n + k - 1, k); }


// BEGIN CUT HERE

    private void Draw(List<Segment> segs, int id = 0)
    {
        var n = 1000;
        var center = 500;
        var bitmap = new Bitmap(n, n);
        var unit = 50;
        var g = Graphics.FromImage(bitmap);

        Console.Clear();

        g.DrawRectangle(Pens.Black, 0, 0, n - 1, n - 1);
        foreach (var seg in segs)
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", seg.x1, seg.y1, seg.x2, seg.y2);

            g.DrawLine(
                Pens.Black,
                center + unit * seg.x1,
                center + unit * seg.y1,
                center + unit * seg.x2,
                center + unit * seg.y2);

            g.DrawPie(Pens.Blue, center + unit * seg.x1 - 2, center + unit * seg.y1 - 2, 4, 4, 0, 360);
            g.DrawPie(Pens.Red, center + unit * seg.x2 - 2, center + unit * seg.y2 - 2, 4, 4, 0, 360);
        }

        g.Save();

        bitmap.Save(string.Format(@"c:\temp\segments{0}.jpeg", id));
    }

    public static void Main(String[] args) {
        try  {
            eq(0, (new PenLift()).numTimes(new string[] { "-10 0 10 0", "0 -10 0 10" }, 1), 1);
            eq(1, (new PenLift()).numTimes(new string[] { "-10 0 0 0", "0 0 10 0", "0 -10 0 0", "0 0 0 10" }, 1), 1);
            eq(2, (new PenLift()).numTimes(new string[] { "-10 0 0 0", "0 0 10 0", "0 -10 0 0", "0 0 0 10" }, 4), 0);
            eq(3, (new PenLift()).numTimes(new string[] {"0 0 1 0",   "2 0 4 0",   "5 0 8 0",   "9 0 13 0",
                "0 1 1 1",   "2 1 4 1",   "5 1 8 1",   "9 1 13 1",
                "0 0 0 1",   "1 0 1 1",   "2 0 2 1",   "3 0 3 1",
                "4 0 4 1",   "5 0 5 1",   "6 0 6 1",   "7 0 7 1",
                "8 0 8 1",   "9 0 9 1",   "10 0 10 1", "11 0 11 1",
                "12 0 12 1", "13 0 13 1"}, 1), 6);
            eq(4,(new PenLift()).numTimes(new string[] {"-2 6 -2 1",  "2 6 2 1",  "6 -2 1 -2",  "6 2 1 2",
                "-2 5 -2 -1", "2 5 2 -1", "5 -2 -1 -2", "5 2 -1 2",
                "-2 1 -2 -5", "2 1 2 -5", "1 -2 -5 -2", "1 2 -5 2",
                "-2 -1 -2 -6","2 -1 2 -6","-1 -2 -6 -2","-1 2 -6 2"}, 5),3);
            eq(5,(new PenLift()).numTimes(new string[] {"-252927 -1000000 -252927 549481","628981 580961 -971965 580961",
               "159038 -171934 159038 -420875","159038 923907 159038 418077",
               "1000000 1000000 -909294 1000000","1000000 -420875 1000000 66849",
               "1000000 -171934 628981 -171934","411096 66849 411096 -420875",
               "-1000000 -420875 -396104 -420875","1000000 1000000 159038 1000000",
               "411096 66849 411096 521448","-971965 580961 -909294 580961",
               "159038 66849 159038 -1000000","-971965 1000000 725240 1000000",
               "-396104 -420875 -396104 -171934","-909294 521448 628981 521448",
               "-909294 1000000 -909294 -1000000","628981 1000000 -909294 1000000",
               "628981 418077 -396104 418077","-971965 -420875 159038 -420875",
               "1000000 -1000000 -396104 -1000000","-971965 66849 159038 66849",
               "-909294 418077 1000000 418077","-909294 418077 411096 418077",
               "725240 521448 725240 418077","-252927 -1000000 -1000000 -1000000",
               "411096 549481 -1000000 549481","628981 -171934 628981 923907",
               "-1000000 66849 -1000000 521448","-396104 66849 -396104 1000000",
               "628981 -1000000 628981 521448","-971965 521448 -396104 521448",
               "-1000000 418077 1000000 418077","-1000000 521448 -252927 521448",
               "725240 -420875 725240 -1000000","-1000000 549481 -1000000 -420875",
               "159038 521448 -396104 521448","-1000000 521448 -252927 521448",
               "628981 580961 628981 549481","628981 -1000000 628981 521448",
               "1000000 66849 1000000 -171934","-396104 66849 159038 66849",
               "1000000 66849 -396104 66849","628981 1000000 628981 521448",
               "-252927 923907 -252927 580961","1000000 549481 -971965 549481",
               "-909294 66849 628981 66849","-252927 418077 628981 418077",
               "159038 -171934 -909294 -171934","-252927 549481 159038 549481"}, 824759),19);
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
