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
using Agenda.Enum;
namespace Agenda
{
    public partial class InserirProfissional : Form
    {
        public Form ReferenciaConsultarProfissional { get; set; }
        public Form ReferenceHomePage { get; set; }

        public InserirProfissional()
        {
            InitializeComponent();
                        
            comboBox1.Items.AddRange(Getters.GetProfissoes());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nome = textBox2.Text;
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
            Profissional profissional = profissionalDAO.BuscarPorNomeEProfissao(nome, idProfissao);

            if(profissional != null)
            {
                MessageBox.Show("Já há um profissional desta área registrado com este nome.");
                return;
            }

            profissional = new Profissional(
                nome, 
                (Enum.Profissao) System.Enum.ToObject(typeof(Enum.Profissao), idProfissao),
                (Enum.HorarioTrabalho) System.Enum.ToObject(typeof(Enum.HorarioTrabalho), idHorario),
                (Enum.DiasTrabalho) System.Enum.ToObject(typeof(Enum.DiasTrabalho), idDias)
                );

            Boolean isSalvo = profissionalDAO.Salvar(profissional);

            if (isSalvo)
            {
                ReferenciaConsultarProfissional.Close();
                this.Close();
                
                ReferenceHomePage.Show();
                MessageBox.Show("Profissional cadastrado com sucesso!");
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
