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
using Agenda.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Agenda
{
    public partial class InserirProfissional : Form
    {
        public InserirProfissional()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nome = textBox2.Text;
            string horarioSelecionado = null;
            string dataSelecionada = null;
            string profissao = comboBox1.SelectedItem?.ToString();

            foreach (Control control in panel2.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    horarioSelecionado = radioButton.Text;
                    break;
                }
            }
            foreach (Control control in panel3.Controls)
            {
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
            Profissional profissional = profissionalDAO.BuscarPorNomeEProfissao(nome, idProfissao);

            if(profissional != null)
            {
                MessageBox.Show("Já há um profissional dessa área registrado com este nome.");
                return;
            }

            profissional = new Profissional(
                nome, 
                (Enum.Profissao) System.Enum.ToObject(typeof(Enum.Profissao), 1),
                (Enum.HorarioTrabalho) System.Enum.ToObject(typeof(Enum.HorarioTrabalho), 1),
                (Enum.DiasTrabalho) System.Enum.ToObject(typeof(Enum.DiasTrabalho), 1)
                );

            Boolean isSalvo = profissionalDAO.Salvar(profissional);
            if (isSalvo)
            {
                //redirect
            }
            else
            {
                //message box
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
