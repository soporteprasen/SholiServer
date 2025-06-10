using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_EN
{
    public class ENproductos
    {
        public string? Token { get; set; }
        public string? Id_producto { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public Decimal? Precio { get; set; }
        public Decimal? Descuento { get; set; }
        public Decimal? Stock { get; set; }
        public string? NombreSlug { get; set; }
        public string? UrlImagen1 { get; set; }
        public string? UrlImagen2 { get; set; }
        public string? ficha { get; set; }
        public string? Id_unidad { get; set; }
        public string? Unidad { get; set; }
        public string? Id_moneda { get; set; }
        public byte[]? Imagen1 { get; set; }
        public string? Id_marca { get; set; }
        public string? Marca { get; set; }
        public string? Id_categoria { get; set; }
        public string? Categoria { get; set; }
        public string? Slug_Categoria { get; set; }
        public string? Id_Usuario { get; set; }
        public string? ArchivoFichaBase64 { get; set; }
        public List<ENImagenProducto>? Imagenes { get; set; } = new List<ENImagenProducto>();
        public List<string>? ImagenesEliminadas { get; set; } = new List<string>();
        public string? MensajeConsulta { get; set; }
        public string? ValorConsulta { get; set; }
    }

    public class ENcategorias
    {
        public string? Token { get; set; }
        public string? Id_categoria { get; set; }
        public string? Id_usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Slug_categoria { get; set; }
        public string? Imagen_categoria { get; set; }
        public string? MensajeConsulta { get; set; }
        public string? ValorConsulta { get; set; }
    }

    public class ENmarcas
    {
        public string? Token {get; set;}
        public string? Id_marcas { get; set; }
        public string? Id_usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Slug_marca { get; set; }
        public string? Imagen_marca { get; set; }
        public string? MensajeConsulta { get; set; }
        public string? ValorConsulta { get; set; }
    }

    public class ENunidadesMedida
    {
        public string? Token { get; set; }
        public string? Id_unidad { get; set; }
        public string? Nombre { get; set; }
        public string? Abreviatura { get; set; }
        public string? MensajeConsulta { get; set; }
        public string? ValorConsulta { get; set; }
    }

    public class ENobtenerSlugProducto
    {
        public string? Token { get; set; }
        public string? Id_producto { get; set; }
        public string? NombreSlug { get; set; }
        public string? MensajeConsulta { get; set; }
        public string? ValorConsulta { get; set; }
    }

    public class ENMensajeWsp
    {
        public string? Token { get; set; }
        public string? MensajeGlobal { get; set; }
        public string? MensajeProducto { get; set; }
        public string? MensajeConsulta { get; set; }
        public string? ValorConsulta { get; set; }
    }


}
