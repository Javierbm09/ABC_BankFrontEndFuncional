using ABC_BankApi.Model;
using ABC_BankApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace ABC_BankApi.Servicios
{
    public class Servicio_Api : IServicio_Api
    {
        private static string _baseurl;


        public Servicio_Api()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;

        }       


        public async Task<List<Contacto>> Lista()
        {
            List<Contacto> lista = new List<Contacto>();            

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync("api/Contacto");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Contacto>>(json_respuesta);
                lista = resultado;
            }

            return lista;
        }

        public async Task<Contacto> Obtener(int id)
        {
            Contacto objeto = new Contacto();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var response = await cliente.GetAsync($"api/Contacto/id:int?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Contacto>(json_respuesta);
                objeto = resultado;
            }

            return objeto;
        }


        public async Task<bool> Guardar(Contacto objeto)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8,"application/json");
             
            var response = await cliente.PostAsync("api/Contacto", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }


        public async Task<bool> Editar(Contacto objeto)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Contacto/{objeto.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var response = await cliente.DeleteAsync($"/api/Contacto/{id}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }  
    }
}
