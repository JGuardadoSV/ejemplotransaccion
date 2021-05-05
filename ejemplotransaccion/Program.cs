using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejemplotransaccion
{
    class Program
    {
        static void Main(string[] args)
        {

            string cadena = "Data Source=.;Initial Catalog=CuentaBancaria;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                connection.Open();

                // Iniciando una transaccion local.
                SqlTransaction sqlTran = connection.BeginTransaction();

                // Asignando la trasaccion a un objeto sqlcommand.
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {
                    
                    command.CommandText ="Insert into Cuenta values ('Prueba 11',200)";
                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into Cuenta values ('Prueba 22',200)";
                    command.ExecuteNonQuery();

                    // hacer Commit
                    sqlTran.Commit();
                    Console.WriteLine("Registro insertados.");
                }
                catch (Exception ex)
                {

                }


            }
            //libera memoria automaticamente

        }
    }
}
