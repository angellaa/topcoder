using System;
using System.Linq;

public class BigBurger 
{
    public int maxWait(int[] a, int[] s)
    {
        int n = a.Length;
        var t = new int[n];

        t[0] = a[0];

        for (int i = 1; i < n; i++)
        {
            var nt = t[i - 1] + s[i - 1];
            t[i] = nt > a[i] ? nt : a[i];
        }

        return t.Select((_, i) => t[i] - a[i]).Max();
    }

    
    // BEGIN CUT HERE

    public static void Test(String[] args) 
    {
        try  
        {
                        eq(0,(new BigBurger()).maxWait(new int[] {3,3,9}, new int[] {2,15,14}),11);
            eq(1,(new BigBurger()).maxWait(new int[] {182}, new int[] {11}),0);
            eq(2,(new BigBurger()).maxWait(new int[] {2,10,11}, new int[] {3,4,3}),3);
            eq(3,(new BigBurger()).maxWait(new int[] {2,10,12}, new int[] {15,1,15}),7);
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
