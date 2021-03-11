using RabbitMQ.Client;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine(System.Environment.CurrentDirectory);
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            Console.WriteLine(Environment.CurrentDirectory);

            Console.ReadLine();
        }
    }
}
