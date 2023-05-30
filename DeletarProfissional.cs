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

namespace Agenda
{
    public partial class DeletarProfissional : Form
    {
        private Profissional profissional = null;
        public Form ReferenciaConsultarProfissional { get; set; }
        public Form ReferenceHomePage { get; set; }
        public DeletarProfissional()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(Getters.GetProfissoes());

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string idProfissional = textBox1.Text;
            if (!Regex.IsMatch(idProfissional, @"^\d+$"))
            {
                MessageBox.Show("O id deve ser um número.");
                return;
            }

            ProfissionalDAO profissionalDAO = new ProfissionalDAO();
            Entidade profissionalBuscado = profissionalDAO.BuscarPorId(Int32.Parse(idProfissional));

            if (profissionalBuscado == null)
            {
                MessageBox.Show("O id informado não corresponde a nenhum profissional salvo.");
                return;
            }

            profissional = (Profissional)profissionalBuscado;

            // preencher formulário.
            textBox3.Text = profissional.Nome;
            comboBox1.SelectedIndex = ((int)profissional.Profissao) - 1;
            int indexHorario = ((int)profissional.Horario) - 1;
            int indexDias = ((int)profissional.Dias) - 1;

            RadioButton radioButton = (RadioButton)panel2.Controls[indexHorario];
            radioButton.Checked = true;

            radioButton = (RadioButton)panel3.Controls[indexDias];
            radioButton.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (profissional == null)
            {
                MessageBox.Show("É preciso buscar um profissional antes de tentar deletar!");
                return;
            }
            ProfissionalDAO profissionalDAO = new ProfissionalDAO();
            profissionalDAO.Excluir(profissional.Id);

            ReferenciaConsultarProfissional.Close();
            this.Close();

            ReferenceHomePage.Show();
            MessageBox.Show("Profissional excluido com sucesso!");
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            ReferenciaConsultarProfissional.Show();
        }

    }
}
