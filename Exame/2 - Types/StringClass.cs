using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp
{
    //string é um objeto do tipo String que contém um array de char (char[]) é um Reference type que parece com um value type
    //o == e o != são sobrescritos para comparar valores, não referencia.

    public static class StringClass
    {
        public static void TestarString()
        {
            //Strings são imutaveis, por isso todos os métodos retornam uma nova string
            //cada vez que você altera uma string ela não é substituida, é criada uma nova 
            //e a variavel fica com o ultimo valor atribuido 
            
            string nome = String.Empty;
            for (int i = 0; i < 10000; i++)
            {
                //Esse codigo cria uma nova string cada vez que entra no loop = 10.000
                nome += "x";
            }
        }

        public static void TestarStringBuilder()
        {
            //Internatemente ele usa um string buffer para concatenar e melhorar a performance
            //em vez de criar uma nova string sempre
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                sBuilder.Append("x");
            }

            Console.WriteLine(sBuilder);

            Console.WriteLine(sBuilder[0]);
        }

        public static void TestarSearch()
        {
            string teste = "String de exemplo para testar";

            if(teste.StartsWith("S"))
                Console.WriteLine("S Sim");

            if (teste.EndsWith("r"))
                Console.WriteLine("r Sim");

            int index = teste.IndexOf("t", StringComparison.CurrentCulture);
            Console.WriteLine("F Index: " + index);
            index = teste.LastIndexOf("t", StringComparison.CurrentCulture);
            Console.WriteLine("L Index: " + index);

            Console.WriteLine("Substring: " + teste.Substring(10, 7));
        }

        //new para esconder o metodo toString e criar um novo 
        public static new string ToString()
        {
            return "Classe de testes com strings";
        }
        
        //Conversão
        public static void TestarConversaoToString()
        {
            double valor = 1234.89;
            Console.WriteLine(valor.ToString("C", new System.Globalization.CultureInfo("pt-BR")));

            var data = DateTime.Now;
            Console.WriteLine(data.Date.ToString("d", new System.Globalization.CultureInfo("pt-BR")));
        }
    }
}
