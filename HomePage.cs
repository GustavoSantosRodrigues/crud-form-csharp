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
using Agenda.Enum;

namespace Agenda
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void RedirectInserirAgenda(Profissao profissao)
        {
            ProfissionalDAO profissionalDAO = new ProfissionalDAO();
            List<Profissional> profissionais = profissionalDAO.buscarPorProfissaoId((int)profissao);

            this.Hide();

            InserirAgenda inserirAgenda = new InserirAgenda(profissionais);
            inserirAgenda.ReferenceHomePage = this;
            
            inserirAgenda.Show();
        }
        // Cabelos
        private void button1_Click(object sender, EventArgs e)
        {
            RedirectInserirAgenda(Profissao.CABELEREIRO);
        }
        // Penteados
        private void button2_Click(object sender, EventArgs e)
        {
            RedirectInserirAgenda(Profissao.CABELEREIRO);
        }
        // Make
        private void button3_Click(object sender, EventArgs e)
        {
            RedirectInserirAgenda(Profissao.MAQUIADOR);
        }
        // Designer de Sobrancelha
        private void button4_Click(object sender, EventArgs e)
        {
            RedirectInserirAgenda(Profissao.DESIGNER_SOMBRANCELHA);
        }
        // Unhas
        private void button5_Click(object sender, EventArgs e)
        {
            RedirectInserirAgenda(Profissao.MANICURE);
        }
        // Visagismo
        private void button6_Click(object sender, EventArgs e)
        {
            RedirectInserirAgenda(Profissao.VISAGISTA);
        }
        // Consultar Profissionais
        private void button8_Click(object sender, EventArgs e)
        {
            ConsultarProfissionais consultarProfissionais = new ConsultarProfissionais();
            consultarProfissionais.Show();
        }
        // Consultar Agenda
        private void button9_Click(object sender, EventArgs e)
        {
            ConsultarAgenda consultarAgenda = new ConsultarAgenda();
            consultarAgenda.Show();
        }
    }
}
