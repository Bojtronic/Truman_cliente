namespace TrumanWeb.Models
{
    public class Producto
    {
        public int Id { get; set; } // idProducto
        public int? CategoriaId { get; set; } // idCategoria (nullable, ya que puede ser NULL)
        public required Categoria Categoria { get; set; } // Relación con la tabla Categorías
        public string Nombre { get; set; } = string.Empty; // nombreProducto
        public string Descripcion { get; set; } = string.Empty; // descripcionProducto
        public decimal Precio { get; set; } // precio
        public int Stock { get; set; } // stock
        public int AlertaStock { get; set; } = 10; // alertaStock
        public string? UrlImagen { get; set; } // urlImagen (opcional)
        public bool Activo { get; set; } = true; // activo
        public DateTime FechaRegistro { get; set; } = DateTime.Now; // fechaRegistro
    }
}
