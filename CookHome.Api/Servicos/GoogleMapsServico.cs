using CookHome.Api.Configuracao;
using CookHome.Api.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookHome.Api.Servicos
{
    public interface IGoogleMapsServico
    {
        Task<GeoLocalizacao> ObterGeolocalizacao(string cep);
    }

    public class GoogleMapsServico : IGoogleMapsServico
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
    }
}
