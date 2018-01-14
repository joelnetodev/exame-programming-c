using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp._4___Data
{
    public static class TextoDataClass
    {
        //Muitas APIS em c# esperam string writer ou reader para trabalhar, 
        //ele é apenas uma adaptação do StringBuilder, internamente ele trabalha como StringBuilder
        public static void TestarWriterReaderXML()
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

        //Writer e Reader de texto em arquivos
        public static void TestarStreamString()
        {
            string path = "texto_stream.txt";

            using (var streamW = new StreamWriter(path, false))
            {
                //o autoflush sgnfica que vai salvar no arquivo cada vez que escrever
                //não precisa chamar o flush no final
                streamW.AutoFlush = true;

                streamW.WriteLine("Escrita teste 1");
                streamW.WriteLine("Escrita teste 2");
            }

            using (var streamR = new StreamReader(path))
            {
                //duas formas de ler.. por linha ou por texto todo
                //ambas as formas anulam a outra, pois la leram o arquivo
                
                Console.WriteLine("Texto por linha");
                string linha;
                while((linha = streamR.ReadLine()) != null)
                    Console.WriteLine(linha);

                string texto = streamR.ReadToEnd();
                Console.WriteLine("Texto todo");
                Console.WriteLine(texto);
            }
        }
    }
}
