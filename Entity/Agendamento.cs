using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Entity
{
    public class Agendamento : Entidade
    {
        public Cliente Cliente { get; set; }
        public Profissional Profissional { get; set; }
        public DateTime Data { get; set; }
        public string Hora { get; set; }
        public Agendamento() { }
        public Agendamento(Cliente cliente, Profissional profissional, DateTime data, string hora)
        {
            Cliente = cliente;
            Profissional = profissional;
            Data = data;
            Hora = hora;
        } 
    }
}
