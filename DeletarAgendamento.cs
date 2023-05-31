using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agenda.DAO;
using Agenda.Entity;
using Agenda.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Agenda
{
    public partial class DeletarAgendamento : Form
    {
        public Form ReferenceHomePage { get; set; }
        public Form ReferenceConsultarAgendamento { get; set; }
        public List<Profissional> Profissionais { get; set; }
        public Agendamento agendamento { get; set; }
        public DeletarAgendamento()
        {
            InitializeComponent();
            comboBox2.Items.AddRange(Getters.GetArrayHorarios());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string idAgendamento = textBox1.Text;
            if (!Regex.IsMatch(idAgendamento, @"^\d+$"))
            {
                MessageBox.Show("O id deve ser um número.");
                return;
            }

            AgendamentoDAO agendamentoDAO = new AgendamentoDAO();
            Entidade agendamentoBuscado = agendamentoDAO.BuscarPorId(Int32.Parse(idAgendamento));

            if (agendamentoBuscado == null)
            {
                MessageBox.Show("O id informado não corresponde a nenhum agendamento salvo.");
                return;
            }

            agendamento = (Agendamento)agendamentoBuscado;

            // preencher formulário.
            textBox2.Text = agendamento.Cliente.Nome;
            maskedTextBox1.Text = agendamento.Cliente.Telefone;

            int idProfissao = (int)agendamento.Profissional.Profissao;

            ProfissionalDAO profissionalDAO = new ProfissionalDAO();
            Profissionais = profissionalDAO.buscarPorProfissaoId(idProfissao);

            foreach (var pf in Profissionais)
            {
                comboBox1.Items.Add(pf.Nome);
            }

            comboBox1.SelectedItem = agendamento.Profissional.Nome;

            dateTimePicker1.Value = agendamento.Data;

            foreach (var item in comboBox2.Items)
            {
                if (item.ToString().Substring(0, 5) == agendamento.Hora)
                {
                    comboBox2.SelectedItem = item;
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (agendamento == null)
            {
                MessageBox.Show("É preciso buscar um agendamento antes de tentar deletar!");
                return;
            }
            AgendamentoDAO agendamentoDAO = new AgendamentoDAO();
            agendamentoDAO.Excluir(agendamento.Id);

            ReferenceConsultarAgendamento.Close();
            this.Close();

            ReferenceHomePage.Show();
            MessageBox.Show("Agendamento excluido com sucesso!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            ReferenceConsultarAgendamento.Show();
        }
    }
}
