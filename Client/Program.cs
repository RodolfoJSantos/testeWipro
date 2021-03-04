using System;
using System.IO;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader leitor = new StreamReader("DadosMoeda.csv");

            string line;
            while ((line = leitor.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }

            Console.ReadLine();
        }
    }
}
