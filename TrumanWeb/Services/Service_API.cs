using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrumanWeb.Services
{
    public class Service_API<T> : IService_API<T> where T : class
    {
        private static readonly string _apiUrl;

        static Service_API()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Configura la URL base de la API desde appsettings.json
            _apiUrl = "https://localhost:7186/api/";
        }

        public async Task<List<T>> Lista(string endpoint)
        {
            List<T> lista = new List<T>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_apiUrl);
                var response = await cliente.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var jsonRespuesta = await response.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<T>>(jsonRespuesta);
                }
            }
            return lista;
        }

        public async Task<T> Obtener(int id, string endpoint)
        {
            T objeto = null;

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_apiUrl);
                var response = await cliente.GetAsync($"{endpoint}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonRespuesta = await response.Content.ReadAsStringAsync();
                    objeto = JsonConvert.DeserializeObject<T>(jsonRespuesta);
                }
            }
            return objeto;
        }

        public async Task<bool> Guardar(T objeto, string endpoint)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_apiUrl);

                var content = new StringContent(
                    JsonConvert.SerializeObject(objeto),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await cliente.PostAsync(endpoint, content);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> Editar(T objeto, string endpoint)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_apiUrl);

                var content = new StringContent(
                    JsonConvert.SerializeObject(objeto),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await cliente.PutAsync(endpoint, content);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> Eliminar(int id, string endpoint)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_apiUrl);
                var response = await cliente.DeleteAsync($"{endpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
        }
    }
}
