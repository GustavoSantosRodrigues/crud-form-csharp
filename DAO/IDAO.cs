using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Entity;

namespace Agenda.DAO
{
    public interface IDAO
    {
        Entidade Salvar(Entidade entidade);
        Entidade Alterar(Entidade entidade);
        void Excluir(int id);
        Entidade BuscarPorId(int id);
        List<Entidade> BuscarTodos();
    }
}
