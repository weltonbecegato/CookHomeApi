using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Servicos.Modelo
{
    public class Coordenada
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Coordenada(string latitude, string longitude)
        {
            Latitude = double.Parse(latitude, CultureInfo.InvariantCulture);
            Longitude = double.Parse(longitude, CultureInfo.InvariantCulture);
        }
    }
}
