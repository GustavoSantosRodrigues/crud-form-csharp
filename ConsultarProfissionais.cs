using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agenda.DAO;
using Agenda.Entity;
using Agenda.Enum;
using Agenda.Utils;

namespace Agenda
{
    public partial class ConsultarProfissionais : Form
    {
        public ConsultarProfissionais()
        {
            InitializeComponent();

            ConstruirTabela();
        }

        private void ConstruirTabela()
        {
            ProfissionalDAO profissionalDAO = new ProfissionalDAO();
            List<Entidade> profissionais = profissionalDAO.BuscarTodos();

            panel1.Controls.Clear();

            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.AutoScroll = true;

            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Id", true), 0, 0);
            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Nome", true), 1, 0);
            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Profissao", true), 2, 0);
            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Dias de Atendimento", true), 3, 0);
            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Horários de Atendimento", true), 4, 0);

            foreach (var entidade in profissionais)
            {
                int line = profissionais.IndexOf(entidade) + 1;

                Profissional profissional = (Profissional)entidade;

                tableLayoutPanel1.Controls.Add(Getters.GetLabel(profissional.Id.ToString(), false), 0, line);
                tableLayoutPanel1.Controls.Add(Getters.GetLabel(profissional.Nome, false), 1, line);
                tableLayoutPanel1.Controls.Add(Getters.GetLabel(profissional.Profissao.OptionString(), false), 2, line);
                tableLayoutPanel1.Controls.Add(Getters.GetLabel(profissional.Dias.OptionString(), false), 0, line);
                tableLayoutPanel1.Controls.Add(Getters.GetLabel(profissional.Horario.OptionString(), false), 1, line);
            }

            panel1.Controls.Add(tableLayoutPanel1);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
