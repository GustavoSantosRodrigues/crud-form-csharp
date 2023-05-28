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
using Agenda.Utils;
using static System.Windows.Forms.LinkLabel;

namespace Agenda
{
    public partial class ConsultarAgenda : Form
    {
        public ConsultarAgenda()
        {
            InitializeComponent();
            ConstruirTabela();
        }

        private void ConstruirTabela()
        {
            AgendamentoDAO agendamentoDAO = new AgendamentoDAO();
            List<Entidade> agendamentos = agendamentoDAO.BuscarTodos();

            panel1.Controls.Clear();

            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.AutoScroll = true;

            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));

            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Id", true), 0, 0);
            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Cliente", true), 1, 0);
            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Telefone", true), 2, 0);
            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Profissional", true), 3, 0);
            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Data", true), 4, 0);
            tableLayoutPanel1.Controls.Add(Getters.GetLabel("Hora", true), 5, 0);

            foreach(var entidade in agendamentos)
            {
                int line = agendamentos.IndexOf(entidade) +1;

                Agendamento agendamento = (Agendamento)entidade;

                tableLayoutPanel1.Controls.Add(Getters.GetLabel(agendamento.Id.ToString(), false), 0, line);
                tableLayoutPanel1.Controls.Add(Getters.GetLabel(agendamento.Cliente.Nome, false), 1, line);
                tableLayoutPanel1.Controls.Add(Getters.GetLabel(agendamento.Cliente.Telefone, false), 2, line);
                tableLayoutPanel1.Controls.Add(Getters.GetLabel(agendamento.Profissional.Nome, false), 0, line);
                tableLayoutPanel1.Controls.Add(Getters.GetLabel(agendamento.Data.ToString(), false), 1, line);
                tableLayoutPanel1.Controls.Add(Getters.GetLabel(agendamento.Hora, false), 2, line);
            }

            panel1.Controls.Add(tableLayoutPanel1);
        }

        private void ConsultarAgenda_Load(object sender, EventArgs e)
        {

        }
        // Alterar
        private void button1_Click(object sender, EventArgs e)
        {

        }
        // Excluir
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
