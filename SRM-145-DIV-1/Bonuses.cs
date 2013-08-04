using System;
using System.Linq;

/// <summary>
/// Learning Notes:
///  - Wasted a lot of time to solve the bonus problem. Turns out that I misunderstood the problem.
///    I used the percenteges insted of the points to get the top scores.
///    For this reason I used a separate bonus array that was not required.
///  
/// Lesson learnt:
///  - Read the problem statement and pay attention to the elements in bold. points was in bold!
///    "give it to the employees that come first in *points*."
///  - Remember that you can change the input array instead of creating a support array
/// 
/// </summary>
public class Bonuses 
{
    public int[] getDivision(int[] points)
    {
        var n = points.Length;
        var sum = points.Sum();
        var v = new int[n];

        for (int i = 0; i < n; i++)
        {
            v[i] = points[i] * 100 / sum;
        }

        int diff = 100 - v.Sum();

        while (diff > 0)
        {
            var max = points.Max();
            var maxi = Array.IndexOf(points, max);
            points[maxi] = -1;
            v[maxi]++;
            diff--;
        }

        return v;
    }

    
    // BEGIN CUT HERE

    public static void Test(String[] args) 
    {
        try  
        {
            eq(0,(new Bonuses()).getDivision(new int[] {1,2,3,4,5}),new int[] { 6,  13,  20,  27,  34 });
            eq(1,(new Bonuses()).getDivision(new int[] {5,5,5,5,5,5}),new int[] { 17,  17,  17,  17,  16,  16 });
            eq(2,(new Bonuses()).getDivision(new int[] {485, 324, 263, 143, 470, 292, 304, 188, 100, 254, 296,
                255, 360, 231, 311, 275,  93, 463, 115, 366, 197, 470}),new int[] { 8,  6,  4,  2,  8,  5,  5,  3,  1,  4,  5,  4,  6,  3,  5,  4,  1,  8,  1,  6,  3,  8 });
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
