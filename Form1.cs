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
using Agenda.Entity;
using Agenda.Enum;

namespace Agenda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

<<<<<<< Updated upstream
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
=======
        private void button1_Click(object sender, EventArgs e)
        {
            Profissional profissional = new Profissional();

            profissional.Id = 0;
            profissional.Nome = "Mateus";
            profissional.Horario = HorarioTrabalho.INICIO_08;
            profissional.Dias = DiasTrabalho.SEG_SEX;

            ProfissionalDAO profissionalDAO = new ProfissionalDAO();
            
            try
            {
                List<Entidade> entidades = profissionalDAO.BuscarTodos();

                foreach (var entidade in entidades)
                {
                    Profissional pf = (Profissional)entidade;
                    MessageBox.Show(pf.Id+pf.Nome);
                }

                //MessageBox.Show("Profissionais buscados com sucesso!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado e não foi possivel salvar o profissional.");

            }
            finally
            {
            //clearForm();
            }
>>>>>>> Stashed changes

        }
    }
}
