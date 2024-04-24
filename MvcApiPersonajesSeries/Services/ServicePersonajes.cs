using MvcApiPersonajesSeries.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiPersonajesSeries.Services
{
    public class ServicePersonajes
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue Header;
        public ServicePersonajes(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
            this.Header = new
               MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/personajes";
            return await this.CallApiAsync<List<Personaje>>(request);   
        }

        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/Personajes/series";
            return await this.CallApiAsync<List<string>>(request);
        }

        public async Task<List<Personaje>>  GetPersonajesBySerieAsync(string serie)
        {
            string request = "api/Personajes/GetPersonajesBySerie/" + serie;
            return await this.CallApiAsync<List<Personaje>>(request);
        }
        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            string request = "api/Personajes/FindPersonaje/" + id;
            return await this.CallApiAsync<Personaje>(request); 
        }

        public async Task InsertPersonaje(Personaje personaje)
        {
            string request = "api/Personajes/InsertPersonaje";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonaje(Personaje personaje)
        {
            string request = "api/Personajes/UpdatePersonaje";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonaje(int id)
        {
            string request = "api/Personajes/DeletePersonaje/" + id;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }
    }
}
