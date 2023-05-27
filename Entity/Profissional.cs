using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Entity;
using Agenda.Enum;

namespace Agenda
{
    public class Profissional : Entidade
    {
        public string Nome { get; set; }
        public Profissao Profissao { get; set; }
        public HorarioTrabalho Horario { get; set; }
        public DiasTrabalho Dias { get; set; }
        
        public Profissional() {}
        
        public Profissional(int id, string nome, Profissao profissao, HorarioTrabalho horario, DiasTrabalho dias)
        {
            Id = id;
            Nome = nome;
            Profissao = profissao;
            Horario = horario;
            Dias = dias;
        }
    }
}
