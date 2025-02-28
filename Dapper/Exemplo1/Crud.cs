using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace Exemplo1
{
    public class Crud
    {
        public void InserirUsuario(int id, string nome, string email)
        {
            using (IDbConnection db = AppDbContext.GetConexao())
            {
                try
                {
                    string query = $"INSERT INTO USUARIOS (ID, NAME, EMAIL) VALUES ({id}, '{nome}', '{email}')";
                    db.Execute(query);
                    Console.WriteLine($"Usuário {nome} inserido com sucesso!\n");
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"ERROR DB: {ex.Message}\n");   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}\n");
                }
            }
        }
        public void AtualizarUsuario(int id, string? nome = null, string? email = null)
        {
            using (IDbConnection db = AppDbContext.GetConexao())
            {
                try
                {
                    string query;
                    if (nome != null && email != null)
                        throw new NpgsqlException("NOTHING TO UPDATE");
                    else if (nome != null && email == null) // UPDATE NAME
                        query = $"UPDATE USUARIOS SET NAME = '{nome}' WHERE ID = {id}";
                    else if (email != null && nome == null) // UPDATE EMAIL
                        query = $"UPDATE USUARIOS SET EMAIL = '{email}' WHERE ID = {id}";
                    else if (nome != null && email != null) // UPDATE NAME AND EMAIL
                        query = $"UPDATE USUARIOS SET NAME = '{nome}', EMAIL = '{email}' WHERE ID = {id}";
                    else
                        throw new NpgsqlException("ERROR ON UPDATE: PARAMETERS ERRORS");

                    db.Execute(query);
                    Console.WriteLine($"Usuário {nome} atualizado com sucesso!\n");
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"ERROR DB: {ex.Message}");   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }
        public void DeletarUsuario(int id)
        {
            using (IDbConnection db = AppDbContext.GetConexao())
            {
                try
                {
                    string query = $"DELETE FROM USUARIOS WHERE ID = {id}";
                    db.Execute(query);
                    Console.WriteLine($"Usuário {id} deletado com sucesso!\n");
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"ERROR DB: {ex.Message}");   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }

        public void ListarUsuario(int? id = null, string? nome = null, string? email = null)
        {
            using (IDbConnection db = AppDbContext.GetConexao())
            {
                try
                {
                    string query;

                    if (id == null && nome == null && email == null)    // GET ALL
                        query = $"SELECT * FROM USUARIOS";
                    else if (id != null && nome == null && email == null)   // GET BY ID
                        query = $"SELECT * FROM USUARIOS WHERE ID = {id}";
                    else if (id == null && nome != null && email == null)   // GET BY NAME
                        query = $"SELECT * FROM USUARIOS WHERE NAME = {nome}";
                    else if (id == null && nome == null && email != null)   // GET BY EMAIL
                        query = $"SELECT * FROM USUARIOS WHERE EMAIL = {email}";
                    else
                        throw new NpgsqlException("ERRO NA CONSULTA: Parâmetros inválidos");
                    
                    var usuarios = db.Query<Usuario>(query).ToList();
                    Console.WriteLine($"\nLista de todos os usuários");
                    foreach (var user in usuarios)
                    {
                        Console.WriteLine($"ID: {user.Id} | NOME: {user.Name} | EMAIL: {user.Email}");
                    }
                    Console.WriteLine($"\n");
                    
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine($"ERROR DB: {ex.Message}");   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }
    }
}