using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._2___Types.BaseTypes
{

    public static class BaseTypeClass
    {
        public static void TestarComparable()
        {
            var lista = new List<ComparationClass>();
            lista.Add(new ComparationClass { Id = 2, Nome = "B" });
            lista.Add(new ComparationClass { Id = 1, Nome = "A" });
            lista.Add(new ComparationClass { Id = 3, Nome = "C" });
            lista.Add(new ComparationClass { Id = 10, Nome = "J" });
            lista.Add(new ComparationClass { Id = 5, Nome = "E" });

            //O sort chama o metodo de comparação
            lista.Sort();

            Console.WriteLine("Lista Ordenada");
            lista.ForEach(x => Console.WriteLine(x.Nome));
        }

        public static void TestarEquatable()
        {
            var lista = new List<EquatableClass>();
            lista.Add(new EquatableClass { Id = 1, Nome = "A" });
            lista.Add(new EquatableClass { Id = 2, Nome = "B" });
            lista.Add(new EquatableClass { Id = 4, Nome = "C" });
            lista.Add(new EquatableClass { Id = 4, Nome = "C" });

            var equa3 = new EquatableClass { Id = 2, Nome = "A" };

            var result = lista.Contains(equa3);

            //O contains usa o equatable para verificar se os objetos são iguais
            Console.WriteLine("Contains");
            Console.WriteLine("Contem {0}: {1}", equa3, result);

            //O distinct usa o equatable e gethashcode para verificar se dicionário hash é igual
            Console.WriteLine("Distinct");
            lista.Distinct().ToList().ForEach(Console.WriteLine);
        }



        //IComparalble serve pra oedenar objetos
        //sobresvrece o metodo e ordena pelo Nome
        private class ComparationClass : IComparable<ComparationClass>
        {
            public int Id { get; set; }

            public string Nome { get; set; }

            public int CompareTo(ComparationClass other)
            {
                if (other == null) return 1;

                return Nome.CompareTo(other.Nome);
            }
        }

        //IEquatable serve pra comparar objetos
        //A diferença de apenas sobrescrever o Equals é que não precisa fazer o box e unboxing
        private class EquatableClass : IEquatable<EquatableClass>
        {
            public int Id { get; set; }

            public string Nome { get; set; }

            public bool Equals(EquatableClass other)
            {
                var result = other != null && other.Id == this.Id;

                return result;
            }

            //O gethashcode é para o distinct verificar se o dicionário de hash é igual
            public override int GetHashCode()
            {
                return Id;
            }

            public override string ToString()
            {
                return string.Format("Id {0} Nome {1}", Id, Nome);
            }
        }
    }
}

