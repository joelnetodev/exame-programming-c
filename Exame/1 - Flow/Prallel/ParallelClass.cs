using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

//Paralelismo

namespace ConsoleApp
{
    public class ParallelClass
    {
        //o modificador volatile manda buscar semrep o ultimo valor da variável
        //Ex: var valor = 5; Console.Write(valor); em cache isso ficaria Console.Write(5);
        //Ex: mas antes de mostrar esse valor pode mudar em outra thread, o volatile garante que vai ser ultilizado sempre o valor mais recente da variável.
        private static volatile int _numeroVolatil;

        public static void LockedAndVolatileVariablesTest()
        {
            var _lock = new object();
            var numero = 0;

            TaskFactory tf = new TaskFactory(TaskCreationOptions.None, TaskContinuationOptions.None);

            var task1 = tf.StartNew(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    _numeroVolatil++;
                    //Dentro do Lock, a thread aguarda esse statement terminar para continuar, significa que o acesso é ThreadSafe
                    //Também pode ser usado o Monitor.Enter e o Monitor.Stop
                    Monitor.Enter(_lock);
                    Monitor.Exit(_lock);
                    lock (_lock)
                    {
                        numero++;
                    }
                }
            });

            for (int i = 0; i < 10000000; i++)
            {
                _numeroVolatil--;

                lock (_lock)
                {
                    numero--;
                }
            }

            task1.Wait();

            Console.WriteLine("Numero lock: " + numero);

            var task2 = tf.StartNew(() =>
            {
                for (int i = 0; i < 100000000; i++)
                {
                    _numeroVolatil++;
                }
            });

            for (int i = 0; i < 100000000; i++)
            {
                _numeroVolatil--;
            }

            task2.Wait();

            Console.WriteLine("Numero volátil: " + _numeroVolatil);
        }

        public static void TestarLinqParallel()
        {
            //PLINQ - Parallel Language Integration Query
            //Fazer consultas usando PLINQ (com dados carregados na memória) ajuda na performance.

            List<string> stringList = new List<string>();

            for (int i = 0; i < 10000000; i++)
            {
                stringList.Add("Nome Joel");
            }

            for (int i = 0; i < 10000000; i++)
            {
                stringList.Add("Nome Silva");
            }

            for (int i = 0; i < 10000000; i++)
            {
                stringList.Add("Nome Lucena");
            }
            
            for (int i = 0; i < 10000000; i++)
            {
                stringList.Add("Nome João");
            }

            for (int i = 0; i < 10000000; i++)
            {
                stringList.Add("Nome Cinthia");
            }

            for (int i = 0; i < 10000000; i++)
            {
                stringList.Add("Nome Cinthia");
            }

            for (int i = 0; i < 10000000; i++)
            {
                stringList.Add("Nome Pacheco");
            }

            for (int i = 0; i < 1; i++)
            {
                stringList.Add("Rita");
            }

            for (int i = 0; i < 10000000; i++)
            {
                stringList.Add("Nome Thais");
            }

            for (int i = 0; i < 10000000; i++)
            {
                stringList.Add("Nome Pedro");
            }

            //Cast para parallel
            var result = stringList.AsParallel().Where(x => x == "Rita").ToList();

            //Posso interar sobre a lista e em paralelo ler o resultado
            var result2 = stringList.AsParallel().Where(x => x == "Rita");
            result2.ForAll(Console.WriteLine);

            if(result.Any())
                Console.WriteLine("Yes");
        }

        //Thread-Safe - internamente usam sincroniação para que não sejam acessadas por várias threads ao mesmo tempo
        public static void TestarConcurrentLists()
        {
            //Adiciona item a coleção, para ler usa o take em ordem de inclusão, quando usa o take, o item é removido
            BlockingCollection<string> blockingCollection = new BlockingCollection<string>();
            ConcurrentBag<string> concurrentBag = new ConcurrentBag<string>();
            

            ConcurrentDictionary<int, string> concurrentDictionary = new ConcurrentDictionary<int, string>();
            //Queue Fila.. FIFO, First In First Out
            ConcurrentQueue<string> concurrentQueue = new ConcurrentQueue<string>();
            //Stack Pilha.. LIFO Last In First Out
            ConcurrentStack<string> concurrentStack = new ConcurrentStack<string>();

            Console.WriteLine("ConcurrentBag");
            Task writeTask = Task.Run(() =>
            {
                bool continua = true;
                while (continua)
                {
                    string digito = Console.ReadLine();
                    if (digito == "0") continua = false;
                    concurrentBag.Add(digito);
                }
            });

            Task readTask = Task.Run(() =>
            {
                while (true)
                {
                    string item;
                    if (concurrentBag.Any())
                    {
                        Console.WriteLine(concurrentBag.LastOrDefault());
                    }                 
                }
            });
            
            //Compilador espera a task write terminar, enquanto isso, a task reade continua rodando, mas so mostra se conseguir pegar o valor de blocking.
            writeTask.Wait();
        }
    }
}