using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._2___Types
{
    //Expression Tree
    //Descreve o codigo, não é o codigo em sí.
    //Modifica e cria, como o javascript faz com DOM HTML
    public static class CodeDOMClass
    {
        public static void TestarCodeDOMexpression()
        {
            BlockExpression blkExp = Expression.Block(
                Expression.Call(null, typeof (Console).GetMethod("WriteLine", new[] {typeof (String)}),Expression.Constant("Teste Método Gerado Expression"))
                );

            var teste = Expression.Lambda<Action>(blkExp).Compile();

            teste();


            Expression<Action<int>> printExpression = param => Console.WriteLine(param * 10);

            var PrintTimes10 = printExpression.Compile();

            PrintTimes10(10);
        }
    }
}
