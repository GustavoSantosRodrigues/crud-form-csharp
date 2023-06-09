﻿using System;
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
using Agenda.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Agenda
{
    public partial class InserirAgendamento : Form
    {
        private List<Profissional> Profissionais { get; set; }
        public Form ReferenceHomePage { get; set; }
        public InserirAgendamento()
        {
            InitializeComponent();
        }
        public InserirAgendamento(List<Profissional> profissionais)
        {
            InitializeComponent();
            
            Profissionais = profissionais;

            if(profissionais.Count == 0 )
            {
                MessageBox.Show("Não há profissionais para o serviço selecionado.");
                ReferenceHomePage.Show();
                this.Close();
            }

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            foreach (var profissional in profissionais)
            {
                comboBox1.Items.Add(profissional.Nome);
            }

            dateTimePicker1.MinDate = DateTime.Today.AddDays(1);
            dateTimePicker1.MaxDate = new DateTime(2023, 12, 31);

            comboBox2.Items.AddRange(Getters.GetArrayHorarios());
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

            if (String.IsNullOrEmpty(nomeCliente))
            {
                MessageBox.Show("O nome do cliente não pode ser vázio.");
                return;
            }
            if (String.IsNullOrEmpty(telefone.Replace("(","").Replace(")","").Replace("-","").Trim()))
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

            AgendamentoDAO agendamentoDao = new AgendamentoDAO();
            List<Entidade> agendamentos = agendamentoDao.BuscarTodos();

            foreach (var entidade in agendamentos)
            {
                Agendamento ag = (Agendamento)entidade;
                if(ag.Profissional.Id == profissional.Id)
                {
                    if(ag.Data == data)
                    {
                        if(ag.Hora == hora)
                        {
                            MessageBox.Show("O profissional já tem um agendamento neste dia e horário. Troque por favor!");
                            return;
                        }
                    }
                }
            }

            Cliente cliente = new Cliente(RemoverEspacos(nomeCliente), RemoverEspacos(telefone));
            Agendamento agendamento = new Agendamento(cliente, profissional, data, hora);

            Boolean isSalvo = agendamentoDao.Salvar(agendamento);

            if (isSalvo)
            {
                MessageBox.Show("Agendamento salvo com sucesso.");
                
                this.Close();
                ReferenceHomePage.Show();
            }
            else
            {
                MessageBox.Show("Erro ao se connectar com o banco. Não foi possível salvar o agendamento.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            ReferenceHomePage.Show();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
