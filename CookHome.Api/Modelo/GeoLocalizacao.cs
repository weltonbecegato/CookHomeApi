using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Modelo
{
    public class GeoLocalizacaoContainer
    {
        [JsonProperty("results")]
        public List<GeoLocalizacaoResultado> Resultados { get; set; }
    }

    public class GeoLocalizacaoResultado
    {
        [JsonProperty("geometry")]
        public GeoLocalizacaoGeometriaResultado Geometria { get; set; }
    }

    public class GeoLocalizacaoGeometriaResultado
    {
        [JsonProperty("location")]
        public GeoLocalizacao Localizacao { get; set; }
    }

    public class GeoLocalizacao
    {
        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lng")]
        public string Longitude { get; set; }
    }
}
