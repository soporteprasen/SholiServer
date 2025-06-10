using CL_EN;
using CL_LN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SholiServer.ConexionAplicaciones;
using System.Text.Json;

namespace SholiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CTproductosController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public CTproductosController(IWebHostEnvironment env)
        {
            _env = env;
        }

        /*
         * PROCESOS DE LISTADOS
         */

        [HttpPost("listarProductosPrincipales")]
        public async Task<IActionResult> listarProductosPrincipales([FromBody] ENrecibirDatosCargaInicial DatosEnviados)
        {
            List<ENproductos> resFinal = new List<ENproductos>();

            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    resFinal = new LNproductos().listarProductosDestacados();
                }
                else
                {
                    ENproductos ent = new ENproductos();
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                    resFinal.Add(ent);
                }
            }
            catch (Exception ex)
            {
                ENproductos ent = new ENproductos();
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
                resFinal.Add(ent);
            }

            return Ok(resFinal);
        }

        [HttpPost("listarCategoriasProductos")]
        public async Task<IActionResult> listarCategoriasProductos([FromBody] ENrecibirListarAtributos DatosEnviados)
        {
            List<ENcategorias> resFinal = new List<ENcategorias>();

            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    resFinal = new LNproductos().listarCategoriasProductos();
                }
                else
                {
                    ENcategorias ent = new ENcategorias();
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                    resFinal.Add(ent);
                }
            }
            catch (Exception ex)
            {
                ENcategorias ent = new ENcategorias();
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
                resFinal.Add(ent);
            }

            return Ok(resFinal);
        }

        [HttpPost("listarMarcasProductos")]
        public async Task<IActionResult> listarMarcasProductos([FromBody] ENrecibirListarAtributos DatosEnviados)
        {
            List<ENmarcas> resFinal = new List<ENmarcas>();

            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    resFinal = new LNproductos().listarMarcasProductos();
                }
                else
                {
                    ENmarcas ent = new ENmarcas();
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                    resFinal.Add(ent);
                }
            }
            catch (Exception ex)
            {
                ENmarcas ent = new ENmarcas();
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
                resFinal.Add(ent);
            }

            return Ok(resFinal);
        }

        [HttpPost("listarUnidadesMedida")]
        public async Task<IActionResult> listarUnidadesMedida([FromBody] ENunidadesMedida DatosEnviados)
        {
            List<ENunidadesMedida> resFinal = new List<ENunidadesMedida>();

            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    resFinal = new LNproductos().listarUnidadesMedida();
                }
                else
                {
                    ENunidadesMedida ent = new ENunidadesMedida();
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                    resFinal.Add(ent);
                }
            }
            catch (Exception ex)
            {
                ENunidadesMedida ent = new ENunidadesMedida();
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
                resFinal.Add(ent);
            }

            return Ok(resFinal);
        }

        [HttpPost("listarProductos")]
        public async Task<IActionResult> ListarProductos([FromBody] ENproductos DatosEnviados)
        {
            List<ENproductos> resFinal = new List<ENproductos>();

            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    resFinal = new LNproductos().ListarProductos();
                }
                else
                {
                    ENproductos ent = new ENproductos();
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                    resFinal.Add(ent);
                }
            }
            catch (Exception ex)
            {
                ENproductos ent = new ENproductos();
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
                resFinal.Add(ent);
            }

            return Ok(resFinal);
        }

        //[Authorize]
        //[HttpPost("ListarImagenes")]
        //public async Task<IActionResult> ListarImagenes([FromBody] ENproductos DatosEnviados)
        //{
        //    ENproductos;
        //}


        [Authorize]
        [HttpPost("ListarProductoxID")]
        public async Task<IActionResult> ListarxID([FromBody] ENproductos DatosEnviados)
        {
            ENproductos resFinal = new ENproductos();

            try
            {
                var idUsuarioJWT = User.FindFirst("id_usuario")?.Value;

                if (string.IsNullOrEmpty(idUsuarioJWT))
                {
                    resFinal.MensajeConsulta = "No se pudo obtener el ID de usuario del token.";
                    resFinal.ValorConsulta = "-101";
                    return Ok(resFinal);
                }

                DatosEnviados.Id_Usuario = idUsuarioJWT;

                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    var rutaPDFs = Path.Combine(_env.ContentRootPath, "pdfs");
                    var rutaImagenes = _env.ContentRootPath;
                    resFinal = new LNproductos().ListarProductoxID(int.Parse(DatosEnviados.Id_producto), rutaPDFs, rutaImagenes);
                }
                else
                {
                    resFinal.MensajeConsulta = "Token incorrecto";
                    resFinal.ValorConsulta = "-98";
                }
            }
            catch (Exception ex)
            {
                resFinal.MensajeConsulta = "Entro al catch -> " + ex.Message;
                resFinal.ValorConsulta = "-100";
            }

            return Ok(resFinal);
        }

        [HttpPost("TraerMensajeWsp")]
        public async Task<IActionResult> TraerMensajewsp([FromBody] ENrecibirListarAtributos DatosEnviados)
        {
            ENMensajeWsp ent = new ENMensajeWsp();
            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    ent = new LNproductos().TraerMensaje();
                }
                else
                {
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                }
            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
            }

            return Ok(ent);
        }

        public class ENrecibirDatosCargaInicial()
        {
            public string Token { get; set; }
            public string Vista { get; set; }

        }

        public class ENrecibirListarAtributos()
        {
            public string Token { get; set; }

        }

        /*
         * PROCESOS DE GUARDADO
         */
        [Authorize]
        [HttpPost("guardarCrearProducto")]
        public async Task<IActionResult> guardarCrearProducto([FromBody] ENproductos DatosEnviados)
        {
            ENproductos ent = new ENproductos();
            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    //LLAMAR AL GESGLI PARA OBTENER EL NOMBRE DEL SLUG DEL PRODUCTO

                    var nombre_slug = new ConexionGescli().ObtenerSlugProductoIndividual(DatosEnviados.Nombre);


                    if (nombre_slug.ValorConsulta == "1" && nombre_slug.NombreSlug.Length > 3)
                    {
                        DatosEnviados.NombreSlug = nombre_slug.NombreSlug;
                        ent = new LNproductos().guardarCrearProducto(DatosEnviados);
                    }
                    else
                    {
                        ent.MensajeConsulta = "ERROR : No se ha obtendido un valorConsulta o slug Valido " + nombre_slug.MensajeConsulta;
                        ent.ValorConsulta = nombre_slug.ValorConsulta;
                    }

                }
                else
                {
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                }
            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
            }

            return Ok(ent);
        }

        [Authorize]
        [HttpPost("guardarCrearCategoria")]
        public async Task<IActionResult> guardarCrearCategoria([FromBody] ENcategorias DatosEnviados)
        {
            ENcategorias ent = new ENcategorias();
            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    //LLAMAR AL GESGLI PARA OBTENER EL NOMBRE DEL SLUG DEL PRODUCTO

                    //var nombre_slug_categoria = new ConexionGescli().ObtenerSlugProductoIndividual(DatosEnviados.Nombre);
                    var slug_marca = DatosEnviados.Nombre.Replace(" ", "-");
                    DatosEnviados.Slug_categoria = slug_marca;

                    ent = new LNproductos().guardarCrearCategoria(DatosEnviados);

                    //if (nombre_slug_categoria.ValorConsulta == "1" && nombre_slug_categoria.NombreSlug.Length > 3)
                    //{
                    //    DatosEnviados.Slug_categoria = nombre_slug_categoria.NombreSlug;
                    //    ent = new LNproductos().guardarCrearCategoria(DatosEnviados);
                    //}
                    //else
                    //{
                    //    ent.MensajeConsulta = "ERROR : No se ha obtendido un valorConsulta o slug Valido " + nombre_slug_categoria.MensajeConsulta;
                    //    ent.ValorConsulta = nombre_slug_categoria.ValorConsulta;
                    //}

                }
                else
                {
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                }
            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
            }

            return Ok(ent);
        }

        [Authorize]
        [HttpPost("guardarCrearMarca")]
        public async Task<IActionResult> guardarCrearMarca([FromBody] ENmarcas DatosEnviados)
        {
            ENmarcas ent = new ENmarcas();
            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    //LLAMAR AL GESGLI PARA OBTENER EL NOMBRE DEL SLUG DEL PRODUCTO

                    //var nombre_slug_marca = new ConexionGescli().ObtenerSlugProductoIndividual(DatosEnviados.Nombre);

                    var slug_marca = DatosEnviados.Nombre.Replace(" ", "-");
                    DatosEnviados.Slug_marca = slug_marca;

                    ent = new LNproductos().guardarCrearMarca(DatosEnviados);

                    //if (nombre_slug_marca.ValorConsulta == "1" && nombre_slug_marca.NombreSlug.Length > 3)
                    //{
                    //    DatosEnviados.Slug_categoria = nombre_slug_marca.NombreSlug;
                    //    ent = new LNproductos().guardarCrearCategoria(DatosEnviados);
                    //}
                    //else
                    //{
                    //    ent.MensajeConsulta = "ERROR : No se ha obtendido un valorConsulta o slug Valido " + nombre_slug_marca.MensajeConsulta;
                    //    ent.ValorConsulta = nombre_slug_marca.ValorConsulta;
                    //}

                }
                else
                {
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                }
            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
            }

            return Ok(ent);
        }

        [Authorize]
        [HttpPost("CambiarMensajeWsp")]
        public async Task<IActionResult> CambiarMensajewsp([FromBody] ENMensajeWsp DatosEnviados)
        {
            ENMensajeWsp ent = new ENMensajeWsp();
            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    
                    ent = new LNproductos().ModificarMensaje(DatosEnviados.MensajeProducto,DatosEnviados.MensajeGlobal);
                }
                else
                {
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                }
            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
            }

            return Ok(ent);
        }

        //Procesos de edicion
        [Authorize]
        [HttpPost("editarProducto")]
        public async Task<IActionResult> EditarProducto([FromForm] ENproductos DatosEnviados, IFormFile? ArchivoFicha)
        {
            ENproductos ent = new ENproductos();
            try
            {
                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    var idUsuarioJWT = User.FindFirst("id_usuario")?.Value;

                    if (string.IsNullOrEmpty(idUsuarioJWT))
                    {
                        ent.MensajeConsulta = "No se pudo obtener el ID de usuario del token.";
                        ent.ValorConsulta = "-101";
                        return Ok(ent);
                    }

                    DatosEnviados.Id_Usuario = idUsuarioJWT;

                    // ⬇️ OBTENER el Slug
                    var nombre_slug = new ConexionGescli().ObtenerSlugProductoIndividual(DatosEnviados.Nombre);

                    if (nombre_slug.ValorConsulta == "1" && nombre_slug.NombreSlug.Length > 3)
                    {
                        DatosEnviados.NombreSlug = nombre_slug.NombreSlug;

                        var eliminadasJson = Request.Form["ImagenesEliminadas"];
                        if (!string.IsNullOrEmpty(eliminadasJson))
                        {
                            DatosEnviados.ImagenesEliminadas = JsonSerializer.Deserialize<List<string>>(eliminadasJson);
                        }

                        var imagenesJson = Request.Form["Imagenes"];
                        if (!string.IsNullOrEmpty(imagenesJson))
                        {
                            DatosEnviados.Imagenes = JsonSerializer.Deserialize<List<ENImagenProducto>>(imagenesJson);
                        }

                        if (DatosEnviados.ImagenesEliminadas != null && DatosEnviados.ImagenesEliminadas.Any())
                        {
                            var rutaImagenes = Path.Combine(_env.ContentRootPath, "imagenes", "productos");

                            var imagenesEsperadas = DatosEnviados.Imagenes?
                                .Where(i => !string.IsNullOrEmpty(i.UrlImagen))
                                .Select(i => i.UrlImagen)
                                .ToList() ?? new List<string>();

                            // Mover a backup las eliminadas (realmente eliminadas)
                            foreach (var urlRelativa in DatosEnviados.ImagenesEliminadas)
                            {
                                if (!imagenesEsperadas.Contains(urlRelativa))
                                {
                                    new LNarchivos().GuardarArchivo(
                                        contenidoBase64: "", // solo eliminamos
                                        nombreSlug: nombre_slug.NombreSlug,
                                        indiceImagen: 0,
                                        rutawwwroot: rutaImagenes, 
                                        id_producto: DatosEnviados.Id_producto,
                                        id_usuario: DatosEnviados.Id_Usuario,
                                        tipo: "image/webp",
                                        imagenesEsperadas: imagenesEsperadas
                                    );
                                }
                            }
                        }

                        if (DatosEnviados.Imagenes != null)
                        {
                            var rutaImagenes = Path.Combine(_env.ContentRootPath, "imagenes", "productos");

                            int indice = 0;
                            foreach (var img in DatosEnviados.Imagenes)
                            {
                                if (!string.IsNullOrEmpty(img.ImagenBase64))
                                {
                                    var resultado = new LNarchivos().GuardarArchivo(
                                        contenidoBase64: img.ImagenBase64,
                                        nombreSlug: nombre_slug.NombreSlug,
                                        indiceImagen: indice,
                                        rutawwwroot: rutaImagenes,
                                        id_producto: DatosEnviados.Id_producto,
                                        id_usuario: DatosEnviados.Id_Usuario,
                                        tipo: "image/webp",
                                        imagenesEsperadas: DatosEnviados.Imagenes
                                            .Where(i => !string.IsNullOrEmpty(i.UrlImagen))
                                            .Select(i => i.UrlImagen)
                                            .ToList()
                                    );

                                    if (resultado.ValorConsulta == "1")
                                    {
                                        // Acá podrías registrar la nueva URL en la base de datos si lo necesitas
                                        // o actualizar el objeto en memoria
                                        img.UrlImagen = resultado.RutaArchivo;
                                    }

                                    indice++;
                                }
                            }
                        }

                        // ⬇️ 🚨 Si recibió un archivo, guardarlo
                        if (ArchivoFicha != null && ArchivoFicha.Length > 0)
                        {
                            // Leer archivo y convertirlo en Base64
                            using (var memoryStream = new MemoryStream())
                            {
                                await ArchivoFicha.CopyToAsync(memoryStream);
                                byte[] fileBytes = memoryStream.ToArray();
                                string base64File = Convert.ToBase64String(fileBytes);

                                // ¿Qué tipo de archivo es?
                                var extension = Path.GetExtension(ArchivoFicha.FileName).ToLower();

                                string tipoArchivo;

                                if (extension == ".pdf")
                                {
                                    tipoArchivo = "application/pdf";
                                }
                                else if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".webp")
                                {
                                    tipoArchivo = "image/webp";
                                }
                                else
                                {
                                    tipoArchivo = ArchivoFicha.ContentType;
                                }

                                var rutaBase = tipoArchivo.ToLower() == "application/pdf"
                                    ? Path.Combine(_env.ContentRootPath, "pdfs")
                                    : Path.Combine(_env.ContentRootPath, "imagenes", "productos");

                                var guardarArchivo = new LNarchivos().GuardarArchivo(
                                    base64File,
                                    nombre_slug.NombreSlug,
                                    0,
                                    rutaBase,
                                    DatosEnviados.Id_producto,
                                    DatosEnviados.Id_Usuario,
                                    tipoArchivo
                                );

                                if (guardarArchivo.ValorConsulta != "1")
                                {
                                    ent.MensajeConsulta = "Error al guardar archivo.";
                                    ent.ValorConsulta = "-200";
                                    return Ok(ent);
                                } 
                                else
                                {
                                    DatosEnviados.ficha = guardarArchivo.RutaArchivo;
                                }
                            }
                        }

                        ent = new LNproductos().EditarProducto(DatosEnviados);

                        if (DatosEnviados.Imagenes != null && DatosEnviados.Imagenes.Any())
                        {
                            foreach (var imagen in DatosEnviados.Imagenes)
                            {
                                if (!string.IsNullOrEmpty(imagen.UrlImagen))
                                {
                                    var resultado = new LNproductos().EditarImagenProducto(
                                         int.Parse(DatosEnviados.Id_producto),
                                        imagen.UrlImagen,
                                        imagen.EsPrincipal == true ? 1 : 0,
                                        imagen.EsPrincipal2 == true ? 1 : 0,
                                        int.Parse(DatosEnviados.Id_Usuario)
                                    );

                                    if (resultado.ValorConsulta != "1")
                                    {
                                        ent.MensajeConsulta = "Error al actualizar imagen: " + resultado.MensajeConsulta;
                                        ent.ValorConsulta = resultado.ValorConsulta;
                                        return Ok(ent);
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        ent.MensajeConsulta = "ERROR: No se ha obtenido un slug válido. " + nombre_slug.MensajeConsulta;
                        ent.ValorConsulta = nombre_slug.ValorConsulta;
                    }
                }
                else
                {
                    ent.MensajeConsulta = "Token incorrecto";
                    ent.ValorConsulta = "-100";
                }
            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "Entro al Catch -> " + ex.Message;
                ent.ValorConsulta = "-99";
            }

            return Ok(ent);
        }
    }
}
