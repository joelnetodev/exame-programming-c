using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._2___Types
{

    //Generic Types
    // where T: struct tem que ser um struct
    // where T: class tem que ser uma classe
    // where T: new() tem que ter um construtor
    // where T: Interface ou Base Classe especifico
    // where T: U outro tipo generico que vai ser definido

    public static class GenericTypesClass
    {
        public static void TestarGeneric()
        {
            var generic = new GeneriType<CPFClass>();

            generic.Add(new CPFClass("1123456"));
            generic.Add(new CPFClass("2123456"));
            generic.Add(new CPFClass("3123456"));

            for (int i = 1; i <= generic.Count; i++)
            {
                Console.WriteLine(generic[i].Numero);
            }

            var iGeneric = new RegisterGeneriType<CPFClass, ICPFClass>();
            iGeneric.GetInstanceType.SetCPF("36806030459");
            Console.WriteLine(iGeneric.GetInstanceType.Numero);
        }

        //Classe generica de T, herda de collection
        private class GeneriType<T> : Collection<T> where T : class, new()
        {
            public new T this[int x]
            { get { return base[x - 1]; } }

        }

        //Tipo generico que recebe um classe do tipo de outra classe
        //Igual a inversão de controle de Interface para Classe no Castle, Ninject etc.
        private class RegisterGeneriType<T, U> where T : U, new()
        {
            private U value;
            public U GetInstanceType { get { return value; } }

            //public void 

            public RegisterGeneriType()
            {
                //Para criar uma nova instancia de U, T precisa ser U e habilitar da palavra new()
                U u = new T();
                value = u;
            }
        }


        private class CPFClass : ICPFClass
        {
            public string Numero { get; set; }

            public void SetCPF(string cpf)
            {
                Numero = cpf;
            }

            //Se eu remover esse construtor, da erro na classe generica
            public CPFClass()
            {
                
            }

            public CPFClass(string numero)
            {
                Numero = numero;
            }
        }

        private interface ICPFClass
        {
            string Numero { get; set; }
            void SetCPF(string cpf);
        }
    }
}
