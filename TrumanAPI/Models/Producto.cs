namespace TrumanAPI.Models
{
    public class Producto
    {
        public int IdProducto { get; set; } // idProducto
        public int? IdCategoria { get; set; } // idCategoria, nullable

        public string NombreProducto { get; set; } = string.Empty; // nombreProducto
        public string? DescripcionProducto { get; set; } // descripcionProducto, nullable
        public decimal Precio { get; set; } // precio
        public int Stock { get; set; } // stock
        public int? AlertaStock { get; set; } = 10; // alertaStock, nullable
        public string? UrlImagen { get; set; } // urlImagen, nullable
        public bool Activo { get; set; } = true; // activo
        public DateTime FechaRegistro { get; set; } = DateTime.Now; // fechaRegistro
    }
}
