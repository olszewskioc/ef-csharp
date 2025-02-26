using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Exemplo1.Services;

namespace Exemplo1
{
    public class Run
    {
        static void Main(string[] args)
        {
            Crud crud= new Crud();
            
            TimeSpan totalTime = new();
            Stopwatch sw = new();

            Console.WriteLine($"Inserir");
            sw.Start();
            crud.InserirUsuario(1, "Thiago", "thiago@digix.com");
            sw.Stop();
            TimeSpan timeInserir = sw.Elapsed;
            Console.WriteLine($"{timeInserir.TotalMilliseconds}ms");
            Console.WriteLine("------------------------");
            Console.WriteLine($"Listar");
            sw.Start();
            crud.ListarUsuarios();
            sw.Stop();
            TimeSpan timeListar = sw.Elapsed;
            Console.WriteLine($"{timeListar.TotalMilliseconds}ms");
            Console.WriteLine("------------------------");
            Console.WriteLine($"Atualizar");
            sw.Start();
            crud.AtualizarUsuario(1, "Thiago Olszewski", "thiago.olszewskioc@gmail.com");
            sw.Stop();
            TimeSpan timeAtualizar = sw.Elapsed;
            Console.WriteLine($"{timeAtualizar.TotalMilliseconds}ms");
            Console.WriteLine("------------------------");
            Console.WriteLine($"Deletar");
            sw.Start();
            crud.DeleteUsuario(1);
            sw.Stop();
            TimeSpan timeDelete = sw.Elapsed;
            Console.WriteLine($"{timeDelete.TotalMilliseconds}ms");
            Console.WriteLine("------------------------");
            Console.WriteLine($"Listar");
            sw.Start();
            crud.ListarUsuarios();
            sw.Stop();
            TimeSpan timeListE = sw.Elapsed;
            Console.WriteLine($"{timeListE.TotalMilliseconds}ms");
            Console.WriteLine("------------------------");
            totalTime = timeAtualizar + timeDelete + timeListE + timeListar + timeInserir;
            Console.WriteLine($"\nTotal: {totalTime.TotalMilliseconds}ms\nMédia: {totalTime.TotalMilliseconds / 5}ms");
            
        }
    }
}