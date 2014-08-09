// BEGIN CUT HERE

// END CUT HERE
using System;
using System.Collections.Generic;
using System.Linq;

public class Lottery 
{
    public string[] sortByOdds(string[] rules) 
    {
        var res = new List<Tuple<string, long>>();

        foreach (var rule in rules)
        {
            var name = rule.Split(':')[0];
            var splits = rule.Split(':')[1].Trim().Split(' ');

            var choices = int.Parse(splits[0]);
            var blanks = int.Parse(splits[1]);
            var sorted = splits[2] == "T";
            var unique = splits[3] == "T";

            long comb = 0;

            if (!sorted && !unique) comb = Dr(choices, blanks);
            if ( sorted && !unique) comb = Cr(choices, blanks);
            if (!sorted &&  unique) comb = D(choices, blanks);
            if ( sorted &&  unique) comb = C(choices, blanks);

            res.Add(new Tuple<string, long>(name, comb));
        }

        return res.OrderBy(x => x.Item2).ThenBy(x => x.Item1).Select(x => x.Item1).ToArray();
    }

    private long P(int n) { long result = 1; for (var i = 1; i <= n; i++) result *= i; return result; }
    private long D(int n, int k) { long result = 1; for (var i = 0; i < k; i++) result *= n--; return result; }
    private long Dr(int n, int k) { long result = 1; for (var i = 0; i < k; i++) result *= n; return result; }
    private long C(int n, int k) { return D(n, k) / P(k); }
    private long Cr(int n, int k) { return C(n + k - 1, k); }

// BEGIN CUT HERE
    public static void Main(String[] args) {
        try  {
            eq(0,(new Lottery()).sortByOdds(new string[] {"PICK ANY TWO: 10 2 F F"
               ,"PICK TWO IN ORDER: 10 2 T F"
               ,"PICK TWO DIFFERENT: 10 2 F T"
               ,"PICK TWO LIMITED: 10 2 T T"}),new string[] { "PICK TWO LIMITED",  "PICK TWO IN ORDER",  "PICK TWO DIFFERENT",  "PICK ANY TWO" });
            eq(1,(new Lottery()).sortByOdds(new string[] {"INDIGO: 93 8 T F",
                "ORANGE: 29 8 F T",
                "VIOLET: 76 6 F F",
                "BLUE: 100 8 T T",
                "RED: 99 8 T T",
                "GREEN: 78 6 F T",
                "YELLOW: 75 6 F F"}
               ),new string[] { "RED",  "ORANGE",  "YELLOW",  "GREEN",  "BLUE",  "INDIGO",  "VIOLET" });
            eq(2,(new Lottery()).sortByOdds(new string[] {}),new string[] { });
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
