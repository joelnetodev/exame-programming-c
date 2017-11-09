using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.EventsDelegates
{
    //Delegates são tipos que definem a assinatura dos metodos. Servem também para guardar métodos anônimos encapsulados na hora ex: () => { string nome = "jose";})
    //Delegates são a base para construção de eventos, pode apontar o delegate para um metodo e invocar atravez do delegate
    //Action é um tipo delegates que assume um metodo void; e Fun<T> é um delegate que retorna um valor.
    public class DelegateClass
    {
        private delegate void DelegateVoidParametro(int numero);

        private delegate string DelegateString();

        private static void VerificarSeParOuImpar(int numero)
        {
            string parImparResult;

            parImparResult = numero % 2 == 0 ? "Par" : "Impar";

            Console.WriteLine("Numero é " + parImparResult);
        }

        private static string MetodoString1()
        {
            Console.WriteLine("Método string delegate 1");
            return "retornou 1";
        }

        private static string MetodoString2()
        {
            Console.WriteLine("Método string delegate 2");
            return "retornou 2";
        }

        public static void TestarDelegate()
        {
            Console.WriteLine("Escreva um número");
            string numeroStr = Console.ReadLine();

            int numero = Convert.ToInt32(numeroStr);

            DelegateVoidParametro delegParImpar = new DelegateVoidParametro(VerificarSeParOuImpar);
            delegParImpar(numero);
        }

        public static void TestarMultipleDelegate()
        {
            Console.WriteLine("Multiple Delegate");

            //+= para atribuir mais metodos domesmo tipo e com mesmos parametros ao delegate
            DelegateString deleg = new DelegateString(MetodoString1);
            deleg += MetodoString2;

            //Percorro lista de metodos e chamo um por vez
            foreach (var invocation in deleg.GetInvocationList())
            {
                var teste = invocation.DynamicInvoke();
                Console.WriteLine(teste);
            }
        }

        //Atribuo um método criado em runtime para o delegate de string e de void parametro inteiro
        public static void TestarLambdaDelegate()
        {
            DelegateString delegString = () => "Teste Lambda String";
            Console.WriteLine(delegString());
            
            DelegateVoidParametro delegVoid1 = (x) => Console.WriteLine(x * 10);
            Console.WriteLine("Digite um numero para multiplicar por 10");
            delegVoid1(Convert.ToInt32(Console.ReadLine()));

            //Para uma variável fora da expressão, o compilador cria uma variável para o escopo da expressão, contendo o mesmo valor que foi setado antes de inicar o método e ela o mesmo tempo de vida do método
            bool teste = false;
            DelegateVoidParametro delegVoid2 = (x) =>
            {
                teste = true;
                for (int i = 0; i <= x; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine(i);
                }
            };

            Console.WriteLine("Digite um numero para ver contando até ele");
            delegVoid2(Convert.ToInt32(Console.ReadLine()));

            if(teste) 
                Console.WriteLine("bool true");
            else
                Console.WriteLine("bool false");

        }

        //Event não pode ser atribuido com o perador =, apenas com +=
        public static void TestEventAction()
        {
            Console.WriteLine("Teste Action OnGet e OnChanged");

            ActionEventClass.Texto1 = "Número alterado!";

            string textoLocal = "Obtendo número... ";
            ActionEventClass.OnGetAction = (int n) => { Thread.Sleep(1000); Console.WriteLine(textoLocal + n); Thread.Sleep(1000); };

            //Fora da classe não pode atribuir um valor nulo para um evento, mas na própria classe sim
            ActionEventClass.OnSetedEvent += (string t) => { Thread.Sleep(1000); Console.WriteLine(t); };

            ActionEventClass.Numero = 1;
            var numero = ActionEventClass.Numero;

            ActionEventClass.Numero = 2;
            numero = ActionEventClass.Numero;

            ActionEventClass.Numero = 3;
            numero = ActionEventClass.Numero;

            //Nada impede de executar a action atravez da classe
            //ActionEventClass.OnGetAction(1);
        }

        private class ActionEventClass
        {
            public static string Texto1;

            private static int _numero;

            public static int Numero
            {
                get
                {
                    if (OnGetAction != null) OnGetAction(_numero);
                    return _numero;
                }
                set
                {
                    _numero = value;
                    OnSetedEvent(Texto1);
                }
            }

            //Action com parametros 0 <= 16 <string, int, object>
            public static Action<int> OnGetAction { get; set; }

            public static event Action<string> OnSetedEvent = delegate //{ }; 
            {
                Console.WriteLine("Método pré-definido do OnSetedEvent");
            };


            //OBS...
            //Posso declarar um delegate e dizer que o evento é do tipo desse delegate
            private delegate void DelegateMetodo();
            private event DelegateMetodo EventoDelegate;
            private void TesteEvento()
            {
                EventoDelegate += TesteEvento;
            }

        }
    }
}
