
using CL_EN;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace CL_AD
{
    public class ADproductos
    {
        public ADproductos()
        {
            
        }

        private readonly string _rutaBasePDFs;
        private readonly string _rutaBaseImagenes;

        public ADproductos(string rutaBasePDFs, string rutaBaseImagenes)
        {
            _rutaBasePDFs = rutaBasePDFs;
            _rutaBaseImagenes = rutaBaseImagenes;
        }

        MiConexion micx = new MiConexion();

        /*
         * PROCESOS DE LISTADO
         */
        public List<ENproductos> VERSION1_SP_listarMenuProductoWeb()
        {
            string cadenaConexion = micx.TraerMiConexion();
            List<ENproductos> lista = new List<ENproductos>();
            try
            {
                using var conexion = new SqlConnection(cadenaConexion);

                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "VERSION1_SP_listarMenuProductoWeb");
                while (reader.Read())
                {
                    ENproductos ent = new ENproductos();
                    ent.Id_producto = DataUtil.ObjectToString(reader["id_producto"]);
                    ent.Codigo = DataUtil.ObjectToString(reader["codigo"]);
                    ent.Nombre = DataUtil.ObjectToString(reader["nombre"]);
                    ent.Descripcion = DataUtil.ObjectToString(reader["descripcion"]);
                    ent.Precio = DataUtil.ObjectToDecimal(reader["precio"]);
                    ent.Descuento = DataUtil.ObjectToDecimal(reader["descuento"]);
                    ent.Stock = DataUtil.ObjectToDecimal(reader["stock"]);
                    ent.NombreSlug = DataUtil.ObjectToString(reader["nombre_slug"]);
                    ent.UrlImagen1 = DataUtil.ObjectToString(reader["urlImagen1"]);
                    ent.UrlImagen2 = DataUtil.ObjectToString(reader["urlImagen2"]);
                    ent.Marca = DataUtil.ObjectToString(reader["marca"]);
                    ent.Categoria = DataUtil.ObjectToString(reader["categoria"]);
                    ent.Slug_Categoria = DataUtil.ObjectToString(reader["slug_categoria"]);
                    ent.MensajeConsulta = "Exito";
                    ent.ValorConsulta = "1";
                    lista.Add(ent);
                }
            }
            catch (Exception ex)
            {
                lista.Clear();
                ENproductos ent = new ENproductos();
                ent.MensajeConsulta = "ERROR : " + ex.Message;
                ent.ValorConsulta = "-99";
                lista.Add(ent);
            }
            return lista;
        }

        public List<ENcategorias> VERSION1_SP_listar_categorias_productos()
        {
            string cadenaConexion = micx.TraerMiConexion();
            List<ENcategorias> lista = new List<ENcategorias>();
            try
            {
                using var conexion = new SqlConnection(cadenaConexion);

                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "VERSION1_SP_listar_categorias_productos");
                while (reader.Read())
                {
                    ENcategorias ent = new ENcategorias();
                    ent.Id_categoria = DataUtil.ObjectToString(reader["id_categoria"]);
                    ent.Nombre = DataUtil.ObjectToString(reader["nombre"]);
                    ent.Descripcion = DataUtil.ObjectToString(reader["descripcion"]);
                    ent.Slug_categoria = DataUtil.ObjectToString(reader["slug_categoria"]);
                    ent.Imagen_categoria = DataUtil.ObjectToString(reader["imagen_categoria"]);
                    ent.MensajeConsulta = "Exito";
                    ent.ValorConsulta = "1";
                    lista.Add(ent);
                }
            }
            catch (Exception ex)
            {
                lista.Clear();
                ENcategorias ent = new ENcategorias();
                ent.MensajeConsulta = "ERROR : " + ex.Message;
                ent.ValorConsulta = "-99";
                lista.Add(ent);
            }
            return lista;
        }

        public List<ENmarcas> VERSION1_SP_listar_marcas()
        {
            string cadenaConexion = micx.TraerMiConexion();
            List<ENmarcas> lista = new List<ENmarcas>();
            try
            {
                using var conexion = new SqlConnection(cadenaConexion);

                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "VERSION1_SP_listar_marcas");
                while (reader.Read())
                {
                    ENmarcas ent = new ENmarcas();
                    ent.Id_marcas = DataUtil.ObjectToString(reader["id_marca"]);
                    ent.Nombre = DataUtil.ObjectToString(reader["nombre"]);
                    ent.Descripcion = DataUtil.ObjectToString(reader["descripcion"]);
                    ent.Slug_marca = DataUtil.ObjectToString(reader["slug_marca"]);
                    ent.Imagen_marca = DataUtil.ObjectToString(reader["imagen_marca"]);
                    ent.MensajeConsulta = "Exito";
                    ent.ValorConsulta = "1";
                    lista.Add(ent);
                }
            }
            catch (Exception ex)
            {
                lista.Clear();
                ENmarcas ent = new ENmarcas();
                ent.MensajeConsulta = "ERROR : " + ex.Message;
                ent.ValorConsulta = "-99";
                lista.Add(ent);
            }
            return lista;
        }

        public List<ENunidadesMedida> VERSION1_SP_Listar_Unidades_Medida()
        {
            string cadenaConexion = micx.TraerMiConexion();
            List<ENunidadesMedida> Lista = new List<ENunidadesMedida>();

            try
            {
                using var conexion = new SqlConnection(cadenaConexion);
                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "VERSION1_SP_Listar_Unidades_Medida");

                while (reader.Read())
                {
                    ENunidadesMedida ent = new ENunidadesMedida();
                    ent.Id_unidad = DataUtil.ObjectToString(reader["id_unidad_medida"]);
                    ent.Nombre = DataUtil.ObjectToString(reader["nombre"]);
                    ent.Abreviatura = DataUtil.ObjectToString(reader["abreviatura"]);
                    ent.MensajeConsulta = "Exito";
                    ent.ValorConsulta = "1";
                    Lista.Add(ent);
                }
            }
            catch (Exception ex)
            {
                Lista.Clear();
                ENunidadesMedida ent = new ENunidadesMedida();
                ent.MensajeConsulta = "ERROR: " + ex.Message;
                ent.ValorConsulta = "-99";
                Lista.Add(ent);
            }
            return Lista;
        }

        /*Crud Productos*/
        public List<ENproductos> VERSION1_SP_ListarProductos()
        {
            string cadenaConexion = micx.TraerMiConexion();
            List<ENproductos> lista = new List<ENproductos>();
            try
            {
                using var conexion = new SqlConnection(cadenaConexion);
                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "VERSION1_SP_listar_productos");

                while (reader.Read()) // 🚩 Usamos WHILE en vez de IF para recorrer TODOS
                {
                    ENproductos ent = new ENproductos();
                    ent.Id_producto = DataUtil.ObjectToString(reader["id_producto"]);
                    ent.Codigo = DataUtil.ObjectToString(reader["codigo"]);
                    ent.Nombre = DataUtil.ObjectToString(reader["nombre"]);
                    ent.Descripcion = DataUtil.ObjectToString(reader["descripcion"]);
                    ent.Precio = DataUtil.ObjectToDecimal(reader["precio"]);
                    ent.Descuento = DataUtil.ObjectToDecimal(reader["descuento"]);
                    ent.Stock = DataUtil.ObjectToDecimal(reader["stock"]);
                    ent.NombreSlug = DataUtil.ObjectToString(reader["nombre_slug"]);
                    ent.UrlImagen1 = DataUtil.ObjectToString(reader["urlimagen1"]);
                    ent.UrlImagen2 = DataUtil.ObjectToString(reader["urlimagen2"]);
                    ent.ficha = DataUtil.ObjectToString(reader["ficha"]);
                    ent.Id_marca = DataUtil.ObjectToString(reader["id_marca"]);
                    ent.Marca = DataUtil.ObjectToString(reader["marca"]);
                    ent.Id_categoria = DataUtil.ObjectToString(reader["id_categoria"]);
                    ent.Categoria = DataUtil.ObjectToString(reader["categoria"]);
                    ent.Slug_Categoria = DataUtil.ObjectToString(reader["slug_categoria"]);
                    ent.Id_unidad = DataUtil.ObjectToString(reader["id_unidad_medida"]);
                    ent.Unidad = DataUtil.ObjectToString(reader["Unidad de medida"]);
                    ent.MensajeConsulta = "Exito";
                    ent.ValorConsulta = "1";
                    lista.Add(ent); 
                }

                if (lista.Count == 0)
                {
                    ENproductos ent = new ENproductos();
                    ent.MensajeConsulta = "No se encontró ningún producto";
                    ent.ValorConsulta = "0";
                    lista.Add(ent);
                }
            }
            catch (Exception ex)
            {
                lista.Clear();
                ENproductos ent = new ENproductos();
                ent.MensajeConsulta = "ERROR : " + ex.Message;
                ent.ValorConsulta = "-99";
                lista.Add(ent);
            }

            return lista;
        }

        public ENproductos VERSION1_SP_ListarProductoxID(int idProducto)
        {
            string cadenaConexion = micx.TraerMiConexion();
            ENproductos ent = new ENproductos();

            try
            {
                using var conexion = new SqlConnection(cadenaConexion);

                SqlParameter[] parametros = new SqlParameter[]
                {
                new SqlParameter("@id_producto", SqlDbType.Int) { Value = idProducto }
                };

                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SP_ListarProductoxID", parametros);

                if (reader.Read())
                {
                    ent.Id_producto = DataUtil.ObjectToString(reader["id_producto"]);
                    ent.Codigo = DataUtil.ObjectToString(reader["codigo"]);
                    ent.Nombre = DataUtil.ObjectToString(reader["nombre"]);
                    ent.Descripcion = DataUtil.ObjectToString(reader["descripcion"]);
                    ent.Precio = DataUtil.ObjectToDecimal(reader["precio"]);
                    ent.Descuento = DataUtil.ObjectToDecimal(reader["descuento"]);
                    ent.Stock = DataUtil.ObjectToDecimal(reader["stock"]);
                    ent.NombreSlug = DataUtil.ObjectToString(reader["nombre_slug"]);
                    ent.UrlImagen1 = DataUtil.ObjectToString(reader["urlimagen1"]);
                    ent.UrlImagen2 = DataUtil.ObjectToString(reader["urlimagen2"]);
                    ent.ficha = DataUtil.ObjectToString(reader["ficha"]);
                    ent.Id_marca = DataUtil.ObjectToString(reader["id_marca"]);
                    ent.Marca = DataUtil.ObjectToString(reader["marca"]);
                    ent.Id_categoria = DataUtil.ObjectToString(reader["id_categoria"]);
                    ent.Categoria = DataUtil.ObjectToString(reader["categoria"]);
                    ent.Slug_Categoria = DataUtil.ObjectToString(reader["slug_categoria"]);
                    ent.Id_unidad = DataUtil.ObjectToString(reader["id_unidad_medida"]);
                    ent.Unidad = DataUtil.ObjectToString(reader["Unidad de medida"]);
                    ent.MensajeConsulta = "Exito";
                    ent.ValorConsulta = "1";

                    if (!string.IsNullOrEmpty(ent.ficha))
                    {
                        var nombreArchivo = Path.GetFileName(ent.ficha);
                        var rutaArchivo = Path.Combine(_rutaBasePDFs, nombreArchivo);

                        if (System.IO.File.Exists(rutaArchivo))
                        {
                            byte[] fileBytes = System.IO.File.ReadAllBytes(rutaArchivo);
                            ent.ArchivoFichaBase64 = Convert.ToBase64String(fileBytes);
                        }
                        else
                        {
                            ent.ArchivoFichaBase64 = null;
                        }
                    }
                    else
                    {
                        ent.ArchivoFichaBase64 = null;
                    }

                    var imagenes = new ADarchivos().VERSION1_SP_ListarImagenesPorProducto(ent.Id_producto, _rutaBaseImagenes);
                    ent.Imagenes = imagenes ?? new List<ENImagenProducto>();

                }
                else
                {
                    ent.MensajeConsulta = "No se encontró el producto";
                    ent.ValorConsulta = "0";
                }
            }
            catch (Exception ex)
            {
                ent = new ENproductos();
                ent.MensajeConsulta = "ERROR : " + ex.Message;
                ent.ValorConsulta = "-97";
            }
            return ent;
        }

        public ENproductos VERSION1_SP_guardarProductoWeb(ENproductos DatosEnviados)
        {
            string cadenaConexion = micx.TraerMiConexion();
            ENproductos ent = new ENproductos();
            try
            {
                using var conexion = new SqlConnection(cadenaConexion);

                SqlParameter[] Parametro = new SqlParameter[12];
                Parametro[0] = new SqlParameter("@codigo", SqlDbType.VarChar);
                Parametro[0].Direction = ParameterDirection.Input;
                Parametro[0].Value = DatosEnviados.Codigo;

                Parametro[1] = new SqlParameter("@nombre", SqlDbType.VarChar);
                Parametro[1].Direction = ParameterDirection.Input;
                Parametro[1].Value = DatosEnviados.Nombre;

                Parametro[2] = new SqlParameter("@descripcion", SqlDbType.VarChar);
                Parametro[2].Direction = ParameterDirection.Input;
                Parametro[2].Value = DatosEnviados.Descripcion;

                Parametro[3] = new SqlParameter("@precio", SqlDbType.Decimal);
                Parametro[3].Direction = ParameterDirection.Input;
                Parametro[3].Value = Convert.ToDecimal(DatosEnviados.Precio);

                Parametro[4] = new SqlParameter("@descuento", SqlDbType.VarChar);
                Parametro[4].Direction = ParameterDirection.Input;
                Parametro[4].Value = Convert.ToDecimal(DatosEnviados.Descuento);

                Parametro[5] = new SqlParameter("@stock", SqlDbType.VarChar);
                Parametro[5].Direction = ParameterDirection.Input;
                Parametro[5].Value = Convert.ToDecimal(DatosEnviados.Stock);

                Parametro[6] = new SqlParameter("@nombre_slug", SqlDbType.VarChar);
                Parametro[6].Direction = ParameterDirection.Input;
                Parametro[6].Value = DatosEnviados.NombreSlug;

                Parametro[7] = new SqlParameter("@urlimagen1", SqlDbType.VarChar);
                Parametro[7].Direction = ParameterDirection.Input;
                Parametro[7].Value = DatosEnviados.UrlImagen1;

                Parametro[8] = new SqlParameter("@urlimagen2", SqlDbType.VarChar);
                Parametro[8].Direction = ParameterDirection.Input;
                Parametro[8].Value = DatosEnviados.UrlImagen2;

                Parametro[9] = new SqlParameter("@id_marca", SqlDbType.VarChar);
                Parametro[9].Direction = ParameterDirection.Input;
                Parametro[9].Value = DatosEnviados.Marca;

                Parametro[10] = new SqlParameter("@id_categoria", SqlDbType.VarChar);
                Parametro[10].Direction = ParameterDirection.Input;
                Parametro[10].Value = DatosEnviados.Categoria;

                Parametro[11] = new SqlParameter("@id_usuario", SqlDbType.VarChar);
                Parametro[11].Direction = ParameterDirection.Input;
                Parametro[11].Value = DatosEnviados.Id_Usuario;


                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "VERSION1_SP_guardarProductoWeb", Parametro);
                while (reader.Read())
                {
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

        public ENproductos SP_EditarProducto(ENproductos DatosEnviados)
        {
            string cadenaConexion = micx.TraerMiConexion();
            ENproductos ent = new ENproductos();
            try
            {
                using var conexion = new SqlConnection(cadenaConexion);

                SqlParameter[] Parametro = new SqlParameter[15];

                Parametro[0] = new SqlParameter("@Id_producto", SqlDbType.Int);
                Parametro[0].Direction = ParameterDirection.Input;
                Parametro[0].Value = int.Parse(DatosEnviados.Id_producto);

                Parametro[1] = new SqlParameter("@Codigo", SqlDbType.VarChar, 100);
                Parametro[1].Direction = ParameterDirection.Input;
                Parametro[1].Value = DatosEnviados.Codigo;

                Parametro[2] = new SqlParameter("@Nombre", SqlDbType.VarChar, 500);
                Parametro[2].Direction = ParameterDirection.Input;
                Parametro[2].Value = DatosEnviados.Nombre;

                Parametro[3] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                Parametro[3].Direction = ParameterDirection.Input;
                Parametro[3].Value = DatosEnviados.Descripcion;

                Parametro[4] = new SqlParameter("@Precio", SqlDbType.Decimal);
                Parametro[4].Precision = 18;
                Parametro[4].Scale = 5;
                Parametro[4].Direction = ParameterDirection.Input;
                Parametro[4].Value = DatosEnviados.Precio;

                Parametro[5] = new SqlParameter("@Descuento", SqlDbType.Decimal);
                Parametro[5].Precision = 18;
                Parametro[5].Scale = 5;
                Parametro[5].Direction = ParameterDirection.Input;
                Parametro[5].Value = DatosEnviados.Descuento;

                Parametro[6] = new SqlParameter("@Stock", SqlDbType.Decimal);
                Parametro[6].Precision = 18;
                Parametro[6].Scale = 5;
                Parametro[6].Direction = ParameterDirection.Input;
                Parametro[6].Value = DatosEnviados.Stock;

                Parametro[7] = new SqlParameter("@Id_marca", SqlDbType.Int);
                Parametro[7].Direction = ParameterDirection.Input;
                Parametro[7].Value = int.Parse(DatosEnviados.Id_marca);
                
                Parametro[8] = new SqlParameter("@NombreSlug", SqlDbType.VarChar, 60);
                Parametro[8].Direction = ParameterDirection.Input;
                Parametro[8].Value = DatosEnviados.NombreSlug;

                Parametro[9] = new SqlParameter("@UrlImagen1", SqlDbType.VarChar);
                Parametro[9].Direction = ParameterDirection.Input;
                Parametro[9].Value = DatosEnviados.UrlImagen1;

                Parametro[10] = new SqlParameter("@UrlImagen2", SqlDbType.VarChar);
                Parametro[10].Direction = ParameterDirection.Input;
                Parametro[10].Value = DatosEnviados.UrlImagen2;

                Parametro[11] = new SqlParameter("@Usu2", SqlDbType.Int);
                Parametro[11].Direction = ParameterDirection.Input;
                Parametro[11].Value = int.Parse(DatosEnviados.Id_Usuario);

                Parametro[12] = new SqlParameter("@Id_categoria", SqlDbType.Int);
                Parametro[12].Direction = ParameterDirection.Input;
                Parametro[12].Value = int.Parse(DatosEnviados.Id_categoria);

                Parametro[13] = new SqlParameter("@Id_unidad", SqlDbType.Int);
                Parametro[13].Direction = ParameterDirection.Input;
                Parametro[13].Value = int.Parse(DatosEnviados.Id_unidad);

                Parametro[14] = new SqlParameter("@ficha", SqlDbType.VarChar);
                Parametro[14].Direction = ParameterDirection.Input;
                Parametro[14].Value = DatosEnviados.ficha;

                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "SP_EditarProducto", Parametro);
                while (reader.Read())
                {
                    ent.ValorConsulta = DataUtil.ObjectToString(reader["Resultado"]);
                }


            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "ERROR: " + ex.Message;
                ent.ValorConsulta = "-99";
            }
            return ent;
        }

        public ENproductos EditarImagenProducto(int id_producto, string url_imagen, int es_principal, int es_principal2, int id_usuario)
        {
            string cadenaConexion = micx.TraerMiConexion();
            ENproductos ent = new ENproductos();

            try
            {
                using var conexion = new SqlConnection(cadenaConexion);

                SqlParameter[] Parametro = new SqlParameter[5];

                Parametro[0] = new SqlParameter("@id_producto", SqlDbType.Int);
                Parametro[0].Direction = ParameterDirection.Input;
                Parametro[0].Value = id_producto;

                Parametro[1] = new SqlParameter("@url_imagen", SqlDbType.VarChar, 500);
                Parametro[1].Direction = ParameterDirection.Input;
                Parametro[1].Value = url_imagen;

                Parametro[2] = new SqlParameter("@es_principal", SqlDbType.Bit);
                Parametro[2].Direction = ParameterDirection.Input;
                Parametro[2].Value = es_principal;

                Parametro[3] = new SqlParameter("@es_principal2", SqlDbType.Bit);
                Parametro[3].Direction = ParameterDirection.Input;
                Parametro[3].Value = es_principal2;

                Parametro[4] = new SqlParameter("@id_usuario", SqlDbType.Int);
                Parametro[4].Direction = ParameterDirection.Input;
                Parametro[4].Value = id_usuario;

                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "VERSION1_SP_EditarImage", Parametro);
                while (reader.Read())
                {
                    ent.ValorConsulta = DataUtil.ObjectToString(reader["Resultado"]);
                }
            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "ERROR: " + ex.Message;
                ent.ValorConsulta = "-99";
            }

            return ent;
        }






        /*
         * PROCESOS DE GUARDADO
         */

        public ENcategorias VERSION1_SP_guardar_categorias_productos(ENcategorias DatosEnviados)
        {
            string cadenaConexion = micx.TraerMiConexion();
            ENcategorias ent = new ENcategorias();
            try
            {
                using var conexion = new SqlConnection(cadenaConexion);

                SqlParameter[] Parametro = new SqlParameter[5];
                Parametro[0] = new SqlParameter("@nombre", SqlDbType.VarChar);
                Parametro[0].Direction = ParameterDirection.Input;
                Parametro[0].Value = DatosEnviados.Nombre;

                Parametro[1] = new SqlParameter("@descripcion", SqlDbType.VarChar);
                Parametro[1].Direction = ParameterDirection.Input;
                Parametro[1].Value = DatosEnviados.Descripcion;

                Parametro[2] = new SqlParameter("@slug_categoria", SqlDbType.VarChar);
                Parametro[2].Direction = ParameterDirection.Input;
                Parametro[2].Value = DatosEnviados.Slug_categoria.ToLower();

                Parametro[3] = new SqlParameter("@imagen_categoria", SqlDbType.VarChar);
                Parametro[3].Direction = ParameterDirection.Input;
                Parametro[3].Value = DatosEnviados.Imagen_categoria;

                Parametro[4] = new SqlParameter("@id_usuario", SqlDbType.VarChar);
                Parametro[4].Direction = ParameterDirection.Input;
                Parametro[4].Value = DatosEnviados.Id_usuario;

                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "VERSION1_SP_guardar_categorias_productos", Parametro);
                while (reader.Read())
                {
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

        public ENmarcas VERSION1_SP_guardar_marcas_productos(ENmarcas DatosEnviados)
        {
            string cadenaConexion = micx.TraerMiConexion();
            ENmarcas ent = new ENmarcas();
            try
            {
                using var conexion = new SqlConnection(cadenaConexion);

                SqlParameter[] Parametro = new SqlParameter[5];
                Parametro[0] = new SqlParameter("@nombre", SqlDbType.VarChar);
                Parametro[0].Direction = ParameterDirection.Input;
                Parametro[0].Value = DatosEnviados.Nombre;

                Parametro[1] = new SqlParameter("@descripcion", SqlDbType.VarChar);
                Parametro[1].Direction = ParameterDirection.Input;
                Parametro[1].Value = DatosEnviados.Descripcion;

                Parametro[2] = new SqlParameter("@slug_marca", SqlDbType.VarChar);
                Parametro[2].Direction = ParameterDirection.Input;
                Parametro[2].Value = DatosEnviados.Slug_marca.ToLower();

                Parametro[3] = new SqlParameter("@imagen_marca", SqlDbType.VarChar);
                Parametro[3].Direction = ParameterDirection.Input;
                Parametro[3].Value = DatosEnviados.Imagen_marca;

                Parametro[4] = new SqlParameter("@id_usuario", SqlDbType.VarChar);
                Parametro[4].Direction = ParameterDirection.Input;
                Parametro[4].Value = DatosEnviados.Id_usuario;

                using var reader = SqlHelper.ExecuteReader(conexion, CommandType.StoredProcedure, "VERSION1_SP_guardar_marcas_productos", Parametro);
                while (reader.Read())
                {
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
