using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleApp
{
    public static class ValidateTypeContentClass
    {
        //Validar data vinda de Malicious Users ou Innocent Users
        //Tipos de Integridade de Dados
        //Entity - Determina que cada entidade deve ser unicamente identificada (geralmente através de Primary Key)
        //Domain - Integridade dos dados contidos na entidade (tipo de dado, valores possíveis)
        //Referential - Integridade da relação das entidades umas com as outras
        //User-defined - Integridade dos dados influenciados/afetados pela regra de negócio

        private static string regexTel = @"^\([0-9]{2}\)[0-9]{4}\-[0-9]{4}$";
        private static string regexCel = @"^\([0-9]{2}\)[0-9]{5}\-[0-9]{4}$";

        public static void TestarIntregridadeNotations()
        {
            Cliente cost = new Cliente
            {
                Cpf = "123456",
            };

            var listaValidacao = Validate(cost);

            if (listaValidacao.Any())
            {
                foreach (var validate in listaValidacao)
                {
                    Console.WriteLine(validate.ErrorMessage);
                }
            }
        }

        private static IList<ValidationResult> Validate(object entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity, null, null);
            Validator.TryValidateObject(entity, context, results);
            return results;
        }

        public static void TestarParseConvert()
        {
            //*.Parse aceita apenas string como parametro para tentar converter
            //Enquanto o Convert.* realiza a conversão de qualquer outro tipo de dado

            int x;
            x = Convert.ToInt32(null);
            Console.WriteLine("Convert.ToInt32 x= " + x);

            int.Parse("123");
            Console.WriteLine("int.Parse \"123\" = 123");

            if(!int.TryParse("abc", out x))
                Console.WriteLine("int.TryParse \"abc\" = false");
        }

        public static void TestarRegEx()
        {
            //Especifico para validação e combinação de strings
            //Força que contenham x carateres, ou que não devam conter

            string fone;

            Console.WriteLine("Escreve um numero de telefone");
            fone = Console.ReadLine();

            if (Regex.Match(fone, regexTel).Success || Regex.Match(fone, regexCel).Success)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("Erro");
            }
        }

        public class Cliente
        {
            public int Id { get; set; }

            [Required, StringLength(20)]
            public string Nome { get; set; }

            [Required]
            [StringLength(11, MinimumLength = 11)]
            public string Cpf { get; set; }

            [RegularExpression(@"^\([0-9]{2}\)[0-9]{4}\-[0-9]{4}$")]
            public string Telefone { get; set; }

            public string Celular { get; set; }
        }
    }
}
