using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Enum
{
    public static class Extensions
    {
        public static string OptionString(this Profissao profissao)
        {
            if (Profissao.CABELEREIRO == profissao)
            {
                return "Cabelereiro";
            }
            else if (Profissao.MAQUIADOR == profissao)
            {
                return "Maquiador";
            }
            else if (Profissao.MANICURE == profissao)
            {
                return "Manicure";
            }
            else if (Profissao.DESIGNER_SOMBRANCELHA == profissao)
            {
                return "Designer de Sombrancelha";
            }
            else
            {
                return "Visagista";
            }
        }
        public static string OptionString(this HorarioTrabalho horario)
        {
            if(HorarioTrabalho.INICIO_08 == horario)
            {
                return "08:00 às 17:00";
            }
            else if(HorarioTrabalho.INICIO_09 == horario)
            {
                return "09:00 às 18:00";
            }
            else
            {
                return "10:00 às 19:00";
            }
        }
        public static string OptionString(this DiasTrabalho dias)
        {
            if (DiasTrabalho.SEG_SEX == dias)
            {
                return "Segunda à Sexta";
            }
            else if (DiasTrabalho.SAB_DOM == dias)
            {
                return "Sabado e Domingo";
            }
            else
            {
                return "Todos os dias";
            }
        }
    }
    public enum Profissao
    {
        CABELEREIRO = 1,
        MAQUIADOR = 2,
        MANICURE = 3,
        DESIGNER_SOMBRANCELHA = 4,
        VISAGISTA = 5,
    }
    public enum HorarioTrabalho
    {
        INICIO_08 = 1,
        INICIO_09 = 2,
        INICIO_10 = 3,
    }
    public enum DiasTrabalho
    {
        SEG_SEX = 1,
        SAB_DOM = 2,
        TODOS_DIAS = 3,
    }
}
