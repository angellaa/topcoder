using System;
using System.Text;
using System.Text.RegularExpressions;

public class MagicalStringDiv1 
{
    /// <summary>
    /// Hurry does not help!
    /// Don't assume that an easy problem does not have particular cases
    /// Always test index conditions before accessing array elements in a short circuit if
    /// Pay extreme attention when using > or >=, justify why and test it!!!
    /// </summary>
    public int getLongest(string S)
    {
        int i = 0;
        int j = S.Length - 1;
        int removes = 0;

        while (i <= j)
        {
            while (i <= j && S[i] == '<') { i++; removes++; }
            while (j >= i && S[j] == '>') { j--; removes++; }
            ++i;
            --j;
        }

        return S.Length - removes;
    }
    
    // BEGIN CUT HERE

    public static void Test(String[] args) 
    {
        try  
        {
            eq(0,(new MagicalStringDiv1()).getLongest("<><><<>"),4);
            eq(1,(new MagicalStringDiv1()).getLongest(">>><<<"),6);
            eq(2,(new MagicalStringDiv1()).getLongest("<<<>>>"),0);
            eq(3,(new MagicalStringDiv1()).getLongest("<<<<><>>><>>><>><>><>>><<<<>><>>>>><<>>>>><><<<<>>"),24);
            eq(4, (new MagicalStringDiv1()).getLongest("<<"), 0);
            eq(5, (new MagicalStringDiv1()).getLongest("<<><<<"), 2);
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
