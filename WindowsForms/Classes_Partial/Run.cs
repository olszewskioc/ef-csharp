using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classes_Partial.Services;

namespace Classes_Partial
{
    public class Run
    {
        static void Main(string[] args)
        {
            Classe classe = new Classe();

            classe.MetodoA();
            classe.MetodoB();
        }
    }
}