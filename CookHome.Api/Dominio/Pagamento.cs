using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Dominio
{
    public class Pagamento
    {
        [Key]
        public int Id { get; set; }

        public int IdCliente { get; set; }
        public string NumeroCartao { get; set; }
        public string Vencimento { get; set; }
        public string CVC { get; set; }
        public string NomeCartao { get; set; }
        public string DocumentoCartao { get; set; }
    }
}
