using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._2___Types
{


    public static class ExtensionsClass
    {
        public static void TestarExtensions()
        {
            var produto = new Produto("Banana", 50);
            Console.WriteLine(produto.Desconto50PorCento());

            var dataAtual = DateTime.Now;
            var data1 = new DateTime(2000,1,1);
            var data2 = new DateTime(2500,1,1);

            Console.WriteLine(dataAtual.InBetween(data1, data2));
        }

    }

    
    public class Produto
    {
        public string Nome { get; private set; }

        public double Preco { get; private set; }

        public Produto(string nome, double preco)
        {
            Nome = nome;
            Preco = preco;
        }
    }

    //Classe de extenção para produto e para datetime do .Net
    public static class MyExtensions
    {
        //this referencia o objeto instanciado que vai receber a extensção de método ou propriedade
        public static double Desconto50PorCento(this Produto product)
        {
            return product.Preco*0.5;
        }

        public static bool InBetween(this DateTime dataAtual, DateTime dataAnterior, DateTime dataPosterior)
        {
            return dataAtual >= dataAnterior && dataAtual <= dataPosterior;
        }
    }

}
