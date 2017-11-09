using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace ConsoleApp
{
    public class InterfaceClass
    {
        public static void TesteInterface()
        {
            var pato = new PatoClass();

            pato.Bicar();

            //para chamar, é precisa fazer uma conversão da classe pato, para uma das interfaces
            ((IPato1)pato).Quack();
            ((IPato2)pato).Quack();

            pato.Quack();
        }
    }


    public class PatoClass : IPato1, IPato2
    {
        //Implementação Implicita
        //void IPato1.Quack()
        //{
        //    Console.WriteLine("Quack Method IPato1");
        //}

        ////Implementação Implicita
        //void IPato2.Quack()
        //{
        //    Console.WriteLine("Quack Method IPato2");
        //}

        public void Quack()
        {
            Console.WriteLine("Quack PatoClass");
        }

        public void Bicar()
        {
            Console.WriteLine("Bicar de IAve em PatoClass");
        }
    }

    public interface IAve
    {
        void Bicar();
    }

    public interface IPato1 : IAve
    {
        void Quack();
    }

    public interface IPato2 : IAve
    {
        void Quack();
    }
}
