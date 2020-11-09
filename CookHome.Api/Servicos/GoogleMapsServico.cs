using CookHome.Api.Configuracao;
using CookHome.Api.Modelo;
using CookHome.Api.Servicos.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookHome.Api.Servicos
{
    public interface IEnderecoServico
    {
        Task<GeoLocalizacao> ObterGeolocalizacao(string cep);
        Task<EnderecoModelo> ObterEnderecoPorCep(string cep);
        Task<double> CalcularDistancia(Coordenada atual, Coordenada alvo);
    }

    public class GoogleMapsServico : IEnderecoServico
    {
        private HttpClient _httpClient;
        private GoogleMapsConfiguracao _googleConfig;

        public GoogleMapsServico(HttpClient httpClient, GoogleMapsConfiguracao googleConfig)
        {
            _httpClient = httpClient;
            _googleConfig = googleConfig;
        }

        public async Task<GeoLocalizacao> ObterGeolocalizacao(string cep)
        {
            cep = cep.Replace("-", "");

            var resposta = await _httpClient.GetAsync($"{_googleConfig.UrlBase}/maps/api/geocode/json?address={cep}&key={_googleConfig.Chave}");
            if (resposta.IsSuccessStatusCode)
            {
                var conteudo = await resposta.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<GeoLocalizacaoContainer>(conteudo);
                if (obj != null)
                    return obj.Resultados.FirstOrDefault()?.Geometria?.Localizacao;
            }

            return null;
        }

        public async Task<EnderecoModelo> ObterEnderecoPorCep(string cep)
        {
            cep = cep.Replace("-", "");
            var resposta = await _httpClient.GetAsync($"https://ws.apicep.com/cep.json?code={cep}");
            if (resposta.IsSuccessStatusCode)
            {
                var conteudo = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EnderecoModelo>(conteudo);
            }

            return null;
        }

        public async Task<double> CalcularDistancia(Coordenada origem, Coordenada destino)
        {
            var endpoint = $"{_googleConfig.UrlBase}/maps/api/directions/json?origin={origem.Latitude.ToString().Replace(",", ".")},{origem.Longitude.ToString().Replace(",", ".")}&destination={destino.Latitude.ToString().Replace(",", ".")},{destino.Longitude.ToString().Replace(",", ".")}&sensor=false&key={_googleConfig.Chave}";
            var resposta = await _httpClient.GetAsync(endpoint);
            if (resposta.IsSuccessStatusCode)
            {
                var conteudo = await resposta.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<RotaConteiner>(conteudo);
                if (obj != null)
                {
                    double? distancia = obj.Rotas.FirstOrDefault()?.Legs?.FirstOrDefault()?.Distance.Value;
                    if (distancia.HasValue)
                        return distancia.Value / 1000;
                }
            }

            return 0;
        }
    }
}
