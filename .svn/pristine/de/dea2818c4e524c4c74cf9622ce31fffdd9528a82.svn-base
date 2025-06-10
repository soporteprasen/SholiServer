using CL_EN;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CL_AD
{
    public class ADusuarios
    {
        MiConexion micx = new MiConexion();

        public ENusuarios VERSION1_SP_buscar_usuario(ENusuarios DatosEnviados)
        {
            string cadenaConexion = micx.TraerMiConexion();
            ENusuarios ent = new ENusuarios();
            try
            {
                using var conexion = new SqlConnection(cadenaConexion);

                SqlParameter[] Parametro = new SqlParameter[2];
                Parametro[0] = new SqlParameter("@usuario", SqlDbType.VarChar);
                Parametro[0].Direction = ParameterDirection.Input;
                Parametro[0].Value = DatosEnviados.UsuarioNombre;

                Parametro[1] = new SqlParameter("@clave", SqlDbType.VarChar);
                Parametro[1].Direction = ParameterDirection.Input;
                Parametro[1].Value = DatosEnviados.Clave;

                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "VERSION1_SP_buscar_usuario", Parametro);
                while (reader.Read())
                {
                    ent.IdUsuario = DataUtil.ObjectToInt(reader["IdUsuario"]);
                    ent.UsuarioNombre = DataUtil.ObjectToString(reader["UsuarioNombre"]);
                    ent.ValorConsulta = DataUtil.ObjectToString(reader["Resultado"]);
                }
            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "ERROR : " + ex.Message;
                ent.ValorConsulta = "-99";
            }
            return ent;
        }
    }
}
