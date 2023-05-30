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
    public partial class AlterarProfissional : Form
    {
        private Profissional profissional = null;
        public Form ReferenciaConsultarProfissional { get; set; }
        public Form ReferenceHomePage { get; set; }
        public AlterarProfissional()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(Getters.GetProfissoes());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string idProfissional = textBox1.Text;
            if(!Regex.IsMatch(idProfissional, @"^\d+$"))
            {
                MessageBox.Show("O id deve ser um número.");
                return;
            }
            
            ProfissionalDAO profissionalDAO = new ProfissionalDAO();
            Entidade profissionalBuscado = profissionalDAO.BuscarPorId(Int32.Parse(idProfissional));

            if(profissionalBuscado == null)
            {
                MessageBox.Show("O id informado não corresponde a nenhum profissional salvo.");
                return;
            }

            profissional = (Profissional)profissionalBuscado;

            // preencher formulário.
            textBox3.Text = profissional.Nome;
            comboBox1.SelectedIndex = ((int)profissional.Profissao)-1;
            int indexHorario = ((int)profissional.Horario)-1;
            int indexDias = ((int)profissional.Dias)-1;

            RadioButton radioButton = (RadioButton)panel2.Controls[indexHorario];
            radioButton.Checked = true;

            radioButton = (RadioButton)panel3.Controls[indexDias];
            radioButton.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(profissional == null)
            {
                MessageBox.Show("É preciso buscar um profissional antes de tentar atualizar!");
                return;
            }

            string nome = textBox3.Text;
            string horarioSelecionado = null;
            string dataSelecionada = null;
            string profissao = comboBox1.SelectedItem?.ToString();
            int idHorario = 0;
            int idDias = 0;

            foreach (Control control in panel2.Controls)
            {
                idHorario++;
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    horarioSelecionado = radioButton.Text;
                    break;
                }
            }
            foreach (Control control in panel3.Controls)
            {
                idDias++;
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    dataSelecionada = radioButton.Text;
                    break;
                }
            }

            if (String.IsNullOrEmpty(nome))
            {
                MessageBox.Show("O nome do profissional não pode ser vázio.");
                return;
            }
            if (String.IsNullOrEmpty(profissao))
            {
                MessageBox.Show("É preciso selecionar uma profissão.");
                return;
            }
            if (String.IsNullOrEmpty(horarioSelecionado))
            {
                MessageBox.Show("É preciso selecionar um horário.");
                return;
            }
            if (String.IsNullOrEmpty(dataSelecionada))
            {
                MessageBox.Show("É preciso selecionar uma data.");
                return;
            }

            int idProfissao = comboBox1.SelectedIndex + 1;

            ProfissionalDAO profissionalDAO = new ProfissionalDAO();
            Profissional profissionalBuscado = profissionalDAO.BuscarPorNomeEProfissao(nome, idProfissao);

            if (profissionalBuscado != null && profissionalBuscado.Id != profissional.Id)
            {
                MessageBox.Show("Já há um profissional desta área registrado com este nome.");
                return;
            }

            profissional.Nome = nome;
            profissional.Profissao = (Enum.Profissao)System.Enum.ToObject(typeof(Enum.Profissao), idProfissao);
            profissional.Horario = (Enum.HorarioTrabalho)System.Enum.ToObject(typeof(Enum.HorarioTrabalho), idHorario);
            profissional.Dias = (Enum.DiasTrabalho)System.Enum.ToObject(typeof(Enum.DiasTrabalho), idDias);
           
            Boolean isAlterado = profissionalDAO.Alterar(profissional);

            if (isAlterado)
            {
                ReferenciaConsultarProfissional.Close();
                this.Close();

                ReferenceHomePage.Show();
                MessageBox.Show("Profissional alterado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao se connectar com o banco. Não foi possível salvar o agendamento.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            ReferenciaConsultarProfissional.Show();
        }
    }
}
