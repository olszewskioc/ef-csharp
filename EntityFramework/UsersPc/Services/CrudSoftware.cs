using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersPc.Data;
using UsersPc.Models;
using Npgsql;

namespace UsersPc.Services
{
    public class CrudSoftware
    {
        public void Inserir(int id, string produto, int hd, int ram, int maquina)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Softwares.Add(new Software {SoftwareId = id, Produto = produto, HardDisk = hd, MemoriaRam = ram, MaquinaId = maquina });
                    db.SaveChanges();
                    Console.WriteLine($"Software {produto} criado com sucesso\n");
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
        public void Listar(int? id = null, string? produto = null, int? hd = null, int? ram = null, int? maquina = null)
        {
            try
            {
                var softwares = new List<Software>();
                using (var db = new AppDbContext())
                {
                    if (id == null && produto == null && hd == null && ram == null && maquina == null)   // GET ALL
                    {
                        softwares = db.Softwares.ToList();
                    } else if (id != null && produto == null && hd == null &&  ram == null && maquina == null)    // GET BY ID
                    {
                        softwares = db.Softwares.Where(s => s.MaquinaId == id).ToList();
                    } else if (id == null && produto != null && hd == null &&  ram == null && maquina == null)    // GET BY PRODUTO
                    {
                        softwares = db.Softwares.Where(s => s.Produto == produto).ToList();
                    } else if (id == null && produto == null && hd != null &&  ram == null && maquina == null)    // GET BY HARDDISK
                    {
                        softwares = db.Softwares.Where(s => s.HardDisk == hd).ToList();
                    } else if (id == null && produto == null && hd == null && ram != null && maquina == null)    // GET BY MEMORIA_RAM
                    {
                        softwares = db.Softwares.Where(s => s.MemoriaRam == ram).ToList();
                    } else if (id == null && produto == null && hd == null && ram == null && maquina != null)    // GET BY FK_MAQUINA
                    {
                        softwares = db.Softwares.Where(s => s.MaquinaId == maquina).ToList();
                    } else
                    {
                        throw new Exception("ERRO: Parâmetros inválidos");
                    }
                    Console.WriteLine($"\n=-=-=-=-=-=-=- Lista de Softwares =-=-=-=-=-=-=-\n");
                    
                    foreach (var software in softwares)
                    {
                        Console.WriteLine($"ID: {software.SoftwareId} | PRODUTO: {software.Produto} | HD: {software.HardDisk} | RAM: {software.MemoriaRam} | MAQUINA: {software.MaquinaId}");
                    }
                    Console.WriteLine($"=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
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

        public void Atualizar(int? id = null, string? produto = null, int? hd = null, int? ram = null, int? maquina = null)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var software = db.Softwares.Find(id) ?? throw new Exception($"Unable to find software {id}");
                    
                    software.Produto = produto ?? software.Produto;
                    software.HardDisk = hd ?? software.HardDisk;
                    software.MemoriaRam = ram ?? software.MemoriaRam;
                    software.MaquinaId = maquina ?? software.MaquinaId;
                    db.SaveChanges();
                    Console.WriteLine($"Máquina {id} atualizada com sucesso\n");
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
                    var software = db.Softwares.Find(id) ?? throw new Exception($"Unable to find software {id}");

                    db.Softwares.Remove(software);
                    db.SaveChanges();
                    Console.WriteLine($"Máquina {id} deletada com sucesso\n");
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