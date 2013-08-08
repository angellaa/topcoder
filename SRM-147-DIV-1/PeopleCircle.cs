using System;

public class PeopleCircle 
{
    public string order(int m, int f, int k)
    {
        var res = new string('M', m + f).ToCharArray();

        int p = -1;
        for (int i = 0; i < f; i++)
        {
            for (int j = 0; j < k; j++)
            {
                p = (p + 1) % (m + f);
                if (res[p] == 'F') j--;
            }

            res[p] = 'F';
        }

        return new string(res);
    }

    
    // BEGIN CUT HERE

    public static void Test(String[] args) 
    {
        try  
        {
            eq(0,(new PeopleCircle()).order(5, 3, 2),"MFMFMFMM");
            eq(1,(new PeopleCircle()).order(7, 3, 1),"FFFMMMMMMM");
            eq(2,(new PeopleCircle()).order(25, 25, 1000),"MMMMMFFFFFFMFMFMMMFFMFFFFFFFFFMMMMMMMFFMFMMMFMFMMF");
            eq(3,(new PeopleCircle()).order(5, 5, 3),"MFFMMFFMFM");
            eq(4,(new PeopleCircle()).order(1, 0, 245),"M");
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
