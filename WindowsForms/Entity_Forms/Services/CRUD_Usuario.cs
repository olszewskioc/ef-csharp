using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity_Forms.Data;
using Entity_Forms.Models;
using Npgsql;

namespace Entity_Forms.Services
{
    public class CrudUser
    {
        public void Inserir(int id, string nome, string senha, int ramal, string especialidade)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Usuarios.Add(new Usuario {Id_Usuario = id, Nome =nome, Password = senha, Ramal = ramal, Especialidade = especialidade});
                    db.SaveChanges();
                    Console.WriteLine($"Usuário {nome} criado com sucesso\n");
                }
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
        public List<string>? Listar(int? id = null, string? nome = null, int? ramal = null, string? especialidade = null)
        {
            try
            {
                List<string> resultado = new List<string>();
                var usuarios = new List<Usuario>();
                using (var db = new AppDbContext())
                {
                    if (id == null && nome == null && ramal == null && especialidade == null)   // GET ALL
                    {
                        usuarios = db.Usuarios.ToList();
                    } else if (id != null && nome == null && ramal == null && especialidade == null)    // GET BY ID
                    {
                        usuarios = db.Usuarios.Where(u => u.Id_Usuario == id).ToList();
                    } else if (id == null && nome != null && ramal == null && especialidade == null)    // GET BY NOME
                    {
                        usuarios = db.Usuarios.Where(u => u.Nome == nome).ToList();
                    } else if (id == null && nome == null && ramal != null && especialidade == null)    // GET BY RAMAL
                    {
                        usuarios = db.Usuarios.Where(u => u.Ramal == ramal).ToList();
                    } else if (id == null && nome == null && ramal == null && especialidade != null)    // GET BY ESPECIALIDADE
                    {
                        usuarios = db.Usuarios.Where(u => u.Especialidade == especialidade).ToList();
                    } else
                    {
                        throw new Exception("ERRO: Parâmetros inválidos");
                    }
                    
                    foreach (var user in usuarios)
                    {
                        string linha  = $"ID: {user.Id_Usuario} | NOME: {user.Nome} | SENHA: {user.Password} | RAMAL: {user.Ramal} | ESPECIALIDADE: {user.Especialidade}";
                        resultado.Add(linha);
                    }
                    return resultado;
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR DB: {ex.Message}");
                return null;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                return null;
            }
        }

        public void Atualizar(int? id = null, string? nome = null, string? senha = null, int? ramal = null, string? especialidade = null)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var usuario = db.Usuarios.Find(id) ?? throw new Exception($"Unable to find usuario {id}");
                    
                    usuario.Nome = nome ?? usuario.Nome;
                    usuario.Password = senha ?? usuario.Password;
                    usuario.Ramal = ramal ?? usuario.Ramal;
                    usuario.Especialidade = especialidade ?? usuario.Especialidade;
                    db.SaveChanges();
                    Console.WriteLine($"Usuário {id} atualizado com sucesso\n");
                }
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

        public void Deletar(int id)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var usuario = db.Usuarios.Find(id) ?? throw new Exception($"Unable to find usuario {id}");

                    db.Usuarios.Remove(usuario);
                    db.SaveChanges();
                    Console.WriteLine($"Usuário {id} deletado com sucesso\n");
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