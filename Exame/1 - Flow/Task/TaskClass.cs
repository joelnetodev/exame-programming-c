using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//A taske é a promessa de um valor esperado, um retorno. Uma task pode registrar o callback quando o valor estiver pronto.
//Se o valor esperado vier do sistema, web, ou do banco de dados, não existe necessidade de usar thread e ficar esperando o valor, usa async e await num metodo task.

//A thread principal finaliza e a task continua rodando = thread de primeiro plano.

namespace ConsoleApp
{
    public class TaskClass
    {
        public static void TestarTask()
        {
            //Melhor usar TaskRun do que o Start, faz a mesma coisa, mas é possivel definir continuações, tempo de vida entre outras coisas, melhor administrado pelo ThreadPool.
            var task = Task.Run(() =>
            {
                for (int i = 0; i < 999; i++)
                {
                    Console.WriteLine("Task "+ i);
                }
            });

            //task.Wait();//Equivalente a Join de Thread, espera a task terminar para continuar
        }

        public static string TestarTaskReturn()
        {
            Task<string> task = Task.Run(() =>
            {
                return "Retorno Joel";
            });

            return task.Result;
        }

        //TaskPai e TaskFilha
        public static void TestarMultipleTasks()
        {
            //Multiple Tasks
            //Task[] tasks = new Task[2];//Poderia colcoar todas as tasks nesse array
            // Task.WaitAll(tasks);//E esperaria ate que todas terminassem

            string nome = "Antes da Multiple Task";
            Task task = Task.Run(() =>
            {
                nome = "Multiple Tasks com condições";
            });

            task.ContinueWith(currentTask =>
            {
                Console.WriteLine(nome);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);//Roda apenas quando a tarefa for concluida


            //Parent Tasks
            string taskString = "Antes da Parent Task";
            Task parentTask = Task.Run(() =>
            {
                Console.WriteLine("Parent Task");
                taskString = "1\n2\n3";
                
            });

            var taskChild = parentTask.ContinueWith(parent =>
            {
                Console.WriteLine("Child Task");
                Console.WriteLine(taskString);
            });

            task.Wait();
            taskChild.Wait();
        }

        //O TaskFactory inicia tasks com a mesma configuração que foi instanciado (LongRuning, Executar asynchrono, syncrono, mesmo Cancellation Token se houver)
        public static void TaskFactoryTest()
        {
            TaskFactory tf = new TaskFactory(TaskCreationOptions.None, TaskContinuationOptions.ExecuteSynchronously);
            
            tf.StartNew(() => { Console.WriteLine("Factory 1"); });
            tf.StartNew(() => { Console.WriteLine("Factory 2"); });
            tf.StartNew(() => { Console.WriteLine("Factory 3"); });
            tf.StartNew(() => { Console.WriteLine("Factory 4"); });
            tf.StartNew(() => { Console.WriteLine("Factory 5"); });
            tf.StartNew(() => { Console.WriteLine("Factory 6"); });
            tf.StartNew(() => { Console.WriteLine("Factory 7"); });
            tf.StartNew(() => { Console.WriteLine("Factory 8"); });
            tf.StartNew(() => { Console.WriteLine("Factory 9"); });
            tf.StartNew(() => { Console.WriteLine("Factory 10"); });
        }

        public static void TaskCancellationTest()
        {
            //Cancellation Source e token = passa pro parametro na factory ou na task e verifica se é pra ser cancelada
            CancellationTokenSource ctSource = new CancellationTokenSource();
            
            TaskFactory tf = new TaskFactory();
            var task1 = tf.StartNew(() =>
            {
                while (!ctSource.IsCancellationRequested)
                {
                    Console.WriteLine("*");
                    Thread.Sleep(1000);
                }
            }, ctSource.Token);

            Console.WriteLine("Pressione algo para sair...");
            Console.ReadLine();
            ctSource.Cancel();

            task1.Wait();
        }
    }
}