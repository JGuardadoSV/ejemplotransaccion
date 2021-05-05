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
                    command.CommandText ="Insert into Cuenta values ('Prueba 333',200)";
                    command.ExecuteNonQuery();
                    command.CommandText = "Insert into Cuenta values ('Prueba 444',200)";
                    command.ExecuteNonQuery();
                    command.CommandText = "Insert into Cuenta values ('Prueba 555',200,111111)"; // hará rollback porque generará un error
                    command.ExecuteNonQuery();
                    //command.CommandText = "Insert into Cuenta values ('Prueba 44',200,65656)";
                    //command.ExecuteNonQuery();
                    // hacer Commit
                    sqlTran.Commit();
                    Console.WriteLine("Registros insertados.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        // hacer roll back .
                        sqlTran.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
            //libera memoria automaticamente

        }
    }
}
