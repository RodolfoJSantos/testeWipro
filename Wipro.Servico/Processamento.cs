using Dominio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WiproTeste.Dominio.Models;
using WiproTeste.Models;

namespace Wipro.Servico
{
    public class Processamento
    {
        public DadosMoeda[] ProcessarCadaMoeda(List<Moeda> lista)
        {
            var arrList = lista.ToArray();
            var moeda1 = arrList[0];
            var moeda2 = arrList[1];
            var moeda3 = arrList[2];

            var result = new List<DadosMoeda>();

            result.AddRange(ObterDadosMoeda()
             .Where(x => x.Id.Equals(moeda1.moeda)
             && x.DataRef <= moeda1.data_fim
             && x.DataRef >= moeda1.data_inicio)
             .Select(s => s).ToList());


            result.AddRange(ObterDadosMoeda()
               .Where(x => x.Id.Equals(moeda2.moeda)
               && x.DataRef <= moeda2.data_fim
               && x.DataRef >= moeda2.data_inicio)
               .Select(s => s).ToList());

            result.AddRange(ObterDadosMoeda()
               .Where(x => x.Id.Equals(moeda3.moeda)
               && x.DataRef <= moeda3.data_fim
               && x.DataRef >= moeda3.data_inicio)
               .Select(s => s).ToList());

            return result.ToArray();
        }

        public DadosMoeda[] ObterDadosMoeda()
        {
            var fileDadosMoeda = "DadosMoeda.csv";

            return File.ReadAllLines(fileDadosMoeda)
                .Skip(1)
                .Select(x => x.Split(';'))
                .Select(e => new DadosMoeda
                {
                    Id = e[0],
                    DataRef = DateTime.Parse(e[1])
                }).ToArray();
        }

        public BaseCotacao[] ObterBaseCotacao()
        {
            var strBaseCotacao = "BaseCotacao.csv";

            return File.ReadAllLines(strBaseCotacao)
                .Skip(1)
                .Select(x => x.Split(';'))
                .Select(e => new BaseCotacao
                {
                    Id = e[0],
                    Cod = int.Parse(e[1])
                }).ToArray();
        }

        public List<DadosCotacao> ObterDadosCotacao(List<Moeda> lista)
        {
            List<DadosCotacao> result;
            var tabela = ObterBaseCotacao();
            var fileDadosCotacao = "DadosCotacao.csv";

            var dados = File.ReadAllLines(fileDadosCotacao)
                .Skip(1)
                .Select(x => x.Split(';'))
                .Select(e => new DadosCotacao
                {
                    Valor = double.Parse(e[0]),
                    Codigo = int.Parse(e[1]),
                    DataCotacao = DateTime.Parse(e[2])
                }).ToList();

            return result = dados.Where(d => tabela.Select(t => t.Cod).Contains(d.Codigo)).ToList();
        }

        public void CriarArquivo(List<Moeda> lista)
        {
            var moedas = ProcessarCadaMoeda(lista);
            var dadosCotacaos = ObterDadosCotacao(lista);

            var dataInicio = DateTime.Now.ToString("HH:mm:ss.fff");

            var queryArquivo =
                (from linha in moedas
                 from linhaCot in dadosCotacaos
                 where linha.DataRef == linhaCot.DataCotacao
                 select new ArquivoResultado
                 {
                     Id = linha.Id,
                     DataRef = linha.DataRef,
                     Valor = linhaCot.Valor
                 }).ToArray();


            var dataFim = DateTime.Now.ToString("HH:mm:ss.fff");


            using (FileStream fs = File.Create("resultado.csv"))
            using (StreamWriter escritor = new StreamWriter(fs, Encoding.UTF8))
            {
                escritor.WriteLine("ID_MOEDA;DATA_REF;VLR_COTACAO");
                for (int i = 0; i < queryArquivo.Length; i++)
                {
                    escritor.WriteLine($"{queryArquivo[i].Id};{queryArquivo[i].DataRef};{queryArquivo[i].Valor}");
                }
            }

            Console.WriteLine($"Arquivo criado");
        }
    }
}
