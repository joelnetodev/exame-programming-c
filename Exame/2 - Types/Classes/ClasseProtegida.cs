using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

//Classe com metodos e propriedades protegidos não ficam visiveis quando instanciados, apenas herdados.
//Tornam-se privados para a própria classe e derivados da classe.

namespace ConsoleApp
{
    public class ClassePessoaFisica : ClassePessoa
    {
        public string Cpf { get; set; }

        public ClassePessoaFisica()
        {
            var pessoa = new ClassePessoa();
            //pessoa. //Propriedades protegidas, não são acessadas quando instanciamos a classe, apenas quando herdamos (private exceto herança)

            //Posso apenas usar o que esta protegido
            Nome = "";
            Endereco = "";
            Cpf = "08230426422";
            TesteProtegido();
        }
    }

    public class ClassePessoa
    {
        protected string Nome { get; set; }
        protected string Endereco { get; set; }

        protected void TesteProtegido()
        {
            Console.WriteLine("Teste metodo protegido invocado por classe derivada");
        }
    }
}
