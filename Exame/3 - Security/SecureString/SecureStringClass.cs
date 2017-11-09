using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace ConsoleApp
{
    public static class SecureStringClass
    {
        //SecureString serve para guardar dados de string criptografada na memória, funciona como um StringBuilder
        //Assim não cria copia sempre que concatena e caso o GC apague, quando o sistema cria uma cópia em disco com a string está encryptada

        public static void TestarSecureString()
        {
            Console.WriteLine("Digite o password:");

            using (SecureString secStr = new SecureString())
            {
                //Secure le apenas um carater por vez, então é concatenado dentro do loop
                while (true)
                {
                    var keyRead = Console.ReadKey(true);

                    if (keyRead.Key == ConsoleKey.Enter)
                        break;

                    secStr.AppendChar(keyRead.KeyChar);
                    Console.Write("x");
                }
                Console.WriteLine();

                //Marshal dentr de Interop, oferece uma série de metodos que podem decruptar a string
                //No fim, tem que apagar da memoria usando o ZeroFree
                var encryptedStr = Marshal.SecureStringToGlobalAllocAnsi(secStr);
                var texto = Marshal.PtrToStringAnsi(encryptedStr);
                Marshal.ZeroFreeGlobalAllocAnsi(encryptedStr);

                Console.WriteLine("Senha digitada foi:");
                Console.WriteLine(texto);
            }
        }
    }
}
