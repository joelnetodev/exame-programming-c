using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace ConsoleApp
{
    public static class DebuggClass
    {
        //Debugar aplicação é o emlhor jeito de encontrar erros e visualizar os procedimentos da sua aplicação.
        //Existem dois tipos de build padrão no visual studio.
        //Release - Otimizada, ambiente configurado para demonstração de aplicação final
        //Debug - Não existe otimização, informações extras não são inputadas

        public static void TestarDebugReleaseTimer()
        {
            //Em 3 segundos este método será chamado
            Timer timer = new Timer(MetodoTimerCallBack, null, 0, 5000);
            Console.WriteLine();
            Console.ReadLine();
        }

        private static void MetodoTimerCallBack(object o)
        {
            //Em modo debug, o GC.Collect() Não funciona bem, pois o objeto esta dentro da bolha do Debug Mode, ainda sendo referenciado.
            //Em modo release, as otimizações e configurações são feitas e o GC coleta as referencias não usadas.

            Console.WriteLine("Timer iniciado em " + DateTime.Now);
            GC.Collect();
        }

        //Diretivas de compilação
        //Usar diretivas de compilação quando quiser executar algo apenas em debug, para testes por exemplo.
        //Ou usar um assembly diferente se não estiver usando WinRT.
        public static void TestarDiretivas()
        {
#if DEBUG
            Console.WriteLine("Modo DEBUG");
#else
                Console.WriteLine("Modo RELEASE");
#endif

            //WinRT são aplicações de ambiente windows, como tablets do win10
#if !WINRT
            Console.WriteLine("Modo Não Windows RT");
#else
                Console.WriteLine("Modo Windows RT (par adispositivos moveis)");
#endif


            //Usar diretivas para mostrar um erro ou aviso quando realizar o build
#if !DEBUG
#error Compliação em modo Release não permitido para metodo TestarDiretivas()
#else
#warning Compliação em modo DEBUG permitida para metodo TestarDiretivas() 
#endif

            //Desabilitar warnings de build
#pragma warning disable
            string semUso;
            while (false)
            {
                Console.WriteLine("teste");
            }
#pragma warning restore
        }

        //Metodo só é executado em modo Debug
        [Conditional("DEBUG")]
        public static void TestarAtributoDiretivas()
        {
            Console.WriteLine("Executado em modo DEBUG");

            //BreakPoint para ver a descrição do objeto
            Cliente cliente = new Cliente(1, "Joel Neto");
            var cli = cliente;
        }



        //OBS:
        //PDB files
        //Arquivos PDB não estão contidos na DLL mas ajudam no Debug
        //Contem informações dotipo: Nomes, pastas e linhas de arquivos e nome de variáveis locais
        //Se colcoar um breackpoint em uma linha, é o PDB que auxilia e tem as informações caso esteja debugando em produção (aplicação em produção e VS local, tem que ter o mesmo PDB file)
        //Elas ajudam tamém a dizer em qual linha e metodo ocorreu a exceção, por exemplo.
        public static void TestarLinhas()
        {

            try
            {
                int numero = 0;

                //Se der um erro aqui, vai ser na linha 900 do arquivo "TesteFile"
#line 900 "TesteFile"
                numero = Convert.ToInt32(Console.ReadLine());

                //Hidden esconde a linha no debug e default é a linha padrão
#line hidden
#line default
                numero = Convert.ToInt32(Console.ReadLine());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

        }


        [DebuggerDisplay("Id={Id} - Nome={Nome}")]
        private class Cliente
        {
            public int Id { get; set; }
            public string Nome { get; set; }

            public int Idade { get; set; }

            public Cliente(int id, string nome)
            {
                Id = id;
                Nome = nome;
            }
        }

    }
}
