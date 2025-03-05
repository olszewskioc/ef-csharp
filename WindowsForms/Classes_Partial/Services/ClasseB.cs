using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Classes_Partial.Services
{
    public partial class Classe
    {
        public int Idade {get; set;} = 22;
        public void MetodoB() => Console.WriteLine($"Método B: {Nome}");
    }
}