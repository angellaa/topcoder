using System;
using System.Linq;

public class PackingBallsDiv1 
{
    /// <summary>
    /// Pay attention when you use a eager algorithm (for subproblems you can't)
    /// Prefer Array.Sort respect of using OrderBy
    /// </summary>
    public int minPacks(int K, int A, int B, int C, int D)   
    {
        var X = new long[K];

        X[0] = A;
        for (int i = 1; i < K; ++i)
            X[i] = (X[i - 1] * B + C) % D + 1;
       
        // eager approach
        long sum = X.Sum(x => x / K);
        X = X.Select(x => x % K).ToArray();

        Array.Sort(X);

        // consider all remaining possibilities (non eager)
        long res = int.MaxValue;
        for (int i = 0; i < K; i++)
            res = Math.Min(res, X[i] + (K - i - 1));

        return (int)(sum + res);
    }
    
    // BEGIN CUT HERE

    public static void Test(String[] args) 
    {
        try  
        {
            eq(0,(new PackingBallsDiv1()).minPacks(3, 4, 2, 5, 6),4);
            eq(1,(new PackingBallsDiv1()).minPacks(1, 58, 23, 39, 93),58);
            eq(2,(new PackingBallsDiv1()).minPacks(23, 10988, 5573, 4384, 100007),47743);
            eq(3,(new PackingBallsDiv1()).minPacks(100000, 123456789, 234567890, 345678901, 1000000000),331988732);
            
            // failed system tests
            eq(4,(new PackingBallsDiv1()).minPacks(14, 104563326, 668440963, 226153708, 423439112), 177443175);
            eq(5,(new PackingBallsDiv1()).minPacks(7, 1, 2, 3, 10), 6);
            
            // execution time!
            eq(6, (new PackingBallsDiv1()).minPacks(100000, 1, 1, 1, 1000000000), 149999);
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
