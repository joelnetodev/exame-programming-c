using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace ConsoleApp
{
    public static class TraceLogEventClass
    {
        //Em um ambiente de produção ou distribuído o debug não é a melhor opção para ver erros e rastrear eventos
        //Para isso existe a estrategia de logging and tracing que é uma maneira de monitorar a execução da sua aplicação
        //saber os metodos que stão sendo chamados, etc

        //Log esta sempre habilitado e é usado para relatorio
        //Pode logar o conteudo de um email ou banco de dados quando existe um problema sério

        public static void TestarTrace()
        {
            Console.WriteLine("Inicio trace");

            //Cria texto e listener para escrever os eventos no arquivo.
            FileStream fs = !File.Exists("Log.txt") 
                ? File.Create("Log.txt")
                : File.Open("Log.txt", FileMode.Append);
            TraceListener tl = new TextWriterTraceListener(fs);


            //SourceLevel para mostrar todos os tipos, ou tipos especificos
            TraceSource trace = new TraceSource("Log Evento:", SourceLevels.All);

            //Se o trace não tiver listener, então o log vai mostrar apenas no console em modo debug
            trace.Listeners.Clear();
            trace.Listeners.Add(tl);

            //Apenas informação
            trace.TraceInformation("Trace de Informação");

            //Trace de evento
            //Podfe ser de erro, critico, verbose, ou apenas de fluxo, como os de baixo
            trace.TraceEvent(TraceEventType.Start, 0, "Começando um trace");
            for (int x = 1; x <= 2; x++)
            {
                trace.TraceEvent(TraceEventType.Resume, x, "Resumo do trace");
            }
            trace.TraceEvent(TraceEventType.Stop, 6, "Fim do trace");

            //trace de data
            //Argumentos extras, que serão mostrados quando for um trace de data
            trace.TraceData(TraceEventType.Warning, 7, new[] {"teste1", "teste2"});

            trace.Flush();
            trace.Close();
        }
    }
}
