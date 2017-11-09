using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace Exame
{
    public static class CryptographyClass
    {
        //Criptografia é sobre criptografar e decriptografar dados.
        //Tornar a criptografia pública é bom, porque quando um algoritimo Data Encryption Standard (DES) é ncessário, 
        //o American National Institute of Standards and Technology (NIST) tentam quebrar a encriptação
        //e elas se tornam públicas, se passarem por todas as etapas.

        //Existem 2 tipos de encriptação
        //Simetrica: Usa Uma chave publica (Se encaixa bem apra criptografar grande quantidade de dados) ex: AES Encrypt
        //Assimetrica: Usa Duas chaves - 1 publica para encriptar e 1 privada para decriptar e vice-versa (Não cai bem com grande quantidade de dados) Ex: RSA

        public static void TestarCriptografiaSimetrica()
        {
            string textoChave;
            byte[] chaveEmBytes = new byte[0];

            bool proseguir = false;
            while (!proseguir)
            {
                Console.WriteLine("Digite a chave que será usada: ");
                textoChave = Console.ReadLine();
                chaveEmBytes = UnicodeEncoding.Default.GetBytes(textoChave);
                
                if (chaveEmBytes.Length < 32)
                {
                    Console.WriteLine();
                    Console.WriteLine("Chave muito pequena, tente novamente");
                    continue;
                }
                
                if (chaveEmBytes.Length > 32)
                {
                    Console.WriteLine();
                    Console.WriteLine("Chave muito grande, tente novamente");
                    continue;
                }
                proseguir = true;
            }       

            Console.WriteLine();
            Console.WriteLine("Chave convertida em bytes: " + BitConverter.ToString(chaveEmBytes));
            
            Console.WriteLine();
            Console.WriteLine("Digite algum texto para criptografar:");
            string texto = Console.ReadLine();

            //Aes = Advanced Encryption Standard para criptografia simetrica
            using (SymmetricAlgorithm symmetricObj = new AesCryptoServiceProvider())
            {
                byte[] mensagemCriptografada = Encrypt(symmetricObj, texto, chaveEmBytes);
                Console.WriteLine();
                Console.WriteLine("Texto criptografado: " + BitConverter.ToString(mensagemCriptografada));


                string textoDecriptado = Decrypt(symmetricObj, mensagemCriptografada, chaveEmBytes);
                Console.WriteLine();
                Console.WriteLine("Texto decriptografado: " + textoDecriptado);
            }
        }

        private static byte[] Encrypt(SymmetricAlgorithm symmetricAESalgorithmObj, string textToEncrypt, byte[] key = null)
        {
            //rgbKey = Chave byte[], usada como segredo para criptografar e decriptografar
            //IV = Initial Vector byte[], valor inicial do hash
            //o byte se torna um valor "concatenado" IV + criptografiaDaChave

            ICryptoTransform cryptorObj = symmetricAESalgorithmObj.CreateEncryptor(key ?? symmetricAESalgorithmObj.Key, symmetricAESalgorithmObj.IV);

            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(mStream, cryptorObj,CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(textToEncrypt);
                    }
                    return mStream.ToArray();
                }
            }
        }

        private static string Decrypt(SymmetricAlgorithm symmetricAESalgorithmObj, byte[] textEncrypted, byte[] key = null)
        {
            ICryptoTransform cryptorObj = symmetricAESalgorithmObj.CreateDecryptor(key ?? symmetricAESalgorithmObj.Key, symmetricAESalgorithmObj.IV);

            using (MemoryStream mStream = new MemoryStream(textEncrypted))
            {
                //CryptoStream encripta ou decripta uma sequencia de bytes
                using (CryptoStream cryptoStream = new CryptoStream(mStream, cryptorObj, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
        
        public static void TestarCriptografiaAssimetrica()
        {
            //Cria duas chaves, uma publica e uma privada no formado XML
            RSACryptoServiceProvider rsaObj = new RSACryptoServiceProvider();
            string keyPublicaXML = rsaObj.ToXmlString(false);
            string keyPrivadaXML = rsaObj.ToXmlString(true);

            Console.WriteLine("Chave publica: " + keyPublicaXML);
            Console.WriteLine("Chave privada: " + keyPrivadaXML);
            
            using (RSACryptoServiceProvider itemRSA = new RSACryptoServiceProvider())
            {
                Console.WriteLine();
                Console.WriteLine("Digite um texto para ser criptografado: ");
                string texto = Console.ReadLine();
                //Encoding usado para gerar os bytes do texto
                byte[] txtoBytes = Encoding.Unicode.GetBytes(texto);

                itemRSA.FromXmlString(keyPublicaXML);
                var textoCriptografadoEmBytes = itemRSA.Encrypt(txtoBytes, false);
                Console.WriteLine();
                //BitConverter retorna a sequencia de bytes em forma de texto
                Console.WriteLine("Texto criptografado: " + BitConverter.ToString(textoCriptografadoEmBytes));

                itemRSA.FromXmlString(keyPrivadaXML);
                byte[] textoDecriptografadoEmBytes = itemRSA.Decrypt(textoCriptografadoEmBytes, false);
                Console.WriteLine();
                //Endocing usado para transformar os bytes em texto
                Console.WriteLine("Texto decriptografado: " + Encoding.Unicode.GetString(textoDecriptografadoEmBytes));
            }
        }
    }
}
