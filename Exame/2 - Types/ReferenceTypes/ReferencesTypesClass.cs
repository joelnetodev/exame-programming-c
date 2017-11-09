using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._2___Types
{
    //Valores de referencia são salvos no Heap, pois são apenas referencias, precisam ser limpos pelo garbage collector
    
    //principios de projeto SOLID
    //Single Responsability
    //Open/Closed - aberto para extensão, mas fechado para modificação
    //Liskov Substitution - O tipo base pode ser substituido por um subtipo
    //Interface segregation - Usar interface especifica, em vez de uma geral
    //Dependency Inversion - Depender da abstração ou da interface, em vez da classe concreta

    public static class ReferencesTypesClass
    {
        public static void TestarTypoReferencia()
        {
            var pessoa = new Pessoa();

            pessoa.ConcatenarNome("Joel");
            pessoa.AtribuirEndereco();
            
            Console.WriteLine(pessoa.Nome);
            
            //Demeter Law = Passagem de parametros simples, por referencia, a ultima hierarquia. Apenas o que se precisa
            Console.WriteLine(pessoa.ObterEnderecoCompleto(pessoa.Endereco));

            pessoa.AtribuirTelefone("32468792");
            pessoa.AtribuirTelefone("84631548");

            Console.WriteLine(pessoa[1]);
            Console.WriteLine(pessoa[2]);
        }




        
        //Todos os set privados, apenas a própria classe pode atribuir valor
        private class Pessoa
        {
            public string Nome { get; private set; }

            public int Idade { get; private set; }

            public Endereco Endereco { get; private set; }

            public List<Telefone> Telefones { get; private set; }

            //Pode acorrentar contrutor para evitar repetição de código.
            //base quando é construtor da classe pai
            public Pessoa()
                : this(21)
            {

            }

            public Pessoa(int idade)
            {
                Telefones = new List<Telefone>();
                Idade = idade;
            }

            public void ConcatenarNome(string nome)
            {
                Nome = nome;
            }

            //Function - Retorna Valor não altera nada no sistema, reader
            public string ObterNome()
            {
                return Nome;
            }

            //Seter de Endereço esta privado, uso método
            public void AtribuirEndereco()
            {
                Endereco = new Endereco();
                Endereco.Rua = "Rua para teste";
                Endereco.Cidade = "Cidade Teste";
                Endereco.CEP = "12345678";
            }

            //Método - Não retorna, altera coisas no sistema, writer
            public string ObterEnderecoCompleto(Endereco endereco)
            {
                return endereco.Rua + ", " + endereco.Cidade + " CEP: " + endereco.CEP;
            }

            public void AtribuirTelefone(string numero)
            {
                Telefones.Add(new Telefone { Numero = numero });
            }

            //Implementadno Acesso ao telefone
            public string this[int posicao]
            {
                get { return Telefones[posicao - 1].Numero; }
            }
        }

        private class Endereco
        {
            public string Rua { get; set; }

            public string Cidade { get; set; }

            public string CEP { get; set; }
        }

        private class Telefone
        {
            public string Numero { get; set; }
        }

    }
}
