using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdatbazisWPF
{
    public class CustomerService
    {
        MySqlConnection connection;
        public CustomerService()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "tagdij";

            connection = new MySqlConnection(builder.ConnectionString);
        }

        public bool Create(Customer customer)
        {
            OpenConnection();
            string sql = "INSERT INTO ugyfel(nev,szulev,irszam,orsz) VALUES (@name,@bday,@pcode,@country)";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@name", customer.Name);
            command.Parameters.AddWithValue("@bday", customer.Birthyear);
            command.Parameters.AddWithValue("@pcode", customer.Postal_Code);
            command.Parameters.AddWithValue("@country", customer.Country);

            int affectedRows = command.ExecuteNonQuery();

            CloseConnection();
            return affectedRows == 1;

        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            OpenConnection();
            string sql = "SELECT * FROM ugyfel";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.Id = reader.GetInt32("azon");
                    customer.Name = reader.GetString("nev");
                    customer.Birthyear = reader.GetInt32("szulev");
                    customer.Postal_Code = reader.GetInt32("irszam");
                    customer.Country = reader.GetString("orsz");
                    customers.Add(customer);
                }
            }
            CloseConnection();
            return customers;
        }

        public bool Update(int id, Customer newValues)
        {
            OpenConnection();
            string sql = @"UPDATE ugyfel 
                            SET nev = @name, 
                                szulev = @bday, 
                                irszam = @pcode, 
                                orsz = @country 
                            WHERE id = @id";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@name", newValues.Name);
            command.Parameters.AddWithValue("@bday", newValues.Birthyear);
            command.Parameters.AddWithValue("@pcode", newValues.Postal_Code);
            command.Parameters.AddWithValue("@country", newValues.Country);
            command.Parameters.AddWithValue("@id", id);
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();
            return affectedRows == 1;
        }

        public bool Delete(int id)
        {
            OpenConnection();
            string sql = "DELETE FROM ugyfel WHERE id = @id";
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            int affectedRows = command.ExecuteNonQuery();
            CloseConnection();
            return affectedRows == 1;
        }

        private void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }
    }
}
