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
            throw new NotImplementedException();
        }

        public Entidade BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Entidade> BuscarTodos()
        {
            NpgsqlConnection connection = DatabaseConnection.GetConnection();

            try
            {
                connection.Open();

                // Realiza uma consulta ao banco de dados
                string query = "SELECT * FROM profissional";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        List<Entidade> profissionais = new List<Entidade>();
                        while (reader.Read())
                        {
                            // Processa os resultados da consulta
                            int id = reader.GetInt32(0);
                            string nome = reader.GetString(1);
                            int idHorario = reader.GetInt32(3);
                            int idData = reader.GetInt32(4);

                            Profissional profissional = new Profissional();

                            profissional.Id = id;
                            profissional.Nome = nome;
                            
                            profissionais.Add(profissional);

                            Console.WriteLine($"ID: {id}, Nome: {nome}");
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

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Boolean Salvar(Entidade entidade)
        {

            

            throw new NotImplementedException();
        }

        public List<Profissional> buscarPorProfissaoId(int profissaoId)
        {
            NpgsqlConnection connection = DatabaseConnection.GetConnection();

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
                            int id = reader.GetInt32(0);
                            string nome = reader.GetString(1);
                            int idHorario = reader.GetInt32(3);
                            int idData = reader.GetInt32(4);

                            Profissional profissional = new Profissional();

                            profissional.Id = id;
                            profissional.Nome = nome;

                            profissionais.Add(profissional);

                            MessageBox.Show($"ID: {id}, Nome: {nome}");
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
}
