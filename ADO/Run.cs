using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADO.Exemplo1;
using Npgsql;
using System.Diagnostics;

namespace ADO
{
    public class Run
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            TimeSpan tempoTotal;
            Crud c = new Crud();
            sw.Start();
            c.DeleteUser();
            sw.Stop();
            TimeSpan tempoDeleteTudo = sw.Elapsed;
            Console.WriteLine("Inserir: " + sw.ElapsedMilliseconds + "ms");

            sw.Start();
            c.InserirUser("Ronaldo", "Koliveira12");
            sw.Stop();
            TimeSpan tempoInsercao = sw.Elapsed;
            Console.WriteLine("Inserir: " + sw.ElapsedMilliseconds + "ms");

            sw.Start();
            c.LerUser("Ronaldo");
            sw.Stop();
            TimeSpan tempoLer = sw.Elapsed;
            Console.WriteLine("Ler: " + sw.ElapsedMilliseconds + "ms");

            sw.Start();
            c.DeleteUser(4);
            sw.Stop();
            TimeSpan tempoDelete = sw.Elapsed;
            Console.WriteLine("Delete: " + sw.ElapsedMilliseconds + "ms");

            sw.Start();
            c.AtualizarUser(1, "Ronaldo", "123456");
            sw.Stop();
            TimeSpan tempoAtualizar = sw.Elapsed;
            Console.WriteLine("Atualizar: " + sw.ElapsedMilliseconds + "ms");
            tempoTotal = tempoInsercao + tempoLer + tempoDelete + tempoAtualizar + tempoDeleteTudo;
            Console.WriteLine("Tempo total: " + tempoTotal.TotalMilliseconds + "ms");
            Console.WriteLine($"Média de tempo: {tempoTotal.TotalMilliseconds / 5}ms");
            
        }
        
        // static void Main(string[] args)
        // {
        //     string conexao = "Host=localhost;Database=digix-sql;Username=postgres;Password=olszewski";

        //     using (NpgsqlConnection con = new NpgsqlConnection(conexao))
        //     {
        //         try
        //         {
        //             con.Open();
        //             Console.WriteLine($"Conexão aberta com Postgres!");

        //             // Imprimindo o que está dentro do banco
        //             string query = "SELECT * FROM \"Users\"";

        //             // Criar o comando de inserção novo registro na tabela Users
        //             // string insertQuery = "INSERT INTO \"Users\" (\"Nome\", \"Password\") VALUES ('Thiago Olszewski', '123456')";


        //             // using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, con))
        //             // {
        //             //     int row = cmd.ExecuteNonQuery(); // Executa o comando que não retorna dados (Insert, Update, Delete)
        //             //     Console.WriteLine($"Inserido {row} registro(s) no banco de dados"); // Quantidade de linhas afetadas
        //             // }

        //             // string queryDelete = "DELETE FROM \"Users\" WHERE \"Nome\" = 'Thiago Olszewski'";
        //             // using (NpgsqlCommand cmd = new NpgsqlCommand(queryDelete, con))
        //             // {
        //             //     int row = cmd.ExecuteNonQuery(); // Executa o comando que não retorna dados (Insert, Update, Delet
        //             //     Console.WriteLine($"Deletado {row} registro(s) no banco de dados"); // Quant
        //             // }

        //             string queryUpdate = "UPDATE \"Users\" SET \"Nome\" = 'Thiago Olszewski' WHERE \"Nome\" = 'Thiago'";
        //             using (NpgsqlCommand cmd = new NpgsqlCommand(queryUpdate, con))
        //             {
        //                 int row = cmd.ExecuteNonQuery(); // Executa o comando que não retorna dados (Insert, Update, Delet
        //                 Console.WriteLine($"Atualizado {row} registro(s) no banco de dados"); // Quant
        //             }
                    
        //             // NpgsqlCommand representa o comando SQL que será executado no banco
        //             using(NpgsqlCommand cmd = new NpgsqlCommand(query, con))
        //             {
        //                 // Lendo e imprimindo os dados
        //                 using(NpgsqlDataReader reader = cmd.ExecuteReader())
        //                 {   // Representa o leitor de dados que irá ler os dados do banco
        //                     Console.WriteLine($"\nTabelas do banco de dados: ");
        //                     while (reader.Read()) // Enquanto há dados para serem lidos
        //                     {
        //                         // Imprime o nome da tabela, ou valor ca doluna 0
        //                         // Console.WriteLine(reader.GetString(0));
        //                         // Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, Password: {reader["Password"]}");    
        //                         Console.WriteLine($"ID: {reader.GetInt32(0)}, Nome: {reader.GetString(1)}, Password: {reader.GetString(2)}");    
                                
        //                     }
        //                 }
        //             }
        //         }
        //         catch (NpgsqlException ex)
        //         {
        //             Console.WriteLine($"ERROR: {ex.Message}");
        //         }
        //     }
        // }
    }
}