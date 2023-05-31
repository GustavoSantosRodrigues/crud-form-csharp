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
    public partial class AlterarAgendamento : Form
    {
        public Form ReferenceHomePage { get; set; }
        public Form ReferenceConsultarAgendamento { get; set; }
        public List<Profissional> Profissionais { get; set; }
        public Agendamento agendamento { get; set; }
        public AlterarAgendamento()
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
            Profissionais = profissionalDAO.buscarPorProfissaoId( idProfissao );

            foreach (var pf in Profissionais)
            {
                comboBox1.Items.Add(pf.Nome);
            }

            comboBox1.SelectedItem = agendamento.Profissional.Nome ;

            dateTimePicker1.Value = agendamento.Data;

            foreach(var item in comboBox2.Items)
            {
                if(item.ToString().Substring(0,5) == agendamento.Hora)
                {
                    comboBox2.SelectedItem = item;
                    break;
                }
            }

        }
        private string RemoverEspacos(string texto)
        {
            return texto.TrimStart().TrimEnd();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (agendamento == null)
            {
                MessageBox.Show("É preciso buscar um agendamento antes de tentar atualizar!");
                return;
            }
            string nomeCliente = textBox2.Text;
            string telefone = maskedTextBox1.Text;

            string nomeProfissional = comboBox1.SelectedItem?.ToString();

            DateTime data = dateTimePicker1.Value;
            string hora = comboBox2.SelectedItem?.ToString();

            if (String.IsNullOrEmpty(nomeCliente))
            {
                MessageBox.Show("O nome do cliente não pode ser vázio.");
                return;
            }
            if (String.IsNullOrEmpty(telefone.Replace("(", "").Replace(")", "").Replace("-", "").Trim()))
            {
                MessageBox.Show("O telefone do cliente não pode ser vázio.");
                return;
            }
            if (String.IsNullOrEmpty(nomeProfissional))
            {
                MessageBox.Show("É preciso selecionar um profissional.");
                return;
            }
            if (data == null)
            {
                MessageBox.Show("É preciso selecionar uma data.");
                return;
            }
            if (String.IsNullOrEmpty(hora))
            {
                MessageBox.Show("É preciso selecionar um horário.");
                return;
            }

            Profissional profissional = new Profissional();
            hora = hora.Substring(0, 5);

            foreach (var pf in Profissionais)
            {
                if (pf.Nome == nomeProfissional)
                {
                    profissional = pf;
                    break;
                }
            }
            //validarHorario
            if ((int)profissional.Horario == 1)
            {
                int horario = Int32.Parse(hora.Substring(0, 2));
                if (horario < 8 || horario > 16)
                {
                    MessageBox.Show("O profissional não atende neste horário. Selecione outro horário.");
                    return;
                }

            }
            else if ((int)profissional.Horario == 2)
            {
                int horario = Int32.Parse(hora.Substring(0, 2));
                if (horario < 9 || horario > 17)
                {
                    MessageBox.Show("O profissional não atende neste horário. Selecione outro horário.");
                    return;
                }
            }
            else
            {
                int horario = Int32.Parse(hora.Substring(0, 2));
                if (horario < 10 || horario > 18)
                {
                    MessageBox.Show("O profissional não atende neste horário. Selecione outro horário.");
                    return;
                }
            }

            //validarData
            if ((int)profissional.Dias == 1)
            {
                int dayOfWeek = (int)data.DayOfWeek;
                if (dayOfWeek == 6 || dayOfWeek == 7)
                {
                    MessageBox.Show("O profissional não atende neste dia da semana. Selecione outra data.");
                    return;
                }
            }
            else if ((int)profissional.Dias == 2)
            {
                int dayOfWeek = (int)data.DayOfWeek;
                if (dayOfWeek != 6 && dayOfWeek != 7)
                {
                    MessageBox.Show("O profissional não atende neste dia da semana. Selecione outra data.");
                    return;
                }
            }

            AgendamentoDAO agendamentoDao = new AgendamentoDAO();
            List<Entidade> agendamentos = agendamentoDao.BuscarTodos();

            foreach (var entidade in agendamentos)
            {
                Agendamento ag = (Agendamento)entidade;
                if (ag.Profissional.Id == profissional.Id)
                {
                    if (ag.Data == data)
                    {
                        if (ag.Hora == hora)
                        {
                            if(ag.Id != agendamento.Id)
                            {
                                MessageBox.Show("O profissional já tem um agendamento neste dia e horário. Troque por favor!");
                                return;
                            }
                        }
                    }
                }
            }

            Cliente cliente = new Cliente(RemoverEspacos(nomeCliente), RemoverEspacos(telefone));

            agendamento.Cliente = cliente;
            agendamento.Profissional = profissional;
            agendamento.Data = data;
            agendamento.Hora = hora;

            Boolean isAlterado = agendamentoDao.Alterar(agendamento);

            if (isAlterado)
            {
                ReferenceConsultarAgendamento.Close();
                this.Close();
                ReferenceHomePage.Show();

                MessageBox.Show("Agendamento alterado com sucesso.");
            }
            else
            {
                MessageBox.Show("Erro ao se connectar com o banco. Não foi possível alterar o agendamento.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            ReferenceConsultarAgendamento.Show();
        }
    }
}
