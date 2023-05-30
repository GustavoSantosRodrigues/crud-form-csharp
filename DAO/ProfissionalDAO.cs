using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agenda.Entity;
using Agenda.Enum;
using Npgsql;

namespace Agenda.DAO
{
    internal class ProfissionalDAO : IDAO
    {
        public Boolean Alterar(Entidade entidade)
        {
            Profissional profissional = (Profissional) entidade;
            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = $@"UPDATE profissional SET 
                                    nome = '{profissional.Nome}', id_horario_trabalho = {(int)profissional.Horario}, 
                                    id_dias_trabalho = {(int)profissional.Dias}, id_profissao = {(int)profissional.Profissao} 
                                    WHERE id = {profissional.Id};";

                    MessageBox.Show(query);

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar os dados: " + ex.Message);
                    return false;
                }
            }
        }

        public Entidade BuscarPorId(int id)
        {
            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT * FROM profissional WHERE id = '{id}';";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Profissional profissional = BuildProfissional(reader);
                                return profissional;

                            }
                            return null;
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

        public List<Entidade> BuscarTodos()
        {
            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM profissional";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<Entidade> profissionais = new List<Entidade>();
                            while (reader.Read())
                            {
                                Profissional profissional = BuildProfissional(reader);
                                profissionais.Add(profissional);
                            }

                            return profissionais;
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

        public void Excluir(int id)
        {
            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = $"DELETE FROM profissional WHERE {id}";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao excluir os dados: " + ex.Message);
                }
            }
        }
    
        public Boolean Salvar(Entidade entidade)
        {
            Profissional profissional = (Profissional)entidade;

            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = $@"INSERT INTO profissional 
                                    (nome, id_horario_trabalho, id_dias_trabalho, id_profissao)
                                    VALUES ('{profissional.Nome}',{(int) profissional.Horario},
                                        {(int) profissional.Dias},{(int) profissional.Profissao})";

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

        private Profissional BuildProfissional(NpgsqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            string nome = reader.GetString(1);
            int idHorario = reader.GetInt32(2);
            int idDias = reader.GetInt32(3);
            int idProfissao = reader.GetInt32(4);

            Profissional profissional = new Profissional(
                id, 
                nome,
                (Enum.Profissao)System.Enum.ToObject(typeof(Enum.Profissao), idProfissao),
                (Enum.HorarioTrabalho)System.Enum.ToObject(typeof(Enum.HorarioTrabalho), idHorario),
                (Enum.DiasTrabalho)System.Enum.ToObject(typeof(Enum.DiasTrabalho), idDias)
            );

            return profissional;
        }

        public List<Profissional> buscarPorProfissaoId(int profissaoId)
        {
            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT * FROM profissional where id_profissao = {profissaoId};";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<Profissional> profissionais = new List<Profissional>();
                            while (reader.Read())
                            {
                                Profissional profissional = BuildProfissional(reader);

                                profissionais.Add(profissional);

                            }

                            return profissionais;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                    throw new NotImplementedException();
                }
            }
        }

        internal Profissional BuscarPorNomeEProfissao(string nome, int idProfissao)
        {
            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT * FROM profissional WHERE nome = '{nome}' and id_profissao = {idProfissao};";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Profissional profissional = BuildProfissional(reader);
                                return profissional;
                            }

                            return null;
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
    }
}
