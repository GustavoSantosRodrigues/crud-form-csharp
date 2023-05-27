using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public partial class InserirAgenda : Form
    {
        private List<Profissional> profissionais;
        public InserirAgenda()
        {
            InitializeComponent();
        }
        public InserirAgenda(List<Profissional> profissionais)
        {
            InitializeComponent();
            
            this.profissionais = profissionais;

            comboBox1.Items.Clear();

            foreach (var profissional in profissionais)
            {
                comboBox1.Items.Add(profissional.Nome);
            }
        }
        private void InserirAgenda_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
