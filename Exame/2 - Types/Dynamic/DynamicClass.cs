using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._2___Types
{
    //objeto fracamente tipado, util para se trabalhar com DOM, COM objects Interop, JSON ou Reflection C#
    
    //Quando o compilador do c# encontra uma objeto dinamic, ele para  de checar o tipo, se metodo existe ou se contem certos argumentos.
    //O compilador guarda o conteudo do codigo para depois ser executado em runtime, não gera erro de compliação, mas pode gerar em execução

    public static class DynamicClass
    {

        public static void TestarDynamic()
        {
            var entities = new List<dynamic>();
            entities.Add(new {Coluna1 = 1, Coluna2 = "Joel"});
            entities.Add(new { Coluna1 = 2, Coluna2 = "Dadinho" });

            LerDynamics(entities);
        }

        private static void LerDynamics(List<dynamic> entities)
        {
            foreach (var entitieItem in entities)
            {
                Console.WriteLine(entitieItem.Coluna1 + " - " + entitieItem.Coluna2);
            }
        }
    }
    
}
