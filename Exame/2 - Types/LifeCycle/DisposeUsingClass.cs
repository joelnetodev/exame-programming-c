using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._2___Types
{
    //Sempre que instancia um objeto, CLR aloca um espaço na memoria
    
    //Valores podem ser guardados em dois lugares na memoria: HEAP and STACK
    //STACK gaurda registro do que esta executando em codigo
    //HEAP guarda registro dos objetos
    //Para um objeto no HEAP, sempre há uma referencia apontando para o STACK.

    //STACK é limpo após o terminio do método, Value types são guardados geralmente no STACK, que é menor, menos custoso e não precisa de atenção do GARBAGE COLLECTOR

    //O GARBAGE COLLECTOR apenas limpa os itens de Geração 0 na memoria HEAP quando não tem espaço o suficiente.
    //Itens que ainda estão sendo referenciados são promovidos apra uma Geração mais alta.


    public static class DisposeUsingClass
    {
        //DataBase connections, File Handles, são exemplos de necessidade de manipular o ciclo de vida de um objeto (UnManagedResources).
        public static void TestarAguardarGarbage()
        {
            string pathFile = "arquivo.txt";
            StreamWriter streamW = new StreamWriter(pathFile);
            streamW.WriteLine("Linha 1 do arquivo.");
            streamW.WriteLine("Linha 2 do arquivo.");
            streamW.Flush();
            //O conteudo se encontra na memoria, Flush serve para escrever de fato e limpar os buffers
            Console.WriteLine("Arquivo Criado");

            streamW.Dispose();
            
            //Se não disposar daria erro, pois o arquivo esta sendo referenciado por outro objeto
            File.Delete(pathFile);
            Console.WriteLine("Arquivo Deletado");
        }

        public static void TestarDisposeInUsing()
        {
            //Usando using, Dispose sempre será chamado no final.
            string pathFile = "arquivo.txt";
            TipoFinalizador tipo;
            using (tipo = new TipoFinalizador())
            {
                Console.WriteLine("Arquivo Criado");
            }

            //Garbage axecuta quando esta prestes a liberar um objeto que não esta mais sendo usado
            //Isso aumenta o ciclo de vida do objeto, pois executa mais codigo dentro do ~finalizer
            //Collect coleta tudo o lixo e libera memória
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //Wait espera a thread para que todos os finalizers sejam terminados

            File.Delete(pathFile);
            Console.WriteLine("Arquivo Deletado");
        }











        private class TipoFinalizador : IDisposable
        {
            public StreamWriter StreamWriterObj { get; private set; }

            public TipoFinalizador()
            {
                string pathFile = "arquivo.txt";
                StreamWriterObj = new StreamWriter(pathFile);
                StreamWriterObj.WriteLine("Linha 1 do arquivo.");
                StreamWriterObj.WriteLine("Linha 2 do arquivo.");
                StreamWriterObj.Flush();
            }

            //Finalizer, será executado quando garbage colletor definir que este item está pronto par a limpeza.
            ~TipoFinalizador()
            {
                StreamWriterObj.Dispose();
            }

            public void Dispose()
            {
                StreamWriterObj.Dispose();           
            }
        }
    } 
}
