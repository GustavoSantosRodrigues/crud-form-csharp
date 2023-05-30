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
        public Form ReferenceHomePage { get; set; }
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
                
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            // Configurar as colunas
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 50
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Nome",
                HeaderText = "Nome",
                DataPropertyName = "Nome",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Profissao",
                HeaderText = "Profissão",
                DataPropertyName = "Profissao",
                Width = 150
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DiasAtendimento",
                HeaderText = "Dias de Atendimento",
                DataPropertyName = "DiasAtendimento",
                Width = 220
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "HorariosAtendimento",
                HeaderText = "Horários de Atendimento",
                DataPropertyName = "HorariosAtendimento",
                Width = 215
            });

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // Adicionar os dados
            foreach (var entidade in profissionais)
            {
                Profissional profissional = (Profissional)entidade;

                dataGridView1.Rows.Add(
                    profissional.Id,
                    profissional.Nome,
                    profissional.Profissao.OptionString(),
                    profissional.Dias.OptionString(),
                    profissional.Horario.OptionString()
                );
            }

            panel1.Controls.Add(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InserirProfissional inserirProfissional = new InserirProfissional();
            inserirProfissional.ReferenciaConsultarProfissional = this;
            inserirProfissional.ReferenceHomePage = ReferenceHomePage;

            this.Hide();
            inserirProfissional.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlterarProfissional alterarProfissional = new AlterarProfissional();
            alterarProfissional.ReferenciaConsultarProfissional = this;
            alterarProfissional.ReferenceHomePage = ReferenceHomePage;

            this.Hide();
            alterarProfissional.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            ReferenceHomePage.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
