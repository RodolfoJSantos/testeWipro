using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WiproTeste.Content
{
    public static class TesteArquivo
    {
        public static string GetDiretoryName()
        {
            var pasta = Path.GetPathRoot(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();

            return pasta;
        }
    }
}
