using APIPizzeria.Data;
using APIPizzeria.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIPizzeria.DataBase

{
  
    public class PizzaData
    {
        #region properties
        private string sSql { get; set; } = "";
        private readonly SPData EjecutarStoreProcedure;
        #endregion
        public PizzaData()
        {
            EjecutarStoreProcedure = new SPData();
        }
        public List<Pizza> VerCatalogo()
        {
            List<Pizza> listaPizza = new List<Pizza>();

            try
            {
                sSql = "SP_ObtenerPizzas";
                DataTable dt = new DataTable();
                List<SqlParameter> listaparametros = new List<SqlParameter>();
                SqlParameter[] lista = listaparametros.ToArray();
                dt = EjecutarStoreProcedure.Ejecutar(sSql, lista, true);
                foreach (DataRow dr in dt.Rows)
                {
                    Pizza p = new Pizza();
                    p.ID_Pizza = Convert.ToInt32(dr["ID_Pizza"]);
                    p.Nombre = dr["Nombre"].ToString();
                    p.Descripcion = dr["Descripcion"].ToString();
                    p.Imagen = dr["Imagen"].ToString();
                    p.PrecioUnitario = Convert.ToDouble(dr["PrecioUnitario"]);
                    listaPizza.Add(p);

                }

            }
            catch (Exception)
            {

                throw;
            }
            return listaPizza;
      
        }
        public List<Pizza> VerPizzaPorID(int ID)
        {
            List<Pizza> listaPizza = new List<Pizza>();

            try
            {
                sSql = "SP_ObtenerPizzasPorID";
                SqlParameter paramID = new SqlParameter("@ID_Pizza",SqlDbType.Int);
                paramID.Value = ID;
                List<SqlParameter> listaParametros = new List<SqlParameter>();
                listaParametros.Add(paramID);
                SqlParameter[] lista = listaParametros.ToArray();

                DataTable dt = new DataTable();
                dt = EjecutarStoreProcedure.Ejecutar(sSql, lista, true);
                foreach (DataRow dr in dt.Rows)
                {
                    Pizza p = new Pizza();
                    p.ID_Pizza = Convert.ToInt32(dr["ID_Pizza"]);
                    p.Nombre = dr["Nombre"].ToString();
                    p.Descripcion = dr["Descripcion"].ToString();
                    p.Imagen = dr["Imagen"].ToString();
                    p.PrecioUnitario = Convert.ToDouble(dr["PrecioUnitario"]);
                    listaPizza.Add(p);

                }

            }
            catch (Exception)
            {

                throw;
            }
            return listaPizza;
        }
        public void Insertar(Pizza pizza) 
        {
            try
            {
                sSql = "SP_InsertarPizza";
                SqlParameter paramNombre = new SqlParameter("@Nombre", SqlDbType.VarChar, 200);
                paramNombre.Value = pizza.Nombre;
                SqlParameter paramDescripcion = new SqlParameter("@Descripcion", SqlDbType.VarChar, 200);
                paramDescripcion.Value = pizza.Descripcion;
                SqlParameter paramPrecioUnitario = new SqlParameter("@PrecioUnitario", SqlDbType.Float);
                paramPrecioUnitario.Value = pizza.PrecioUnitario;
                SqlParameter paramImagen = new SqlParameter("@Imagen", SqlDbType.VarChar, 200);
                paramImagen.Value = pizza.Imagen;
                SqlParameter paramActivo = new SqlParameter("@Activo", SqlDbType.Bit);
                paramActivo.Value = 1;

                List<SqlParameter> listaParametros = new List<SqlParameter>();
                listaParametros.Add(paramNombre);
                listaParametros.Add(paramDescripcion);
                listaParametros.Add(paramPrecioUnitario);
                listaParametros.Add(paramImagen);
                listaParametros.Add(paramActivo);

                SqlParameter[] lista = listaParametros.ToArray(); 
                EjecutarStoreProcedure.Ejecutar(sSql, lista,false);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Actualizar(Pizza pizza, int ID)
        {
            try
            {
                sSql = "SP_ModificarPizza";
                SqlParameter paramID = new SqlParameter("@ID_Pizza", SqlDbType.Int);
                paramID.Value = ID;
                SqlParameter paramNombre = new SqlParameter("@Nombre", SqlDbType.VarChar, 200);
                paramNombre.Value = pizza.Nombre;
                SqlParameter paramDescripcion = new SqlParameter("@Descripcion", SqlDbType.VarChar, 200);
                paramDescripcion.Value = pizza.Descripcion;
                SqlParameter paramPrecioUnitario = new SqlParameter("@PrecioUnitario", SqlDbType.Float);
                paramPrecioUnitario.Value = pizza.PrecioUnitario;
                SqlParameter paramImagen = new SqlParameter("@Imagen", SqlDbType.VarChar, 200);
                paramImagen.Value = pizza.Imagen;
                SqlParameter paramActivo = new SqlParameter("@Activo", SqlDbType.Bit);
                paramActivo.Value = 1;

                List<SqlParameter> listaParametros = new List<SqlParameter>();
                listaParametros.Add(paramID);
                listaParametros.Add(paramNombre);
                listaParametros.Add(paramDescripcion);
                listaParametros.Add(paramPrecioUnitario);
                listaParametros.Add(paramImagen);
                listaParametros.Add(paramActivo);

                SqlParameter[] lista = listaParametros.ToArray();
                EjecutarStoreProcedure.Ejecutar(sSql, lista, false);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public void Eliminar( int ID)
        {
            try
            {
                sSql = "SP_EliminarPizza";
                SqlParameter paramID = new SqlParameter("@ID_Pizza", SqlDbType.Int);
                paramID.Value = ID;
                SqlParameter paramActivo = new SqlParameter("@Activo", SqlDbType.Bit);
                paramActivo.Value = 0;

                List<SqlParameter> listaParametros = new List<SqlParameter>();
                listaParametros.Add(paramID);
                listaParametros.Add(paramActivo);

                SqlParameter[] lista = listaParametros.ToArray();
                EjecutarStoreProcedure.Ejecutar(sSql, lista, false);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
