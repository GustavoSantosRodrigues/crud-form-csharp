using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Entity;
using Npgsql;

namespace Agenda.DAO
{
    internal class AgendamentoDAO : IDAO
    {
        public Boolean Alterar(Entidade entidade)
        {
            throw new NotImplementedException();
        }

        public Entidade BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Entidade> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Boolean Salvar(Entidade entidade)
        {
            Agendamento agendamento = (Agendamento) entidade;

            using (NpgsqlConnection connection = DatabaseConnection.GetConnection() )
            {
                try
                {
                    connection.Open();

                    string query = $@"INSERT INTO agendamento 
                        (data, hora, id_cliente, id_agendamento) 
                        VALUES ({agendamento.Data})";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao salvar os dados: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
