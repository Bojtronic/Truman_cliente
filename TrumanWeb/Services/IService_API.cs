using TrumanWeb.Models;

namespace TrumanWeb.Services
{
    public interface IService_API
    {
        Task<List<Usuario>> lista();
        Task<Usuario> obtener(int id);
        Task<bool> guardar(Usuario objeto);
        Task<bool> editar(Usuario objeto);
        Task<bool> eliminar(int id);

    }
}
