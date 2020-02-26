using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocThuMuc
{
    class Program
    {
        static void Main(string[] args)
        {
            Handle han = new Handle();
            han.PhanLoaiBa();
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
