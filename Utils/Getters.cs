using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda.Utils
{
    public class Getters
    {
        public static string[] GetArrayHorarios()
        {
            string[] horarios = new string[]
            {
            "08:00 às 09:00",
            "09:00 às 10:00",
            "10:00 às 11:00",
            "11:00 às 12:00",
            "12:00 às 13:00",
            "13:00 às 14:00",
            "14:00 às 15:00",
            "15:00 às 16:00",
            "16:00 às 17:00",
            "17:00 às 18:00",
            "18:00 às 19:00"
            };

            return horarios;
        }

        public static Label GetLabel(String titulo, Boolean isCabecalho)
        {
            Font fonte;
            if (isCabecalho)
            {
                fonte = new Font("Arial", 12, FontStyle.Bold);
            }
            else
            {
                fonte = new Font("Arial", 12);
            }

            return new Label()
            {
                Text = $"{titulo}",
                Font = fonte,
                TextAlign = ContentAlignment.MiddleCenter
            };
        }
    }
}
