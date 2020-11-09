using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Modelo
{
    public class EnderecoModelo
    {
        [JsonProperty("address")]
        public string Endereco { get; set; }

        [JsonProperty("state")]
        public string Estado { get; set; }

        [JsonProperty("city")]
        public string Cidade { get; set; }

        [JsonProperty("district")]
        public string Bairro { get; set; }

        [JsonProperty("code")]
        public string Cep { get; set; }
    }
}
