using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    //WeakReference é uma solução capaz de alocar um objeto muito grande, para que possa ser reutilizado em diferentes partes do seu codigo
    //ele não aponta para uma referencia no HEAP, por tanto, ele não pode ser GARBAGE COLLETED de maneira convencional (Global.asx)

    public static class WeakClass
    {
        public static void TestarWeakRef()
        {
            WeakReferenceStringClass.GetData();
        }
    }

    public static class WeakReferenceStringClass
    {
        private static WeakReference weakRefDataSystem;

        static WeakReferenceStringClass()
        {
            weakRefDataSystem = new WeakReference(LoadList());
        }

        public static List<string> GetData()
        {
            if (weakRefDataSystem == null)
            {
                weakRefDataSystem = new WeakReference(LoadList());
            }

            if (!weakRefDataSystem.IsAlive)
            {
                weakRefDataSystem.Target = LoadList();
            }

            return weakRefDataSystem.Target as List<string>;
        }

        private static List<string> LoadList()
        {
            var lista = new List<string>();
            for (int i = 0; i < 10000; i++)
            {
                lista.Add(i.ToString("000"));
            }
            return lista;
        }
    }
}
