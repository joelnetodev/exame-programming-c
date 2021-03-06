﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp.EventsDelegates;
using ConsoleApp._2___Types;
using ConsoleApp._2___Types.BaseTypes;
using Exame;
using ConsoleApp._4___Data;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Main");
                Console.WriteLine("");

                TestarFlowThreads();
                TestarTypesLifeCycle();
                TestarSecurityLog();
                TestarData();

                Console.WriteLine("\n\n");
                Console.WriteLine("End Main");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey(); 
            }
            
        }

        private static void TestarFlowThreads()
        {
            ////Flow

            ////Thread
            //ThreadClass.TestarThread();
            //ThreadClass.ThreadParametro();
            //ThreadClass.TestarThreadAtributo();


            ////Task
            //TaskClass.TestarTask();
            //Console.WriteLine(TaskClass.TestarTaskReturn());
            //TaskClass.TestarMultipleTasks();
            //TaskClass.ThreadFactoryTest();
            //TaskClass.TaskCancellationTest();

            ////Async
            //AsyncClass.TestarAsync();
            //AsyncClass.TestarAsyncConfigureAwait();

            ////Parallel
            //ParallelClass.TestarLinqParallel();
            //ParallelClass.TestarConcurrentLists();
            //ParallelClass.LockedAndVolatileVariablesTest();

            ////Delegates
            //DelegateClass.TestarDelegate();
            //DelegateClass.TestarMultipleDelegate();,
            //DelegateClass.TestarLambdaDelegate();
            //DelegateClass.TestEventAction();

            ////EventHandle
            //TesteEventHandleClass eventHandle = new TesteEventHandleClass();
            //eventHandle.TestarEventHandle();

            ////Exceptions
            //ExceptionClass.TestarException();
            //ExceptionClass.TestarSemException();
            //ExceptionClass.TestarSpecifcException();
            //ExceptionClass.TestarCustomExcpetion();
        }

        private static void TestarTypesLifeCycle()
        {
            ////Types LifeCycle

            ////Value
            //ValueTypesClass.TestarEnum();

            ////Reference
            //ReferencesTypesClass.TestarTypoReferencia();
            //GenericTypesClass.TestarGeneric();
            //ExtensionsClass.TestarExtensions();

            ////Conversão
            //ConversionClass.TestarConversaoImplicitaExplicita();
            //ConversionClass.TestarConversarAseIs();
            //DynamicClass.TestarDynamic();

            //Classes e Interfaces
            //InterfaceClass.TesteInterface();
            //AbstractClass.TestarClasseAbstrata();

            ////Reflection
            //ReflectionAttributeClass.TestarAtributo();
            //ReflectionClass.TestarReflectionExecutarMetodo();
            //ReflectionClass.TestarReflectionInstanciarClasse();
            //CodeDOMClass.TestarCodeDOMexpression();

            ////Ciclo de Vida
            //GargabeClass.TestarAguardarGarbage();
            //GargabeClass.TestarDisposeInUsing();
            //WeakClass.TestarWeakRef();

            ////STRINGS
            //StringClass.TestarStringBuilder();
            //StringClass.TestarSearch();
            //Console.WriteLine(StringClass.ToString());
            //StringClass.TestarToString();

            ////Base Types 
            //BaseTypeClass.TestarComparable();
            //BaseTypeClass.TestarEquatable();

        }

        private static void TestarSecurityLog()
        {
            ////Security Logs

            ////Criptografia
            //CryptographyClass.TestarCriptografiaSimetrica();
            //CryptographyClass.TestarCriptografiaAssimetrica();

            ////ValidationResult conteudo e tipo
            //ValidateTypeContentClass.TestarParseConvert();
            //ValidateTypeContentClass.TestarIntregridadeNotations();
            //ValidateTypeContentClass.TestarRegEx();

            ////Validar Serialização
            //ValidateJsonXmlClass.TestarJson();
            //ValidateJsonXmlClass.TestarXML();

            ////Certificado
            //CertificateClass.TestarCertificado();

            ////Permissão local
            //PermissionClass.TestarPermissaoNegada();
            //PermissionClass.GarantirAcesso();

            ////SecureString
            //SecureStringClass.TestarSecureString();

            ////DEBUGG
            //DebuggClass.TestarDebugReleaseTimer();
            //DebuggClass.TestarDiretivas();
            //DebuggClass.TestarAtributoDiretivas();
            //DebuggClass.TestarLinhas();

            //Trace e Log
            //TraceLogEventClass.TestarTrace();
        }

        private static void TestarData()
        {
            //Leitura Escrita String Texto e XML
            //TextoDataClass.TestarWriterReaderXML();
            //TextoDataClass.TestarStreamString();

            //SQL DataAccess
            SqlClass.TestarConsultaReader();
            SqlClass.TestarConsultaTable();
        }
    }
}