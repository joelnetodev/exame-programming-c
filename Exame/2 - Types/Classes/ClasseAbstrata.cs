using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


//Classes abstratas não podem ser instanciadas, apenas herdadas.
//Metodos abstratos são assinaturas, devem ser implementados e sobrescritos
//Metodos virtual podem ser sobrescritos

namespace ConsoleApp
{
    public class AbstractClass
    {
        public static void TestarClasseAbstrata()
        {
            //Como Verificar() não é override e o tipo da classe é a Abs Base, então será chamado o Verificar() de AbsBase
            //Se substituir, ou converter
          //ClassePublicaNormal//
            ClasseAbstrataBase classeAbs;
            classeAbs = new ClassePublicaNormal(10);

            ((ClassePublicaNormal)classeAbs).Verificar();

            classeAbs.Verificar();
            classeAbs.TesteAbstrato1();
            classeAbs.TesteAbstrato3NaoImplementado();
            classeAbs.Verificar();

            ((ClassePublicaNormal)classeAbs).Verificar();
        }
    }

    

    
    /// <summary>
    /// Classe publica que herda de abstrata
    /// </summary>
    public class ClassePublicaNormal : ClasseAbstrataBase
    {
        public ClassePublicaNormal(int numero)
            : base(numero)
        {
         
        }

        public override void TesteAbstrato1()
        {
            base.TesteAbstrato1();
            Console.WriteLine(MensagemClasseAbstrata);
        }

        public override void TesteAbstrato3NaoImplementado()
        {
            while (IsAtivo)
            {
                Desativar();
            }       
        }

        //New hiding base method, substitui por outro da classe derivada
        public new bool Verificar()
        {
            Console.WriteLine(IsAtivo);
            return IsAtivo;
        }
    }

   
    /// <summary>
    /// Classe abstrata
    /// </summary>
    public abstract class ClasseAbstrataBase
    {
        protected string MensagemClasseAbstrata;
        private readonly int _numero;

        /// <summary>
        /// Retorna se está ativo
        /// </summary>
        protected bool IsAtivo { get; private set; }

        protected ClasseAbstrataBase(int numero)
        {
            _numero = numero;
            IsAtivo = true;
        }


        /// <summary>
        /// Metodo virtual implementado, podendo ser sobrescrito pela classe que herda
        /// </summary>
        public virtual void TesteAbstrato1()
        {
            MensagemClasseAbstrata = "Mensagem: Testando propriedade classe abstrada / Número: " + _numero;
        }

        protected void Desativar()
        {
            IsAtivo = false;
        }

        public bool Verificar()
        {
            return IsAtivo;
        }

        /// <summary>
        /// Apenas a assinatura do metodo abstrato, para que a classe que herda tenha que sobrescrever
        /// </summary>
        public abstract void TesteAbstrato3NaoImplementado();
    }
}