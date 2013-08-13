using System;
using System.Linq;

public class CircleGame 
{
    public int cardsLeft(string deck)
    {
        var n = deck.Where(x => x != 'K')
                    .Select(x =>
                    {
                        if (x == 'A') return 1;
                        if (x == 'T') return 10;
                        if (x == 'J') return 11;
                        if (x == 'Q') return 12;
                        return x - '0';
                    }).ToList();

        bool change;

        do
        {
            change = false;

            for (var i = 0; i < n.Count; i++)
            {
                var j = (i + 1) % n.Count;
                
                if (n[i] + n[j] == 13)
                {
                    n[i] = 0; n[j] = 0; change = true;
                }
            }

            n = n.Where(x => x != 0).ToList();
        } 
        while (change);

        return n.Count; ;
    }

    
    // BEGIN CUT HERE

    public static void Test(String[] args) 
    {
        try  
        {
            eq(0,(new CircleGame()).cardsLeft("KKKKKKKKKK"),0);
            eq(1,(new CircleGame()).cardsLeft("KKKKKAQT23"),1);
            eq(2,(new CircleGame()).cardsLeft("KKKKATQ23J"),6);
            eq(3,(new CircleGame()).cardsLeft("AT68482AK6875QJ5K9573Q"),4);
            eq(4,(new CircleGame()).cardsLeft("AQK262362TKKAQ6262437892KTTJA332"),24);
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
