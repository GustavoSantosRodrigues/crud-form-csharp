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
    public partial class ConsultarAgendamentos : Form
    {
        public Form ReferenceHomePage { get; set; }
        public ConsultarAgendamentos()
        {
            InitializeComponent();
            ConstruirTabela();
        }

        private void ConstruirTabela()
        {
            AgendamentoDAO agendamentoDAO = new AgendamentoDAO();
            List<Entidade> agendamentos = agendamentoDAO.BuscarTodos();
            panel1.Controls.Clear();

            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 50
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "Cliente",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Telefone",
                HeaderText = "Telefone",
                DataPropertyName = "Telefone",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Profissional",
                HeaderText = "Profissional",
                DataPropertyName = "Profissional",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Data",
                HeaderText = "Data",
                DataPropertyName = "Data",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Hora",
                HeaderText = "Hora",
                DataPropertyName = "Hora",
                Width = 100
            });

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // Limpar as linhas existentes
            dataGridView1.Rows.Clear();

            foreach (var entidade in agendamentos)
            {
                Agendamento agendamento = (Agendamento)entidade;

                dataGridView1.Rows.Add(
                    agendamento.Id,
                    agendamento.Cliente.Nome,
                    agendamento.Cliente.Telefone,
                    agendamento.Profissional.Nome,
                    agendamento.Data.ToString().Substring(0, 10),
                    agendamento.Hora
                );
            }

            panel1.Controls.Add(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlterarAgendamento alterarAgendamento = new AlterarAgendamento();
            alterarAgendamento.ReferenceHomePage = ReferenceHomePage;
            alterarAgendamento.ReferenceConsultarAgendamento = this;

            this.Hide();
            alterarAgendamento.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            ReferenceHomePage.Show();
        }

        private void ConsultarAgenda_Load(object sender, EventArgs e)
        {

        }
    }
}
