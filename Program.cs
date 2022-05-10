using System;
using System.Collections.Generic;

namespace EFProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var ccontext = new SchoolContext();
            var services = new Services(ccontext);


            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
