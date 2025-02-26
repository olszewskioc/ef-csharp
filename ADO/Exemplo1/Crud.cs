using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace ADO.Exemplo1
{
    // Creating a CRUD
    public class Crud
    {
        string conexao = "Host=localhost;Database=digix-sql;Username=postgres;Password=olszewski";

        public void InserirUser(string nome, string password)
        {
            string query = $"INSERT INTO \"Users\" (\"Nome\", \"Password\") VALUES ('{nome}', '{password}')";

            using NpgsqlConnection con = new(conexao);

            try
            {
                con.Open();
                Console.WriteLine($"Conectado - Inserir!\n");
                
                using NpgsqlCommand cmd = new(query, con);
                cmd.Parameters.AddWithValue("\"Nome\"", nome);
                cmd.Parameters.AddWithValue("\"Password\"", password);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"User: {nome} criado com sucesso!\n");
                
                con.Close();
                Console.WriteLine($"Fechado!\n");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void LerUser(string nome)
        {
            string query = $"SELECT * FROM \"Users\" WHERE \"Nome\" = '{nome}'";

            using NpgsqlConnection con = new(conexao);
            try
            {
                con.Open();
                Console.WriteLine($"Conectado - Ler!\n");

                using NpgsqlCommand cmd = new(query, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32(0)}, Nome: {reader.GetString(1)}, Password: {reader.GetString(2)}");
                }
                con.Close();
                Console.WriteLine($"Fechado!\n");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void DeleteUser(int? id = null)
        {
            string query;
            if (id == null)
            {
                query = $"DELETE FROM \"Users\"";
            } else
            {
                query = $"DELETE FROM \"Users\" WHERE \"Id\" = {id}";
            }

            using NpgsqlConnection con = new(conexao);

            try
            {
                con.Open();
                Console.WriteLine($"Conectado - Delete!\n");
                
                using NpgsqlCommand cmd = new(query, con);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"User: {id} deletado com sucesso!\n{rows} linha(s) afetadas!\n");
                
                con.Close();
                Console.WriteLine($"Fechado!\n");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
        public void AtualizarUser(int id, string nome, string password)
        {
            string query = $"UPDATE \"Users\" SET \"Nome\" = '{nome}', \"Password\" = '{password}' WHERE \"Id\" = {id}";

            using NpgsqlConnection con = new(conexao);

            try
            {
                con.Open();
                Console.WriteLine($"Conectado - Atualizar!\n");
                
                using NpgsqlCommand cmd = new(query, con);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"User: {id} atualizado com sucesso!\n{rows} linha(s) afetadas!\n");
                
                con.Close();
                Console.WriteLine($"Fechado!\n");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }


    }
}