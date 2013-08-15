using System;
using System.Linq;

public class InterestingDigits 
{
    public int[] digits(int b)
    {
        return Enumerable.Range(2, b).Where(x => interesting(b, x)).ToArray();
    }

    private bool interesting(int b, int x)
    {
        int i = x;

        for(;;)
        {
            var n = i;
            int s = 0;

            for (int j = 0; j < 3; j++)
            {
                s += n % b;
                n = n / b;
            }

            if (n > 0)
                return true;

            if (s % x != 0)
                return false;

            i += x;
        }
    }

    // BEGIN CUT HERE

    public static void Test(String[] args) 
    {
        try  
        {
            eq(0,(new InterestingDigits()).digits(10),new int[] { 3,  9 });
            eq(1,(new InterestingDigits()).digits(3),new int[] { 2 });
            eq(2,(new InterestingDigits()).digits(9),new int[] { 2,  4,  8 });
            eq(3,(new InterestingDigits()).digits(26),new int[] { 5,  25 });
            eq(4,(new InterestingDigits()).digits(30),new int[] { 29 });
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
