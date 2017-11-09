using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace ConsoleApp
{
    public static class ValidateJsonXmlClass
    {
        //Json e Xml são usados para troca de informações entre diferentes aplicações
        //Notations

        public static void TestarJson()
        {
            //Valida se é um objeto esperado do Json

            ValidateTypeContentClass.Cliente cliente = new ValidateTypeContentClass.Cliente();
            cliente.Id = 1;
            cliente.Nome = "Joel";
            cliente.Cpf = "08230426422";

            //Em system.Web.Extensions
            var jss = new JavaScriptSerializer();
            string json = jss.Serialize(cliente);
            Console.WriteLine(json);

            //Se for único objeto, deserializa como dic 
            var dic = jss.Deserialize<Dictionary<string, object>>(json);
            var clienteDes = jss.Deserialize<ValidateTypeContentClass.Cliente>(json);

            Console.WriteLine();
            foreach (var itemDic in dic)
            {
                Console.WriteLine("Key: " + itemDic.Key);
                Console.WriteLine("Value: " + itemDic.Value);
            }
        }

        public static void TestarXML()
        {
            Console.WriteLine("Validando XML com XSD");

            ValidateTypeContentClass.Cliente cliente = new ValidateTypeContentClass.Cliente();
            cliente.Id = 1;
            cliente.Nome = "Joel";
            cliente.Cpf = "08230426422";

            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(typeof (ValidateTypeContentClass.Cliente));
            serializer.Serialize(stringwriter, cliente);
            string stringXML = stringwriter.ToString();

            //XSD é um arquivo XML para validar arquivos XML
            //Contém se campos são válidos, se estão preenchidos com valores, etc
            string caminhoXSD = @"resource\cliente.xsd";

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Schemas.Add("", caminhoXSD);
            xmlDocument.LoadXml(stringXML);
            xmlDocument.Validate(ValidationEventHandler);

            Console.WriteLine("XML cliente Validade");
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }
    }
}
