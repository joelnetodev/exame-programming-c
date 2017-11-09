using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

//existem tres tipos de Types em C#
//Value Types
//Reference Types
//Pointer Types

//Reference Typos contem referencia ao valor
//Value Types contem o valor direto

//Valores podem ser guardados em dois lugares na memoria: heap and Stack

//Value types são guardados geralmente no Stack que é menor, menos cutoso e não precisa de atenção do garbage collector 

namespace ConsoleApp
{
    //Special value types C# Enum
    [Flags]
    internal enum DiaDaSemanaEnum
    {
        Domingo = 0,
        Segunda = 1,
        Terca = 2,
        Quarta = 4,
        Quinta = 8,
        Sexta = 16,
        Sabado = 32,

        Teste = 5
    }

    //Todos os obetos em c# herdam de System.Object exceto Value Types que herdam de System.ValueTypes
    //Struc cria um novo Value Type, pois voc~e não pode herdar de System.ValueTypes
    //Sctruc tem que ser pequenos e imutaveis, se não, usa-se Reference Type
    //Quando passados por parametros a um metodos, é feita uma copia dos dados, o original não muda.
    struct Pointer
    {
        public int X;
        public int Y;
    }

    public class ValueTypesClass
    {
        public static void TestarEnum()
        {
            var diaSemana = DiaDaSemanaEnum.Domingo;
            Console.WriteLine(diaSemana.ToString());

            //Pode ser atribuido mais de um valor para o enum.
            var diasPossiveis = DiaDaSemanaEnum.Segunda | DiaDaSemanaEnum.Quarta;

            //O Enum não se torna uma lista de dois valores, ele se torna dois valores
            if (diasPossiveis == (DiaDaSemanaEnum.Segunda | DiaDaSemanaEnum.Quarta))
            {
                Console.WriteLine(DiaDaSemanaEnum.Segunda.ToString());
                Console.WriteLine(DiaDaSemanaEnum.Quarta.ToString());
            }

            //Mas tbm, se existir um meso valor com esse ID, ele se torna esse enum também
            if (diasPossiveis == DiaDaSemanaEnum.Teste)
            {
                Console.WriteLine(DiaDaSemanaEnum.Teste.ToString());
            }
        }


    }
}