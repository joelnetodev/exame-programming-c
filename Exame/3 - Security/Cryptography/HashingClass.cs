using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace ConsoleApp
{
    public static class HashingClass
    {
        //Hashing é o processo de mapear uma grande quantidade de dados em uma pequena de tamanho fixo.
        
        //Em vez de procurar o item pelo objeto, é comparado o hash
        //HashTable e Dictionary fazem busca pelo hash.

        //(HashTable não é generico, Dictionary sim)
        

        public static void TestarHash()
        {
            var vaso1 = new Vaso { Id = 1, Nome = "Vaso1", DataFabricacao = DateTime.Now };
            var vaso2 = new Vaso { Id = 2, Nome = "Vaso2", DataFabricacao = DateTime.Now };
            var vaso3 = new Vaso { Id = 3, Nome = "Vaso3", DataFabricacao = DateTime.Now };

            List<Vaso> listaVaso = new List<Vaso> { vaso1, vaso2, vaso3 };

            foreach (var vaso in listaVaso)
            {
                string vasoStr = String.Empty;
                if (vaso1.Equals(vaso))
                {
                    vasoStr = "1";
                }
                else if (vaso2.Equals(vaso))
                {
                    vasoStr = "2";
                }
                else if (vaso3.Equals(vaso))
                {
                    vasoStr = "3";
                }

                Console.WriteLine("Encontrado Vaso " + vasoStr);
            }
        }


        //Quanto mais seguro seu hash e rondomico, mais seguro é a sua utilização
        public static void TestarSha256()
        {
            Vaso vaso1 = new Vaso { Id = 1, Nome = "Vaso1", DataFabricacao = DateTime.Now };
            Vaso vaso2 = new Vaso { Id = 1, Nome = "Vaso1", DataFabricacao = DateTime.Now };

            //.NET possui várias bibliotecas para hash
            UnicodeEncoding unicode = new UnicodeEncoding();
            SHA256 sha256 = SHA256.Create();

            byte[] hashCrip1 = sha256.ComputeHash(unicode.GetBytes(vaso1.ToString()));
            byte[] hashCrip2 = sha256.ComputeHash(unicode.GetBytes(vaso2.ToString()));

            var teste = (hashCrip1.SequenceEqual(hashCrip2));

            var mensagem = teste ? "Sequencias byte de vaso 1 e 2 são iguais." : "Sequencias byte não batem";
            Console.WriteLine(mensagem);

        }

        public class Vaso
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public DateTime DataFabricacao { get; set; }

            //Reescreve o GetHashCode com calculos com numeros primos e identificações distintas, para não haver objetos diferentes com mesmo hash
            public override int GetHashCode()
            {
                //Uncheked serve para desconsiderar exceção de estouro de tamanho de int, caso ocorra
                unchecked
                {
                    int hash = (Id ^ 3) % (Nome.GetHashCode() + DataFabricacao.GetHashCode());

                    return hash;
                }
            }

            public override bool Equals(object obj)
            {
                return obj.GetHashCode() == this.GetHashCode();
            }

            public override string ToString()
            {
                return GetHashCode().ToString();
            }
        }
    }
}
