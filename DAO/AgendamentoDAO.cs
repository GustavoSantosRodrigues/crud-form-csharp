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
            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM agendamento";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<Entidade> agendamentos = new List<Entidade>();
                            while (reader.Read())
                            {
                                Agendamento agendamento = BuildAgendamento(reader);
                                agendamentos.Add(agendamento);
                            }

                            return agendamentos;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                    return null;
                }
            }
        }
        private Agendamento BuildAgendamento(NpgsqlDataReader reader)
        {
            return new Agendamento();
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
                        (nome_cliente, telefone_contato, id_profissional, data, hora) 
                        VALUES ({agendamento.Cliente.Nome},{agendamento.Cliente.Telefone}, {agendamento.Profissional.Id}, 
                        {agendamento.Data}, {agendamento.Hora.Substring(0, 5)})";

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
