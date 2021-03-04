using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TesteWipro.FilaService
{
    public class FilaService
    {
                                                                                    
        public void AddItemFila(string moeda)
        {
            var queue = new Queue<string>();


        }

        public void Leitor()
        {
            StreamReader leitor = new StreamReader("DadosMoeda.csv");

            string linha;
            while ((linha = leitor.ReadLine()) != null)
            {
                Console.WriteLine(linha);
            }
        }
    }
}
