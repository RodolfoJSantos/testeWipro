using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiproTeste.Repository.Interfaces
{
    public interface IRepositorio<T> where T : class
    {
        int Excluir(string id);
        IEnumerable<T> Listar();
        T ObterPorUltimo(string id);
        int Persistir(T obj);
        int Atualizar(T obj);
    }
}
