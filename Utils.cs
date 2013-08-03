using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TopCoder
{
    /// <summary>
    /// Collection of useful methods that can be reused accross competitions.
    /// More info http://andrea-angella.blogspot.co.uk/2013/07/configuring-topcoder-arena-for-c.html
    /// </summary>
    [TestFixture]
    public class Utils
    {
        // BEGIN CUT HERE

        private static int[] StringToIntArray(string s)
        {
            return s.Select(x => x - '0').ToArray();
        }

        private static string IntArrayToString(IEnumerable<int> v)
        {
            return new string(v.Select(x => (char)(x + '0')).ToArray());
        }

        // END CUT HERE

        [Test]
        public static void StringToIntArrayTest()
        {
            Assert.AreEqual(new int[0], StringToIntArray(""));
            Assert.AreEqual(new [] { 5 }, StringToIntArray("5"));
            Assert.AreEqual(new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, StringToIntArray("0123456789"));
        }

        [Test]
        public static void IntArrayToStringTest()
        {
            Assert.AreEqual("", IntArrayToString(new int[0]));
            Assert.AreEqual("5", IntArrayToString(new[] { 5 }));
            Assert.AreEqual("0123456789", IntArrayToString(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
        }
    }
}
