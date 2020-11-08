using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Dominio
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        public int IdCliente { get; set; }

        public int IdCozinheiro { get; set; }

        public DateTime Data { get; set; }
    }
}
