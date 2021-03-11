using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WiproTeste.Dominio.Models;
using WiproTeste.Models;

namespace Servico
{
    public class Arquivo
    {

        public void LerArquivo()
        {
            var pathFiles = @"C:\Labs\Files";
            var strTeste = "teste.csv";
            var fileDadosCotacao = "DadosCotacao.csv";

            var pathFile = Path.Combine(pathFiles, fileDadosCotacao);

            var lines = File.ReadAllLines(pathFile)
                .Skip(1)
                .Select(x => x.Split(';'))
                .Select(e => new DadosCotacao
                {
                    Valor = double.Parse(e[0]),
                    Codigo = int.Parse(e[1]),
                    DataCotacao = DateTime.Parse(e[2])
                }).ToArray();
        }
    }
}
