using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Modelo
{
    public class RotaConteiner
    {
        [JsonProperty("routes")]
        public List<Rota> Rotas { get; set; }
    }

    public class Rota
    {
        [JsonProperty("legs")]
        public List<Leg> Legs { get; set; }
    }

    public class Leg
    {
        [JsonProperty("distance")]
        public Distance Distance { get; set; }
    }

    public class Distance
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}
