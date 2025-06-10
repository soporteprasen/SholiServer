using CL_AD;
using CL_EN;
using SixLabors.ImageSharp.Formats.Webp;

namespace CL_LN
{
    public class LNarchivos
    {
        public ENarchivos VERSION1_SP_recuperarSlugConIndiceProducto(string id_producto)
        {
            var res = new ADarchivos().VERSION1_SP_recuperarSlugConIndiceProducto(id_producto);
            return res;
        }



        public ENarchivos GuardarArchivo(string contenidoBase64, string nombreSlug, int indiceImagen, string rutawwwroot, string id_producto, string id_usuario, string tipo, List<string> imagenesEsperadas = null)
        {
            ENarchivos ent = new ENarchivos();
            try
            {
                string nombreArchivo = "";
                string rutaFisica = "";
                string rutaBaseImagenes = "";

                var datosProducto = new ADproductos(rutawwwroot, rutaBaseImagenes).VERSION1_SP_ListarProductoxID(int.Parse(id_producto));

                var imagenesActuales = datosProducto.Imagenes?
                .Select(img => img.UrlImagen)
                .Where(url => !string.IsNullOrEmpty(url))
                .ToList() ?? new List<string>();

                if (!string.IsNullOrEmpty(datosProducto.ficha))
                {
                    var rutaFisicaVieja = Path.Combine(rutawwwroot, Path.GetFileName(datosProducto.ficha));

                    if (File.Exists(rutaFisicaVieja))
                    {
                        var carpetaPDFs = Path.GetDirectoryName(rutaFisicaVieja);
                        var carpetaRaiz = Directory.GetParent(carpetaPDFs).FullName;

                        string rutaRespaldo = Path.Combine(carpetaRaiz, "Backup", "pdfs_respaldo");

                        Directory.CreateDirectory(rutaRespaldo);

                        string nombreRespaldo = Path.GetFileNameWithoutExtension(rutaFisicaVieja) + "_" +
                                                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";

                        string rutaArchivoRespaldo = Path.Combine(rutaRespaldo, nombreRespaldo);

                        File.Move(rutaFisicaVieja, rutaArchivoRespaldo);
                    }
                }

                if (tipo == "application/pdf")
                {
                    var random = new Random();
                    char letraAleatoria = (char)random.Next('A', 'Z' + 1);
                    string numeroAleatorio = random.Next(0, 100).ToString("D2");

                    nombreArchivo = nombreSlug + "_" + letraAleatoria + numeroAleatorio + ".pdf";
                    rutaFisica = Path.Combine(rutawwwroot, nombreArchivo);

                    Directory.CreateDirectory(Path.GetDirectoryName(rutaFisica));

                    byte[] bytes = Convert.FromBase64String(contenidoBase64);
                    File.WriteAllBytes(rutaFisica, bytes);

                    if (File.Exists(rutaFisica))
                    {
                        ent.ValorConsulta = "1";
                        ent.RutaArchivo = "/pdfs/" + nombreArchivo;
                    }
                    else
                    {
                        ent.ValorConsulta = "3";
                    }
                }
                else if (tipo.StartsWith("image/") && imagenesEsperadas != null && imagenesEsperadas.Count > 0)
                {
                    var imagenesEliminadas = imagenesActuales
                        .Where(img => !imagenesEsperadas.Contains(img, StringComparer.OrdinalIgnoreCase))
                        .ToList();

                    foreach (var rutaRelativaEliminada in imagenesEliminadas)
                    {
                        var rutaFisicaImagen = Path.Combine(rutawwwroot, Path.GetFileName(rutaRelativaEliminada));
                        var carpetaRaiz = Directory.GetParent(Directory.GetParent(rutawwwroot).FullName).FullName;
                        var rutaBackup = Path.Combine(carpetaRaiz, "Backup", "imagenes_respaldo", "productos");

                        Directory.CreateDirectory(rutaBackup);

                        var nombreRespaldo = Path.GetFileNameWithoutExtension(rutaFisicaImagen) + "_" +
                                             DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                                             Path.GetExtension(rutaFisicaImagen);

                        var rutaDestino = Path.Combine(rutaBackup, nombreRespaldo);

                        if (File.Exists(rutaFisicaImagen))
                        {
                            File.Move(rutaFisicaImagen, rutaDestino);
                            var resultadoEliminacionBD = new ADarchivos().VERSION1_SP_EliminarImagenPorProducto(id_producto, rutaRelativaEliminada);

                        }
                    }

                    var nuevasImagenes = imagenesEsperadas
                        .Where(nueva => !imagenesActuales.Contains(nueva, StringComparer.OrdinalIgnoreCase))
                        .ToList();

                    int contador = 1;
                    foreach (var nuevaBase64 in nuevasImagenes)
                    {
                        if (!string.IsNullOrEmpty(contenidoBase64))
                        {
                            var bytes = Convert.FromBase64String(contenidoBase64);
                            var nombreArchivoImg = $"{nombreSlug}_{contador}_{DateTime.Now:yyyyMMdd_HHmmss}.webp";
                            var rutaImg = Path.Combine(rutawwwroot, nombreArchivoImg);

                            Directory.CreateDirectory(Path.GetDirectoryName(rutaImg));
                            File.WriteAllBytes(rutaImg, bytes);

                            contador++;

                            if (File.Exists(rutaImg))
                            {
                                ent.ValorConsulta = "1";
                                ent.RutaArchivo = "/imagenes/productos/" + nombreArchivoImg;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ent.ValorConsulta = "-99";
                ent.MensajeConsulta = "ERROR: " + ex.Message;
            }

            return ent;
        }





    }
}
