using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Dominio
{
    public class CozinheiroCulinaria
    {
        [Key]
        public int Id { get; set; }
        public int CozinheiroId { get; set; }
        public int TipoCulinariaId { get; set; }
    }
}
