using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


//Podemos aproveitar o poder de processamento de CPUs que possuem mais de um núcleo, 
//pois podemos programar para que cada thread tenha o seu processamento realizado por núcleos diferentes. 

//Mas é sempre importante avaliar corretamente a utilização de threads, 
//pois a concorrência poderá causar deadlocks e funcionamento incorreto de um procedimento.

//A leitura se torna mais rápida usando mais de um núcleo para cada thread.

//O WPF usa uma thread para UI e uma ou possivelmente mais de uma rodando por baixo para binding entre outras coisas


namespace ConsoleApp
{
    public class ThreadClass
    {
        public static bool IsStop { get; set; }

        //Com esse atributo, para cada thread, a CLR cria uma nova variavel
        [ThreadStatic]
        public static int Quantidade;

        public static void TestarThread()
        {
            //Background sempre é false, significa que a thread é primeiro plano. 
            //Uma Thread de primeiro plano impede que uma de segundo prossiga se ela for encerrada. Já uma threads de segundo plano (true) não impede o que uma de primeiro prossiga, finalizando a execução. 

            var thread = new Thread(() =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("Thread count: " + i);
                    Thread.Sleep(500);
                }
            });

            thread.IsBackground = false;
            thread.Start();

            //thread.Join();//Significa que o ate esse ponto o metodo vai esperar a thread terminar.

        }

        public static void ThreadParametro()
        {
            Console.WriteLine("Digite um numero:");
            int numero = Convert.ToInt32(Console.Read());

            ThreadClass.IsStop = false;

            var thread = new Thread(ThreadClass.TestarThreadParametro);
            thread.Start(numero);

            Thread.Sleep(3000);
            ThreadClass.IsStop = true;
        }

        //Thread so recebe metodo que o parametro seja um object
        private static void TestarThreadParametro(object pNumeroFim)
        {
            Console.WriteLine("O número foi: " + pNumeroFim);

            int count = 0;
            while (IsStop == false)
            {
                Thread.Sleep(1000);
                count++;
            }

            Console.WriteLine("Aguardei por: " + count + " segundos...");
        }

        public static void TestarThreadAtributo()
        {
            var thread1 = new Thread(() =>
            {
                for (int i = 0; i < 9999999; i++)
                {
                    Quantidade += 1;
                    Console.WriteLine("Primeira thread: " + Quantidade);
                    Thread.Sleep(500);
                }
            });
            thread1.Start();



            var thread2 = new Thread(() =>
            {
                for (int i = 0; i < 99999999; i++)
                {
                    Quantidade += 1;
                    Console.WriteLine("Segunda thread: " + Quantidade);
                    Thread.Sleep(500);
                }
            });

            thread2.Start();
        }
    }
}
