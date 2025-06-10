using CL_AD;
using CL_EN;
using CL_LN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp;

namespace SholiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CTarchivosController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public CTarchivosController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [Authorize]
        [HttpPost("subirArchivo")]
        public async Task<IActionResult> subirArchivo([FromForm] ENsubidaArchivos DatosEnviados)
        {
            ENarchivos ent  = new ENarchivos();
            try
            {
                var rutawwwroot = _env.ContentRootPath; //ruta de la carpeta wwwroot}
                var rutaBase = Path.Combine(rutawwwroot, "imagenes", "productos");
                Directory.CreateDirectory(rutaBase);


                if (DatosEnviados.Token == "TOKENSUPERSECRETO")
                {
                    var idUsuarioJWT = User.FindFirst("id_usuario")?.Value;

                    if (string.IsNullOrEmpty(idUsuarioJWT))
                    {
                        ent.MensajeConsulta = "No se pudo obtener el ID de usuario del token.";
                        ent.ValorConsulta = "-101";
                        return Ok(ent);
                    }

                    DatosEnviados.Id_usuario = idUsuarioJWT;

                    //conectarse a la base de datos y traer el slug del producto con su indice de imagen
                    var obtenerSlug = new LNarchivos().VERSION1_SP_recuperarSlugConIndiceProducto(DatosEnviados.Id_producto);
                    int indiceImagen = Convert.ToInt32(obtenerSlug.IndiceImagen);

                    foreach (var archivo in DatosEnviados.Imagenes)
                    {
                        if (archivo.Length > 0)
                        {
                            var base64String = await ConvertToBase64Async(archivo);

                            var guardarArchivo = new LNarchivos().GuardarArchivo(
                                base64String,
                                obtenerSlug.NombreSlug,
                                indiceImagen,
                                rutaBase,
                                DatosEnviados.Id_producto,
                                idUsuarioJWT,
                                archivo.ContentType
                            );

                            if (guardarArchivo.ValorConsulta != "1")
                            {
                                return Ok(new { valorConsulta = "2", mensajeConsulta = "Error al registrar imagen en base de datos." });
                            }

                            indiceImagen++;
                        }
                    }
                    ent.ValorConsulta = "1";
                    ent.MensajeConsulta = "Imágenes subidas correctamente.";
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

        private async Task<string> ConvertToBase64Async(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public class ENsubidaArchivos
        {
            [FromForm(Name = "token")]
            public string? Token { get; set; }

            [FromForm(Name = "id_producto")]
            public string? Id_producto { get; set; }

            public string? Id_usuario { get; set; }

            [FromForm(Name = "imagenes")]
            public List<IFormFile> Imagenes { get; set; } = new List<IFormFile>();

            public string? TipoArchivo { get; set; }
        }

    }
}
