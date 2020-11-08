using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Dominio
{
    public class Cozinheiro
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public int CidadeId { get; set; }
        public string Documento { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public List<CozinheiroCulinaria> Culinarias { get; set; }
    }
}
