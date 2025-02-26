using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exemplo1.Data;
using Exemplo1.Models;
using Npgsql;

namespace Exemplo1.Services
{
    public class Crud
    {
        public void InserirUsuario(int id, string name, string email)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Usuarios.Add(new Usuario {Id = id, Name = name, Email = email});
                    db.SaveChanges();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void ListarUsuarios()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var usuarios = db.Usuarios.ToList();
                    foreach (var usuario in usuarios)
                    {
                        Console.WriteLine($"ID: {usuario.Id}, NOME: {usuario.Name}, EMAIL: {usuario.Email}");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void DeleteUsuario(int id)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var usuario = db.Usuarios.Find(id);

                    if (usuario == null)
                    {
                        Console.WriteLine($"Usuário não encontrado");
                    } else
                    {
                        db.Usuarios.Remove(usuario);
                        db.SaveChanges();
                        Console.WriteLine($"Usuário {id} deletado com sucesso\n");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void AtualizarUsuario(int id, string? name = null, string? email = null)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var usuario = db.Usuarios.Find(id);
                    
                    if (usuario == null)
                    {
                        Console.WriteLine($"Usuário não encontrado");
                    } else
                    {
                        usuario.Name = name ?? usuario.Name;
                        usuario.Email = email ?? usuario.Email;
                        db.SaveChanges();
                        Console.WriteLine($"Usuário {id} atualizado com sucesso\n");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}