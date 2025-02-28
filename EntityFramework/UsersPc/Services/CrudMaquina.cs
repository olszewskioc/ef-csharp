using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersPc.Data;
using UsersPc.Models;
using Npgsql;

namespace UsersPc.Services
{
    public class CrudMaquina
    {
        public void Inserir(int id, string tipo, int velocidade, int hd, int rede, int ram, int usuario)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Maquinas.Add(new Maquina {MaquinaId = id, Tipo = tipo, Velocidade = velocidade, HardDisk = hd, PlacaRede = rede, MemoriaRam = ram, UsuarioId = usuario });
                    db.SaveChanges();
                    Console.WriteLine($"Máquina {tipo} criada com sucesso\n");
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
        public void Listar(int? id = null, string? tipo = null, int? velocidade = null, int? hd = null, int? rede = null, int? ram = null, int? usuario = null)
        {
            try
            {
                var maquinas = new List<Maquina>();
                using (var db = new AppDbContext())
                {
                    if (id == null && tipo == null && velocidade == null && hd == null && rede == null && ram == null && usuario == null)   // GET ALL
                    {
                        maquinas = db.Maquinas.ToList();
                    } else if (id != null && tipo == null && velocidade == null && hd == null && rede == null &&  ram == null && usuario == null)    // GET BY ID
                    {
                        maquinas = db.Maquinas.Where(m => m.MaquinaId == id).ToList();
                    } else if (id == null && tipo != null && velocidade == null && hd == null && rede == null &&  ram == null && usuario == null)    // GET BY TIPO
                    {
                        maquinas = db.Maquinas.Where(m => m.Tipo == tipo).ToList();
                    } else if (id == null && tipo == null && velocidade != null && hd == null && rede == null &&  ram == null && usuario == null)    // GET BY VELOCIDADE
                    {
                        maquinas = db.Maquinas.Where(m => m.Velocidade == velocidade).ToList();
                    } else if (id == null && tipo == null && velocidade == null && hd != null && rede == null &&  ram == null && usuario == null)    // GET BY HARDDISK
                    {
                        maquinas = db.Maquinas.Where(m => m.HardDisk == hd).ToList();
                    } else if (id == null && tipo == null && velocidade == null && hd == null && rede != null &&  ram == null && usuario == null)    // GET BY PLACA_REDE
                    {
                        maquinas = db.Maquinas.Where(m => m.PlacaRede == rede).ToList();
                    } else if (id == null && tipo == null && velocidade == null && hd == null && rede == null &&  ram != null && usuario == null)    // GET BY MEMORIA_RAM
                    {
                        maquinas = db.Maquinas.Where(m => m.MemoriaRam == ram).ToList();
                    } else if (id == null && tipo == null && velocidade == null && hd == null && rede == null &&  ram == null && usuario != null)    // GET BY FK_USUARIO
                    {
                        maquinas = db.Maquinas.Where(m => m.UsuarioId == usuario).ToList();
                    } else
                    {
                        throw new Exception("ERRO: Parâmetros inválidos");
                    }
                    Console.WriteLine($"\n=-=-=-=-=-=-=- Lista de Máquinas =-=-=-=-=-=-=-\n");
                    
                    foreach (var maquina in maquinas)
                    {
                        Console.WriteLine($"ID: {maquina.MaquinaId} | TIPO: {maquina.Tipo} | VELOCIDADE: {maquina.Velocidade} | HD: {maquina.HardDisk} | RAM: {maquina.MemoriaRam} | REDE: {maquina.PlacaRede} | USER: {maquina.UsuarioId}");
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

        public void Atualizar(int? id = null, string? tipo = null, int? velocidade = null, int? hd = null, int? rede = null, int? ram = null, int? usuario = null)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var maquina = db.Maquinas.Find(id) ?? throw new Exception($"Unable to find maquina {id}");
                    
                    maquina.Tipo = tipo ?? maquina.Tipo;
                    maquina.Velocidade = velocidade ?? maquina.Velocidade;
                    maquina.HardDisk = hd ?? maquina.HardDisk;
                    maquina.PlacaRede = rede ?? maquina.PlacaRede;
                    maquina.MemoriaRam = ram ?? maquina.MemoriaRam;
                    maquina.UsuarioId = usuario ?? maquina.UsuarioId;
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
                    var maquina = db.Maquinas.Find(id) ?? throw new Exception($"Unable to find máquina {id}");

                    db.Maquinas.Remove(maquina);
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