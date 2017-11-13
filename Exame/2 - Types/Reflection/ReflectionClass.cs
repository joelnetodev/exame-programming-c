using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp._2___Types
{
    //Attribute podem ser atribuidos a qualque type em C#, não exstem apenas os dados, existem também os MetaDados
    //Atributos são formas poderosas de adicionar metadados a aplicação

    [CustomAttribute("Teste 1"), CustomAttribute("Teste 2")]
    public static class ReflectionClass
    {
        //O Assembly.ReflectionOnlyLoad significa que não pode instanciar classe nem executar método do assembly
        //Já o Load permite (O Assembly.LoadFrom é pra ler assembly de uma cadeia de bytes)

        public static void TestarAtributo()
        {
            if (Attribute.IsDefined(typeof(ReflectionClass), typeof(CustomAttribute)))
            {
                Console.WriteLine("Existe Atributo");
            }

            var atributos = typeof(ReflectionClass).GetCustomAttributes(typeof(CustomAttribute), false);
            foreach (var atributo in atributos)
            {
               Console.WriteLine(((CustomAttribute)atributo).Parametro);
            }
        }

        public static void TestarReflectionExecutarMetodo()
        {
            //Carrego a dll corrente em que o codigo esta sendo executado 
            //Carrego todas as dlls do dominio da aplicação
            var assembly = Assembly.Load("ConsoleApp");
            var localAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            //Pego todos os tipos, e executo um metodo especifio
            var types = assembly.GetTypes();
            Type type = types.First(x => x.Name == "ReflectionClass");
            MethodInfo methodInfo = type.GetMethod("TestarAtributo");
            methodInfo.Invoke(type, null);
        }

        public static void TestarReflectionInstanciarClasse()
        {
            
            var assembly = Assembly.Load("ConsoleApp");
            

            var types = (from t in assembly.GetTypes() 
                             where t == typeof(ClassePessoaFisica)
                             select  t);

            foreach (var type in types)
            {
                var instancia = Activator.CreateInstance(type);

                foreach (var field in instancia.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                {
                    Console.WriteLine(field.Name + ": " + field.GetValue(instancia));
                }
            }
        }


        //Definição do atributo, se é de classe, de metodo, se permite mais de 1
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
        private sealed class CustomAttribute : Attribute
        {
            public string Parametro { get; set; }

            public CustomAttribute(string parametro)
            {
                Parametro = parametro;
            }
        }

    }
    
}