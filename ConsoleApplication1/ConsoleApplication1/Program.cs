using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> outcomeProb = new List<double>();
            Console.WriteLine(Convert.ToInt16(outcomeProb.Sum()));

            if (Convert.ToInt16(outcomeProb.Sum()) == 0)
            {
                Console.WriteLine("BEEEEP");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
