using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiproTeste.Repository.Interfaces;

namespace WiproTeste.Repository
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        public string key = "id_e_";
        private IConfiguration _configuracoes;
        private string _conexao { get { return _configuracoes.GetConnectionString("RedisServer"); } }
        private RedisClient _cache;

        public Repositorio(IConfiguration configuracoes, RedisClient cache)
        {
            _configuracoes = configuracoes;
            _cache = cache;
        }


        public int Atualizar(T obj)
        {
            throw new NotImplementedException();
        }

        public int Excluir(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Listar()
        {
            throw new NotImplementedException();
        }

        public T ObterPorId(string id)
        {
            throw new NotImplementedException();
        }

        public int Persistir(T obj)
        {
            throw new NotImplementedException();
        }

        public T ObterPorUltimo(string id)
        {
            throw new NotImplementedException();
        }
    }
}
