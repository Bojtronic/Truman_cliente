using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TrumanWeb.Models;

namespace TrumanWeb.Services
{
    public class Service_API : IService_API
    {
        private static string _apiurl; 

        public Service_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            //_apiurl = builder.GetSection("APIsettings.APIURL").Value;
            _apiurl = "https://localhost:7186/api/";
        }

        public Task<bool> editar(Usuario objeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> guardar(Usuario objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Usuario>> lista()
        {
            List<Usuario> lista = new List<Usuario>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_apiurl);

            var response = await cliente.GetAsync("usuario");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Usuario>>(json_respuesta);
                lista = resultado;
            }
            return lista;
            
        }

        public Task<Usuario> obtener(int id)
        {

            throw new NotImplementedException();

        }


        /*
        public async Task obtener()
        {
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_apiurl);


            var content = new StringContent(JsonConvert.SerializeObject(x), Encoding.UTF8, "application/json");


            var response = await cliente.PostAsync("usuario", content);
            var json_respuesta = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<Usuario>(json_respuesta);

        }
        */
    }
}
