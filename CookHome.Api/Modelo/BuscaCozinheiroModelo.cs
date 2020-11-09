using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Modelo
{
    public class BuscaCozinheiroModelo
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public List<int> TipoCulinarias { get; set; }
        public int Distancia { get; set; }
    }
}
