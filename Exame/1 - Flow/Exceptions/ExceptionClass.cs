using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Exceptions
{
    //Quando um erro ocorre em algum lugar da aplicação, uma exceção é levantada
    //O runtime começa a procurar o lugar onde você trata a exceção, se não existir a aplicação é encerrada
    //Para tratar erros você precisa embrulhar um codigo dentro de um try catch
    
    public class ExceptionClass
    {
        public static void TestarException()
        {
            try
            {
                Console.WriteLine("Escreve alguma coisa:");
                string digito = Console.ReadLine();

                Convert.ToInt32(digito);
            }
            catch (Exception e)
            {
                Console.WriteLine("Algum erro aconteceu:");
                Console.WriteLine(e.Message);
            }
        }

        public static void TestarSemException()
        {
            //Se quiser que o sistema prossiga, mesmo depois de ter detectado o erro, então não capture o mesmo
            //Deixando o catch vazio, o sistema prossegue com a execução

            try
            {
                Console.WriteLine("Escreve alguma coisa:");
                string digito = Console.ReadLine();

                Convert.ToInt32(digito);
            }
            catch { }
        }

        public static void TestarSpecifcException()
        {
            //O compilador trata primeiro erros mais especificos, depois os menos especificos
            //Excpetion é a exceção mais geral, então sempre tem que vir pro ultimo
            //Os primeiros tratamentos tem que ser do erro mais expecifico, caso contrário o sistema lança uma exceção geral
            try
            {
                Console.WriteLine("Escreve alguma coisa:");
                string digito = Console.ReadLine();

                Convert.ToInt32(digito);
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada não é número");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro não tratado.");
                Console.WriteLine(ex.Message);
            }     
        }

        public static void TestarFinallyException()
        {
            //É correto afirmar que quando uma exceção ocorre, sua aplicação não está no melhor estado.
            //Para isso é ultilizado um bloco finally, visto que se estiver tratando cada exceção, teria que escrever a reversão em cada um deles
            //O bloco finally vai ser executado, independente da exceção ser levantada ou não, pode ser usado para reversão

            try
            {
                Console.WriteLine("Escreve alguma coisa:");
                string digito = Console.ReadLine();

                Convert.ToInt32(digito);
            }
            finally
            {
                Console.WriteLine("Fim do bloco Try");
            }
        }

        public static void TestarCustomExcpetion()
        {
            try
            {
                throw new UnknownException("Testando lançamento de exceção customizada");
            }
            catch (UnknownException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Erro não tratado.");
            }      
        }

        //Exceção customizada não deve nunca herdar de ApplicationAexception, por padrão, utilizar o sufixo Exception
        //Bom ter um construtor a mais para guardar uma exceção gerada por outra exceção. Inner Exception. 
        //E serializar para trablhar corretamente entre diferentes dominios e aplicações

        [Serializable]
        public class UnknownException : Exception
        {
            public UnknownException(string message) : base(message)
            {
                
            }

            public UnknownException(string message, Exception innerException)
                : base(message, innerException)
            {

            }
        }
    }
}
