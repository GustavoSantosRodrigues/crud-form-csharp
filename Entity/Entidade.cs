using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity
{
    public abstract class Entidade
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
