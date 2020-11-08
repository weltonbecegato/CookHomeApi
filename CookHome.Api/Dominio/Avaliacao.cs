using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Dominio
{
    public class Avaliacao
    {
        [Key]
        public int Id { get; set; }

        public int IdAgendamento { get; set; }

        public int Nota { get; set; }

        public DateTime Data { get; set; }

        public string Observacao { get; set; }
    }
}
