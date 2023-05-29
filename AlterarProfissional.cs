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

namespace Agenda
{
    public partial class AlterarProfissional : Form
    {
        private Profissional profissional = null;
        public Form ReferenciaConsultarProfissional { get; set; }
        public AlterarProfissional()
        {
            InitializeComponent();
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
            //Salvar alterações
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            ReferenciaConsultarProfissional.Show();
        }
    }
}
