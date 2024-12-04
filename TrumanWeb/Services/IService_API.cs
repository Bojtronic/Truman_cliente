
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrumanWeb.Services
{
    public interface IService_API<T> where T : class
    {
        Task<List<T>> Lista(string endpoint);
        Task<T> Obtener(int id, string endpoint);
        Task<bool> Guardar(T objeto, string endpoint);
        Task<bool> Editar(T objeto, string endpoint);
        Task<bool> Eliminar(int id  , string endpoint);
    }
}



/*
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
*/