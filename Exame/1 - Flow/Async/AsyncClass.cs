using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

//Operações I/O 
//Async quando chega num determinado ponto, o await obriga o CLR a esperar o retorno, 
//o código é executado por outra thread, deixando a principal livre (para realizar outra requisição, no caso ASP.NET, que trabalha apenas com uma thread)
//async sempre retorna uma Task, ou uma Task tipada, para usar await o dentro do metodo ele tem que ser async

namespace ConsoleApp
{
    public class AsyncClass
    {
        public static void TestarAsync()
        {
            Console.WriteLine("Await");
            TestarAsyncTwo();
            Console.WriteLine("Fim Await");
        }

        private static async Task TestarAsyncTwo()
        {
            var httpClient = new HttpClient();
            var stringAsync = await httpClient.GetStringAsync("https://www.google.com.br/");

            Console.WriteLine(stringAsync);
        }

        public static void TestarAsyncConfigureAwait()
        {
            Console.WriteLine("Await Configure false");
            TestarAsyncConfigureAwaitTwo();
            Console.WriteLine("Fim Await");
        }

        //Um programa Desktop usa uma thread UI e provavelmente varias outras por baixo para responsividade... já uma requisição web trabalha apenas com uma thread principal (a de requisição).
        //Usando o ConfigureAwait(false), ele abstrai o modo em que os dois tipos de aplicações trabalham, e retorna o código remanescente para a thread principal caso você precise atualizar algo na UI
        //Ou seja, quando se tem a resposta do await, em vez de executar o resto do codigo em outra thread, é executado na thread "principal".
        private static async Task TestarAsyncConfigureAwaitTwo()
        {
            var httpClient = new HttpClient();
            var stringAsync = await httpClient.GetStringAsync("https://www.google.com.br/").ConfigureAwait(false);

            Console.WriteLine(stringAsync);
        }
    }
}