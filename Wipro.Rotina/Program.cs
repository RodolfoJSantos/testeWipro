using System;
using Wipro.Servico;

namespace Wipro.Rotina
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Worker worker = new Worker();

            worker.GetItemFila();

            Console.ReadLine();
        }
    }
}
