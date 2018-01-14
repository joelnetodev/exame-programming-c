using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp._4___Data
{
    public static class SqlClass
    {
        private static string ConnString = "connstring";

        public static void TestarInsert()
        {

        }

        public static void TestarConsultaReader()
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                string query = "SELECT * FROM TabelaTeste where Id = @id";

                //cria um command com a string de conexão
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //adiciona uma parametro no comando
                    command.Parameters.AddWithValue("id", 1);

                    //abre a conexão para poder acessar o banco
                    connection.Open();

                    //executa o datareader para retornar do banco e ler as linhas 
                    //lendo as linhas, pode-se adicionar o valor ao objeto de cada coluna
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) return;

                        while (reader.Read())
                        {
                            var tabelaResult = new TableObj();
                            tabelaResult.Id = reader.GetInt32(0);
                            tabelaResult.Nome = reader.GetString(1);
                        }
                    }
                }

                connection.Close();
            }
        }

        public static void TestarConsultaTable()
        {
            DataTable dt = new DataTable("TabelaTeste");

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                //Retorna uma tabela do banco
                //o data adapter pode ser uma query ou um comand com parametros, como no exemplo anterior
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TabelaTeste", connection);

                //preenche o DataTable com essa tabela
                da.Fill(dt);

                connection.Close();
            }

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                //uma matriz, primeiro a linha e depois a coluna
                //pode ser o nome da coluna ou pode ser o index o retorno
                var id = dt.Rows[r]["Id"].ToString();
                var nome = dt.Rows[r]["Nome"].ToString();
            }
        }

        public class TableObj
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }
    }
}
