using System;
using Microsoft.Data.SqlClient;

namespace EstudosADONET
{
    class Program
    {
        static void Main(string[] args)
        {
            //para se conectar ao banco ↓
            const string connectString = "Server=MATHEUS;Database=Estudos;User ID=sa;Password=123456";

            //Microsoft.Data.SqlClient = pacote nuget
            //dotnet add package microsoft.data.sqlclient --version 2.1.3

            var connection = new SqlConnection(connectString); //sempre deve abrir e fechar a conexão

#region comentado
            #region Errado
            connection.Open();
            // insert
            connection.Close();
            connection.Open();
            // update
            connection.Close();
            #endregion

            #region Certo
            connection.Open();
            // insert
            // update
            connection.Close();
            #endregion

            #region Dispose ("destruir" a conexão)
            connection.Open();

            connection.Close();

            connection.Dispose();
            //apos o dispose é necessario dar um novo new SqlConnection()
            #endregion

            #region Using
            using (var connection2 = new SqlConnection(connectString))
            {
                Console.WriteLine("Conectado");
                connection2.Open(); //garantindo que está aberto

                using (var command = new SqlCommand())
                {
                    command.Connection = connection2; //garantir que ela está aberta
                    command.CommandType = System.Data.CommandType.Text; //o tipo do command que eu vou executar
                    command.CommandText = "SELECT ctg.Id, ctg.Title, ctg.Url FROM Category ctg"; // comando sql

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)} - exemple.com.br/{reader.GetString(2)}");
                    }
                }
            }
            #endregion
#endregion
        }
    }
}