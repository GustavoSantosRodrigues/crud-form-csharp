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
        Boolean Salvar(Entidade entidade);
        Boolean Alterar(Entidade entidade);
        void Excluir(int id);
        Entidade BuscarPorId(int id);
        List<Entidade> BuscarTodos();
    }
}
