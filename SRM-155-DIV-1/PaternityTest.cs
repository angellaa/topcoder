using System;
using System.Linq;

/// <summary>
/// Interesting the use of Enumerable.Range(0, n).Where to get all the indexes 
/// of an array that satisfy a particular predicate.
/// </summary>
public class PaternityTest 
{
    public int[] possibleFathers(string child, string mother, string[] men)
    {
        int n = child.Length;

        var motherMatch = Enumerable.Range(0, n).Where(i => child[i] == mother[i]).ToList();

        var result = Enumerable.Range(0, men.Length).Where(j =>
        {
            var man = men[j];
            var manMatch = Enumerable.Range(0, n).Where(i => child[i] == man[i]).ToList();

            return manMatch.Count >= n / 2 && (manMatch.Union(motherMatch).Distinct().Count() == n);
        });

        return result.ToArray();
    }

    
    // BEGIN CUT HERE

    public static void Test(String[] args) 
    {
        try  
        {
            eq(0,(new PaternityTest()).possibleFathers("ABCD", "AXCY", new string[] { "SBTD", "QRCD" }),new int[] { 0 });
            eq(1,(new PaternityTest()).possibleFathers("ABCD", "ABCX", new string[] { "ABCY", "ASTD", "QBCD" } ),new int[] { 1,  2 });
            eq(2,(new PaternityTest()).possibleFathers("ABABAB", "ABABAB", new string[] { "ABABAB", "ABABCC", "ABCCDD", "CCDDEE" }),new int[] { 0,  1 });
            eq(3,(new PaternityTest()).possibleFathers("YZGLSYQT", "YUQRWYQT", new string[] {"YZQLDPWT", "BZELSWQM", "OZGPSFKT", "GZTKFYQT", "WQJLSMQT"}),new int[] { });
            eq(4,(new PaternityTest()).possibleFathers("WXETPYCHUWSQEMKKYNVP", "AXQTUQVAUOSQEEKCYNVP", new string[] { "WNELPYCHXWXPCMNKDDXD",
                 "WFEEPYCHFWDNPMKKALIW",
                 "WSEFPYCHEWEFGMPKIQCK",
                 "WAEXPYCHAWEQXMSKYARN",
                 "WKEXPYCHYWLLFMGKKFBB" }),new int[] { 1,  3 });
        } 
        catch(Exception exx)  
        {
            Console.WriteLine(exx);
            Console.WriteLine(exx.StackTrace);
        }
        Console.ReadKey();
    }

    private static void eq(int n, object have, object need) 
    {
        if(eq(have, need)) 
        {
            Console.WriteLine("Case " + n + " passed.");
        } 
        else 
        {
            Console.Write("Case " + n + " failed: expected ");
            print(need);
            Console.Write(", received ");
            print(have);
            Console.WriteLine();
        }
    }

    private static void eq(int n, Array have, Array need) 
    {
        if(have == null || have.Length != need.Length) 
        {
            Console.WriteLine("Case " + n + " failed: returned " + have.Length + " elements; expected " + need.Length + " elements.");
            print(have);
            print(need);
            return;
        }

        for(int i = 0; i < have.Length; i++) 
        {
            if(!eq(have.GetValue(i), need.GetValue(i))) 
            {
                Console.WriteLine("Case " + n + " failed. Expected and returned array differ in position " + i);
                print(have);
                print(need);
                return;
            }
        }

        Console.WriteLine("Case " + n + " passed.");
    }

    private static bool eq(object a, object b) 
    {
        if (a is double && b is double) 
        {
            return Math.Abs((double)a - (double)b) < 1E-9;
        } 
        else
        {
            return a != null && b != null && a.Equals(b);
        }
    }

    private static void print(object a) 
    {
        if (a is string)
        {
            Console.Write("\"{0}\"", a);
        } 
        else if (a is long) 
        {
            Console.Write("{0}L", a);
        } 
        else 
        {
            Console.Write(a);
        }
    }

    private static void print( Array a ) 
    {
        if (a == null) 
        {
            Console.WriteLine("<NULL>");
        }

        Console.Write('{');
        
        for (int i = 0; i < a.Length; i++ ) 
        {
            print(a.GetValue(i));

            if( i != a.Length - 1) 
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine( '}' );
    }

    // END CUT HERE
}
