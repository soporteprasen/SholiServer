using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_EN
{
    public class ENarchivos
    {
        public string? NombreSlug { get; set; }
        public string? tipo { get; set; }
        public string? IndiceImagen { get; set; }
        public string? MensajeConsulta { get; set; }
        public string? ValorConsulta { get; set; }
        public string? RutaArchivo { get; set; }
    }

    public class ENImagenProducto
    {
        public int? IdImagenProducto { get; set; }
        public string? UrlImagen { get; set; }
        public bool? EsPrincipal { get; set; }
        public bool? EsPrincipal2 { get; set; }
        public string? ImagenBase64 { get; set; }
    }
}
