using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace ConsoleApp
{
    public static class CertificateClass
    {
        //Certificados digitais é a área onde criptografia assimetrica e hash se encontram.
        
        //Certificados são parte da Public Key Infrastruture (PKI) que é um sistema de certificado digital.
        //Certificate Authorithy (CA) resolve e analisa confiavelmente certificados que contenham uma chave, um assunto para que o certificado é emitido e detalhes sobre o mesmo.

        //Usando o MakeCert.exe é possivel criar um certificado teste padrão x.509 para testar
        // X.509 é um padrão de PKI que define os requisitos para um certificado digital


        //PS: certificado usa SHA1, hash tbm.
        public static void TestarCertificado()
        {
            Console.WriteLine("Digite um texto");
            string msg1 = Console.ReadLine();
            var textoAssinado = Assinar(msg1);
            
            Console.WriteLine("Digite outro texto");
            string msg2 = Console.ReadLine();

            bool teste = Verificar(msg2, textoAssinado);

            string result = teste ? "Mensagens batem" : "Mensagens não batem";
            Console.WriteLine(result);

        }

        //Usa chave provada para assinar
        private static byte[] Assinar(string txt)
        {
            var cert = ObterCertificado();
            var cryptoServiceKey = (RSACryptoServiceProvider) cert.PrivateKey;

            var hashedTxt = DoHashFromString(txt);

            //CryptoConfig.MapNameToOID("SHA1") = Algoritmo que foi usado para criptografar o texto 
            return cryptoServiceKey.SignHash(hashedTxt, CryptoConfig.MapNameToOID("SHA1"));
        }

        //Usa chave pública para verificar
        private static bool Verificar(string txt, byte[] hashedTxtSigned)
        {
            var cert = ObterCertificado();
            var cryptoServiceKey = (RSACryptoServiceProvider)cert.PublicKey.Key;

            var hashedTxt = DoHashFromString(txt);

            //Verifica se o hash da mensagem é igual a mensagem assinada com certificado usando a chave publica
            return cryptoServiceKey.VerifyHash(hashedTxt, CryptoConfig.MapNameToOID("SHA1"), hashedTxtSigned);
        }

        private static byte[] DoHashFromString(string txt)
        {
            //USA SHA1 para criptografar o texto
            HashAlgorithm hashing = new SHA1Managed();
            UnicodeEncoding encode = new UnicodeEncoding();

            byte[] txtEmBytes = encode.GetBytes(txt);
            return hashing.ComputeHash(txtEmBytes);
        }

        //makecert -n "CN=CertificadoJoel" -sr CurrentUser -ss customCertStore
        private static X509Certificate2 ObterCertificado()
        {
            //Tenho acesso a pasta "customCertStore" onde o certifficado teste esta instalado com o nome "CN=CertificadoJoel"
            X509Store store = new X509Store("customCertStore", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);

            
            foreach (var cert in store.Certificates)
            {
                if (cert.SubjectName.Name == "CN=CertificadoJoel")
                {
                    return cert;
                }                  
            }

            return null;

            //D:\CertificadoTeste.cer

            //using (FileStream fileStream = File.OpenRead(@"D:\CertificadoTeste.cer"))
            //{
            //    byte[] certByte = new byte[fileStream.Length];
            //    fileStream.Read(certByte, 0, certByte.Length);

            //    var cert = new X509Certificate2(certByte);
            //    cert.PrivateKey = PrivateKey;
            //    return cert;
            //}
        }
    }
}
