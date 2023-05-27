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
                return "Option 1";
            }
            else if (Profissao.DESIGNER_SOMBRANCELHA == profissao)
            {
                return "Option 2";
            }
            else
            {
                return "Option 3";
            }
        }
        public static string OptionString(this HorarioTrabalho horario)
        {
            if(HorarioTrabalho.INICIO_08 == horario)
            {
                return "Option 1";
            }
            else if(HorarioTrabalho.INICIO_09 == horario)
            {
                return "Option 2";
            }
            else
            {
                return "Option 3";
            }
        }
        public static string OptionString(this DiasTrabalho dias)
        {
            if (DiasTrabalho.SEG_SEX == dias)
            {
                return "Option 1";
            }
            else if (DiasTrabalho.SAB_DOM == dias)
            {
                return "Option 2";
            }
            else
            {
                return "Option 3";
            }
        }
    }
    public enum Profissao
    {
        CABELEREIRO = 1,
        DESIGNER_SOMBRANCELHA = 2,
        MANICURE = 3,
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
