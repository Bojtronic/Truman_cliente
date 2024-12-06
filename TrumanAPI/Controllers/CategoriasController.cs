using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TrumanAPI.Models;

namespace TrumanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CategoriasController(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnectionString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        // GET: api/Categorias
        [HttpGet]
        public IActionResult GetAllCategorias()
        {
            using (var context = new SqlConnection(GetConnectionString()))
            {
                var categorias = context.Query<Categoria>(@"
                    SELECT idCategoria AS Id, descripcion AS Descripcion, activo AS Activo, fechaRegistro AS FechaRegistro
                    FROM Categorias");
                return Ok(categorias);
            }
        }

        // GET: api/Categorias/{id}
        [HttpGet("{id}")]
        public IActionResult GetCategoriaById(int id)
        {
            using (var context = new SqlConnection(GetConnectionString()))
            {
                var categoria = context.QueryFirstOrDefault<Categoria>(@"
                    SELECT idCategoria AS Id, descripcion AS Descripcion, activo AS Activo, fechaRegistro AS FechaRegistro
                    FROM Categorias
                    WHERE idCategoria = @Id", new { Id = id });

                if (categoria == null)
                    return NotFound(new { Mensaje = "Categoría no encontrada" });

                return Ok(categoria);
            }
        }

        // POST: api/Categorias
        [HttpPost]
        public IActionResult CreateCategoria([FromBody] Categoria model)
        {
            if (model == null)
                return BadRequest(new { Mensaje = "Modelo de categoría inválido" });

            using (var context = new SqlConnection(GetConnectionString()))
            {
                var sql = @"
                    INSERT INTO Categorias (descripcion, activo)
                    VALUES (@Descripcion, @Activo);
                    SELECT CAST(SCOPE_IDENTITY() as INT);";

                var id = context.ExecuteScalar<int>(sql, new
                {
                    model.Descripcion,
                    model.Activo
                });

                model.Id = id;
                return CreatedAtAction(nameof(GetCategoriaById), new { id }, model);
            }
        }

        // PUT: api/Categorias
        [HttpPut]
        public IActionResult UpdateCategoria([FromBody] Categoria model)
        {
            if (model == null)
                return BadRequest(new { Mensaje = "Modelo de categoría inválido" });

            using (var context = new SqlConnection(GetConnectionString()))
            {
                // Verificar si la categoría existe
                var existingCategoria = context.QueryFirstOrDefault<Categoria>(@"
                    SELECT * FROM Categorias WHERE idCategoria = @Id", new { Id = model.Id });

                if (existingCategoria == null)
                    return NotFound(new { Mensaje = "Categoría no encontrada" });

                // Actualizar la categoría
                var sql = @"
                    UPDATE Categorias 
                    SET descripcion = @Descripcion, activo = @Activo 
                    WHERE idCategoria = @Id";

                var rowsAffected = context.Execute(sql, new
                {
                    Id = model.Id,
                    model.Descripcion,
                    model.Activo
                });

                if (rowsAffected > 0)
                    return NoContent(); // Actualización exitosa
                else
                    return BadRequest(new { Mensaje = "Error al actualizar la categoría" });
            }
        }

        // DELETE: api/Categorias/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria(int id)
        {
            using (var context = new SqlConnection(GetConnectionString()))
            {
                var sql = "DELETE FROM Categorias WHERE idCategoria = @Id";
                var rowsAffected = context.Execute(sql, new { Id = id });

                if (rowsAffected > 0)
                    return NoContent();
                else
                    return NotFound(new { Mensaje = "Categoría no encontrada" });
            }
        }
    }
}
