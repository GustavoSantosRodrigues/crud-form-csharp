using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Agenda.DAO
{
    public class DatabaseConnection
    {
        private DatabaseConnection() { }

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Host=localhost;Port=5432;Database=agenda;Username=postgres;Password=postgres");
        }

    }
}
