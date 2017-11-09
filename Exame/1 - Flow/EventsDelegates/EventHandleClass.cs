using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.EventsDelegates
{
    //Em vez do Tipo Action para o event, para seguir as convenções  de codigo do .Net FrameWork, foi definido q teria que usar o EventHandler/EventHandler<T>
    public class TesteEventHandleClass
    {
        public void TestarEventHandle()
        {
            //Event não pode ser atribuido com o perador =, apenas com +=
            EventHandleClass p = new EventHandleClass();
            p.OnSeted += MetodoDoEvento;

            p.PessoaArgs = new PessoaArgs("Joel", "08230426422", "Rua afogados da ingazeira, 268");

        }

        //Por Default tem como parametro o objeto sender (que é quem levanta o evento, this, ou nullo, caso venha de um metodo estatico) e EventArgs
        private void MetodoDoEvento(object sender, EventArgs eventArgs)
        {
            if (eventArgs.GetType() == typeof(PessoaArgs))
            {
                var argsPessoa = (PessoaArgs)eventArgs;
                Console.WriteLine("Nome: " + argsPessoa.Nome);
                Console.WriteLine("CPF: " + argsPessoa.CPF);
                Console.WriteLine("Endereço: " + argsPessoa.Endereco);
            }
        }
    }



    public class EventHandleClass
    {
        private PessoaArgs _pessoa;
        public PessoaArgs PessoaArgs
        {
            get { return _pessoa; }
            set 
            { 
                _pessoa = value;

                if (OnSeted != null)
                    OnSeted(this, _pessoa);
            }
        }

        //Um evento do tipo EventHandler aceita apenas parametros que sejam do tipo EventArgs
        public event EventHandler<PessoaArgs> OnSeted;
    }

    //Um evento do tipo EventHandler aceita apenas parametros que sejam do tipo EventArgs
    public class PessoaArgs : EventArgs
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Endereco { get; set; }

        public PessoaArgs(string nome, string cpf, string endereco)
        {
            Nome = nome;
            CPF = cpf;
            Endereco = endereco;
        }
    }
}
