using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TrumanAPI.Models;

namespace TrumanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _config;

        public UsuarioController(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnectionString()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        // GET: api/Usuario
        [HttpGet]
        public IActionResult GetAllUsuarios()
        {
            using (var context = new SqlConnection(GetConnectionString()))
            {
                var usuarios = context.Query<Usuario>("SELECT * FROM Usuarios");
                return Ok(usuarios);
            }
        }

        // GET: api/Usuario/{id}
        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            using (var context = new SqlConnection(GetConnectionString()))
            {
                var usuario = context.QueryFirstOrDefault<Usuario>(
                    "SELECT * FROM Usuarios WHERE id = @Id", new { Id = id });

                if (usuario == null)
                    return NotFound(new { Mensaje = "Usuario no encontrado" });

                return Ok(usuario);
            }
        }

        // POST: api/Usuario
        [HttpPost]
        public IActionResult CreateUsuario([FromBody] Usuario model)
        {
            if (model == null)
                return BadRequest(new { Mensaje = "Modelo de usuario inválido" });

            using (var context = new SqlConnection(GetConnectionString()))
            {
                var sql = @"
                    INSERT INTO Usuarios (nombre, contrasena) 
                    VALUES (@Nombre, @Contrasena);
                    SELECT CAST(SCOPE_IDENTITY() as INT);";

                var id = context.ExecuteScalar<int>(sql, new
                {
                    model.Nombre,
                    model.Contrasena
                });

                model.Id = id;
                return CreatedAtAction(nameof(GetUsuarioById), new { id }, model);
            }
        }

        // PUT: api/Usuario/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] Usuario model)
        {
            if (model == null)
                return BadRequest(new { Mensaje = "Modelo de usuario inválido" });

            using (var context = new SqlConnection(GetConnectionString()))
            {
                var sql = @"
                    UPDATE Usuarios 
                    SET nombre = @Nombre, contrasena = @Contrasena 
                    WHERE id = @Id";

                var rowsAffected = context.Execute(sql, new
                {
                    Id = id,
                    model.Nombre,
                    model.Contrasena
                });

                if (rowsAffected > 0)
                    return NoContent();
                else
                    return NotFound(new { Mensaje = "Usuario no encontrado" });
            }
        }

        // DELETE: api/Usuario/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            using (var context = new SqlConnection(GetConnectionString()))
            {
                var sql = "DELETE FROM Usuarios WHERE id = @Id";
                var rowsAffected = context.Execute(sql, new { Id = id });

                if (rowsAffected > 0)
                    return NoContent();
                else
                    return NotFound(new { Mensaje = "Usuario no encontrado" });
            }
        }
    }
}
