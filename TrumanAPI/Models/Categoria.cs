namespace TrumanAPI.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; } // idCategoria
        public string Descripcion { get; set; } = string.Empty; // descripcion
        public bool Activo { get; set; } = true; // activo
        public DateTime FechaRegistro { get; set; } = DateTime.Now; // fechaRegistro
    }
}
