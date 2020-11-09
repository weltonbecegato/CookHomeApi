using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Modelo
{
    public class CozinheiroModelo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string Documento { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Linkedin { get; set; }
        public string Site { get; set; }
        public List<int> Culinarias { get; set; }
    }
}
