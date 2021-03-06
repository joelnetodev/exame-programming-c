﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._2___Types
{

    //Conversão Explicita: Casting, precisa de sintaxe especial
    //Conversão Implicita, direto... um objeto pode ser do outro, se for o mesmo tipo ou base class

    public static class ConversionClass
    {
        public static void TestarConversaoImplicitaExplicita()
        {
            Money m = new Money(20.50);

            //Explicita
            Console.WriteLine((int)m);


            //Implicita
            string valorString = m;
            Console.WriteLine(valorString);
            
        }

        public static void TestarConversarAseIs()
        {
            object money1 = new Money(20);

            var money2 = money1 as Money;

            if (money2 != null && money2 is Money)
            {
                Console.WriteLine("Conversão efetuada");
            }

            //as retorna nulo se não conseguir fazer a conversão
            var money3 = money1 as ClassePessoa;
            if (money3 == null || !(money3 is Money))
            {
                Console.WriteLine("Conversão não efetuada");
            }
        }



        private class Money
        {
            public double Valor { get; set; }

            public Money(double valor)
            {
                Valor = valor;
            }

            public override string ToString()
            {
                return Valor.ToString();
            }

            //Implicita de string = variavel vai receber money
            //(string)money converte money em string
            public static implicit operator string(Money money)
            {
                return money.ToString();
            }

            //explicita de int, ao converter m par int, retorna valor inteiro
            //(string)money converte money em int
            public static explicit operator int(Money money)
            {
                return (int)money.Valor;
            }

            //Sobrescreve a comparação por ele mesmo == 
            //em vez de comprar se objeto igual a objeto, compara se valor igual a valor
            public static bool operator ==(Money m1, Money m2)
            {
                return m1.Valor == m2.Valor;
            }
            public static bool operator !=(Money m1, Money m2)
            {
                return m1.Valor != m2.Valor;
            }
        }
    }
    
}
