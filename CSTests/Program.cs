using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var squared = new List<int> { 1, 2, 3 }.Select(Square);
        }

        static int Square(int x) => x * x;
    }
}
