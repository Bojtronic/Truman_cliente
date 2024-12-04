namespace TrumanAPI.Models
{
    public class Producto
    {
        public int Id { get; set; } // Identificador único
        public string Categoria { get; set; } = string.Empty; // Categoría del producto
        public string Nombre { get; set; } = string.Empty; // Nombre del producto
        public string Descripcion { get; set; } = string.Empty; // Descripción del producto
        public int Cantidad { get; set; } // Cantidad en inventario
    }
}
