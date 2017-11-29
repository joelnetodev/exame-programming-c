using System;
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
            lista.Add(new ComparationClass { Id = 2 });
            lista.Add(new ComparationClass { Id = 1 });
            lista.Add(new ComparationClass { Id = 3 });
            lista.Add(new ComparationClass { Id = 10 });
            lista.Add(new ComparationClass { Id = 5 });

            lista.Sort();

            Console.WriteLine("Lista Ordenada");
            lista.ForEach(x => Console.WriteLine(x.Id));
        }

        //IComparalble serve pra oedenar objetos
        //sobresvrece o metodo e ordena pelo ID
        private class ComparationClass : IComparable<ComparationClass>
        {
            public int Id { get; set; }

            public int CompareTo(ComparationClass other)
            {
                if (other == null) return 1;

                return Id.CompareTo(other.Id);
            }
        }
    }
}

