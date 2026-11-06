using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.DAL
{
    public class DAL_RP
    {
        public string Insertar_RP(Entitie_RP obj)
        {
            string Mensaje = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion = Conexion.conectardb();
                SqlCommand cmd = new SqlCommand("Insertar_RP", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Nombre_Cultivo", SqlDbType.VarChar).Value = obj.Nombre_Cultivo;
                cmd.Parameters.Add("@Tipo_Cultivo", SqlDbType.VarChar).Value = obj.Tipo_Cultivo;
                cmd.Parameters.Add("@Categoria_Cultivo", SqlDbType.VarChar).Value = obj.Categoria_Cultivo;
                cmd.Parameters.Add("@Estacion", SqlDbType.VarChar).Value = obj.Estacion_Siembra;
                cmd.Parameters.Add("@Tipo_Suelo", SqlDbType.VarChar).Value = obj.Tipo_Suelo;
                cmd.Parameters.Add("@Duracion", SqlDbType.VarChar).Value = obj.Duracion;

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Mensaje = "Datos ingresados correctamente";
                }
                else
                {
                    Mensaje = "Ha ocurrido un problema al ingresar los datos";
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open) { conexion.Close(); }
            }
            return Mensaje;
        }
    }
}
