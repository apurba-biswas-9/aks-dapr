using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApp
{
   public interface IServiceApi 
    {
        Task<List<WeatherForecast>> Gets();
    }
    public class ServiceApi : IServiceApi
    {
        private readonly HttpClient _httpClient;
        public ServiceApi(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<WeatherForecast>> Gets()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"WeatherForecast");
            var response = await _httpClient.SendAsync(request);    
            //Console.WriteLine("_httpClient.BaseAddress" + _httpClient.BaseAddress.ToString());
            //Console.WriteLine("_httpClient" + _httpClient.ToString());
            //Console.WriteLine("Request");
            //Console.WriteLine("response" + response.ToString());
            //Console.WriteLine("response.RequestMessage" + response.RequestMessage);
            //Console.WriteLine("response.Content" + response.Content);
            //Console.WriteLine("Content.ReadAsStringAsync" + await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();
        }
    }
}



