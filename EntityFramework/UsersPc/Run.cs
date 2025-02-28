using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UsersPc.Services;

namespace UsersPc
{
    public class Run
    {
        static void Main(string[] args)
        {
            CrudMaquina crudMaquina= new CrudMaquina();
            CrudSoftware crudSoftware= new CrudSoftware();
            CrudUser crudUser= new CrudUser();

            crudSoftware.Deletar(1);
            crudMaquina.Deletar(1);
            crudUser.Deletar(1);

            crudUser.Inserir(1, "THIAGO", "senha", 1234, "TI");
            crudMaquina.Inserir(1, "DESKTOP", 12, 500, 4, 6, 1);
            crudSoftware.Inserir(1, "LINUX", 50, 2, 1);

            crudUser.Listar();
            crudMaquina.Listar();
            crudSoftware.Listar();
        }
    }
}