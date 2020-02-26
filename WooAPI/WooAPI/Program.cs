using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            WooComerceAPI woo = new WooComerceAPI();
            woo.Add();
            Console.ReadLine();
        }
    }
}
