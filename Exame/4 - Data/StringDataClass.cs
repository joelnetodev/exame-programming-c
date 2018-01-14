using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp._4___Data
{
    public static class StringDataClass
    {
        //Muitas APIS em c# esperam string writer ou reader para trabalhar, 
        //ele é apenas uma adaptação do StringBuilder, internamente ele trabalha como StringBuilder
        public static void TestarWriterReader()
        {
            //Writer
            var strWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(strWriter))
            {
                //Cria um nó de pessoa e escreve elementos dentro
                xmlWriter.WriteStartElement("Pessoa");

                xmlWriter.WriteElementString("Nome", "Joel Neto");
                xmlWriter.WriteElementString("Idade", "26");

                xmlWriter.WriteElementString("Nome", "José Augusto");
                xmlWriter.WriteElementString("Idade", "30");

                xmlWriter.WriteEndElement();

                //Descarrega, livra memoria
                xmlWriter.Flush();
            }

            var xmlString = strWriter.ToString();

            Console.WriteLine(xmlString);
            Console.WriteLine();

            //Reader
            var strReader = new StringReader(xmlString);
            using (var xmlReader = XmlReader.Create(strReader))
            {
                xmlReader.ReadToFollowing("Pessoa");
                Console.WriteLine(xmlReader.ReadInnerXml());
            }

            //Console.WriteLine(strWriter);
        }
    }
}
