using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Classes_Partial.Services
{
    public partial class Classe
    {
        public string Nome {get; set;} = "Thiago";
        public void MetodoA() => Console.WriteLine($"Método A: {Idade}");
    }
}