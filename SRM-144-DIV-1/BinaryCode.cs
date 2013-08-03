using System;
using System.Collections.Generic;
using System.Linq;

public class BinaryCode
{
    public string[] decode(string message)
    {
        return new[] { Decode(message, 0), Decode(message, 1) };
    }

    private string Decode(string message, int start)
    {
        int n = message.Length;
        var c = FromDigitString(message);

        var a = new int[n];
        a[0] = start;

        for (int i = 1; i < n; ++i)
        {
            a[i] = c[i - 1] - a[i - 1] - ((i > 1) ? a[i - 2] : 0);

            if (a[i] != 0 && a[i] != 1)
                return "NONE";
        }

        int lastTwo = a[n - 1] + (n > 1 ? a[n - 2] : 0);

        return c[n - 1] != lastTwo ? "NONE" : ToDigitString(a);
    }

    private int[] FromDigitString(string s)
    {
        return s.Select(x => x - '0').ToArray();
    }

    private string ToDigitString(IEnumerable<int> v)
    {
        return new string(v.Select(x => (char)(x + '0')).ToArray());
    }
    
    // BEGIN CUT HERE

    public static void Main(String[] args) 
    {
        try  
        {
                        eq(0,(new BinaryCode()).decode("123210122"),new string[] { "011100011",  "NONE" });
            eq(1,(new BinaryCode()).decode("11"),new string[] { "01",  "10" });
            eq(2,(new BinaryCode()).decode("22111"),new string[] { "NONE",  "11001" });
            eq(3,(new BinaryCode()).decode("123210120"),new string[] { "NONE",  "NONE" });
            eq(4,(new BinaryCode()).decode("3"),new string[] { "NONE",  "NONE" });
            eq(5,(new BinaryCode()).decode("12221112222221112221111111112221111"),new string[] { "01101001101101001101001001001101001",  "10110010110110010110010010010110010" });
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
