using System;

public class Inventory 
{
    // PROBLEMS
    //   - Spent a lot of time to fix the double precision problem.
    //   - The sentence derailed me doing the ceiling per month instead of only the last.
    //     "if the *expected* number of sales per month is not a whole number, 
    //     you should round up since it is probably better to have one too many 
    //     items than it is to have one too few"
    //
    public int monthlyOrder(int[] s, int[] d)
    {
        double sum = 0;
        int m = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == 0) continue;
            sum += (30.0 / d[i]) * s[i];
            m++;
        }

        return (int)Math.Ceiling(sum / m - 1e-9);
    }

    
    // BEGIN CUT HERE

    public static void Test(String[] args) 
    {
        try  
        {
            eq(0,(new Inventory()).monthlyOrder(new int[] {5}, new int[] {15}),10);
            eq(1,(new Inventory()).monthlyOrder(new int[] {75,120,0,93}, new int[] {24,30,0,30}),103);
            eq(2,(new Inventory()).monthlyOrder(new int[] {8773}, new int[] {16}),16450);
            eq(3,(new Inventory()).monthlyOrder(new int[] {1115,7264,3206,6868,7301}
               , new int[] {1,3,9,4,18}),36091);
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
