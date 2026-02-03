using APIPizzeria.DataBase;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;

namespace APIPizzeria.Data
{
    public class SPData
    {
        public DataTable Ejecutar(string spSQL, SqlParameter[] listaParametros, bool RetTabla = false) 
        {
            DataTable DT= new DataTable();
            using (SqlConnection conexion = new SqlConnection(ConexionBDD.RutaConexion)) 
            {
                conexion.Open();
                SqlCommand command = new SqlCommand(spSQL, conexion);
                command.CommandType = CommandType.StoredProcedure; ;
                if(listaParametros != null)
                {
                    command.Parameters.AddRange(listaParametros);
                }
                if (RetTabla) 
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    { DT.Load(reader); }
                }
                else 
                { command.ExecuteNonQuery(); }
            }
            return DT;
        }
    }
}
