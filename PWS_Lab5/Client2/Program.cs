using Client2.ServiceReference1;
using System;
using WCF;

namespace Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client service1Client = new Service1Client();

            var sumResult = service1Client.Sum(
                new A { s = "wor", k = 2, f = 1.1f },
                new A { s = "ld", k = 3, f = 1.4f }
                );

            Console.WriteLine($"======  SUM  ======");
            Console.WriteLine($"\ns = {sumResult.s}\nf = {sumResult.f}\nk = {sumResult.k}\n\n");
            Console.WriteLine($"======  ADD  =======");
            Console.WriteLine($"\n{sumResult.k} + 10 = " + service1Client.Add(sumResult.k, 10) + "\n\n");
            Console.WriteLine($"=====  CONCAT  =====");
            Console.WriteLine($"\n{sumResult.s} + {sumResult.f} = " + service1Client.Concat(sumResult.s, sumResult.f) + "\n\n");

            service1Client.Close();
            
            Console.ReadKey();
        }
    }
}
