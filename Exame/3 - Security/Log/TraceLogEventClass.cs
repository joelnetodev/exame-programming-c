using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
    public static class TraceLogEventClass
    {
        //É possivel garantir ou negar acesso ao disco utilizando diretivas de permissão.
        
        public static void TestarPermissaoNegada()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                
                //Tipo de permissão: Negar Criação
                DirectoryInfo dInfo = new DirectoryInfo(path);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.CreateFiles, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Deny));
                dInfo.SetAccessControl(dSecurity);

                Console.WriteLine("Permissão Negada");
                Console.WriteLine("Pressione qualquer tecla para tentar criar aquivo");
                Console.ReadLine();

                path = path + "texto.txt";
                if (!File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("First Line");
                        tw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                GarantirAcesso();
            }
        }

        private static void GarantirAcesso()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;

                //DirectoryInfo dInfo = new DirectoryInfo(path);
                //DirectorySecurity dSecurity = dInfo.GetAccessControl();
                //dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.CreateFiles, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                //dInfo.SetAccessControl(dSecurity);

                //Tipo de permissão: irrestrita, concedido qualquer permissão ao diretório
                //Tipo de permissão de acesso: Total aos arquivos do diretório
                FileIOPermission fPerm = new FileIOPermission(PermissionState.Unrestricted);
                fPerm.AllLocalFiles = FileIOPermissionAccess.AllAccess;
                fPerm.Demand();

                Console.WriteLine("Permissão garantida");
                Console.WriteLine("Pressione qualquer tecla para tentar criar aquivo");
                Console.ReadLine();

                path = path + "texto.txt";
                if (!File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("First Line");
                        tw.Close();
                    }
                }

                Console.WriteLine("Arquivo criado e editado.");
            }
            catch (SecurityException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
