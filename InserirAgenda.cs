using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agenda.DAO;
using Agenda.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Agenda
{
    public partial class InserirAgenda : Form
    {
        private List<Profissional> profissionais;
        public Form ReferenceHomePage { get; set; }
        public InserirAgenda()
        {
            InitializeComponent();
        }
        public InserirAgenda(List<Profissional> profissionais)
        {
            InitializeComponent();
            
            this.profissionais = profissionais;

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            foreach (var profissional in profissionais)
            {
                comboBox1.Items.Insert(profissionais.IndexOf(profissional), profissional.Nome);
            }

            dateTimePicker1.MinDate = DateTime.Today.AddDays(1);
            dateTimePicker1.MaxDate = new DateTime(2023, 12, 31);

            comboBox2.Items.AddRange(Utils.Getters.GetArrayHorarios());
        }
        private string RemoverEspacos(string texto)
        {
            return texto.TrimStart().TrimEnd();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string nomeCliente = textBox1.Text;
            string telefone = maskedTextBox1.Text;

            string nomeProfissional = comboBox1.SelectedItem?.ToString();

            DateTime data = dateTimePicker1.Value;
            string hora = comboBox2.SelectedItem?.ToString();

            MessageBox.Show(nomeCliente+telefone+nomeProfissional+data+hora);

            if (String.IsNullOrEmpty(nomeCliente))
            {
                MessageBox.Show("O nome do cliente não pode ser vázio.");
                return;
            }
            else if (String.IsNullOrEmpty(telefone.Replace("(","").Replace(")","").Replace("-","").Trim()))
            {
                MessageBox.Show("O telefone do cliente não pode ser vázio.");
                return;
            }
            else if (String.IsNullOrEmpty(nomeProfissional))
            {
                MessageBox.Show("É preciso selecionar um profissional.");
                return;
            }
            else if (data == null)
            {
                MessageBox.Show("É preciso selecionar uma data.");
                return;
            }
            else if (String.IsNullOrEmpty(hora))
            {
                MessageBox.Show("É preciso selecionar um horário.");
                return;
            }
            
            Profissional profissional = new Profissional();

            foreach (var pf in profissionais)
            {
                if(pf.Nome == nomeProfissional)
                {
                    profissional = pf;
                    break;
                }
            }
            //validarHorario
            if((int) profissional.Horario == 1)
            {
                int horario = Int32.Parse(hora.Substring(0, 2));
                if(horario < 8 || horario > 16)
                {
                    MessageBox.Show("O profissional não atende neste horário. Selecione outro horário.");
                    return;
                }

            }
            else if((int) profissional.Horario == 2)
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
                int dayOfWeek = (int) data.DayOfWeek;
                if (dayOfWeek != 6 && dayOfWeek != 7)
                {
                    MessageBox.Show("O profissional não atende neste dia da semana. Selecione outra data.");
                    return;
                }
            }

            Cliente cliente = new Cliente(RemoverEspacos(nomeCliente), RemoverEspacos(telefone));

            Agendamento agendamento = new Agendamento(cliente, profissional, data, hora);

            AgendamentoDAO agendamentoDao = new AgendamentoDAO();
            
            Boolean isSalvo = agendamentoDao.Salvar(agendamento);

            if (isSalvo)
            {
                MessageBox.Show("Agendamento salvo com sucesso.");
            }
            else
            {
                MessageBox.Show("Erro ao se connectar com o banco. Não foi possível salvar o agendamento.");
            }
        }

        private void InserirAgenda_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            ReferenceHomePage.Show();
        }
    }
}
