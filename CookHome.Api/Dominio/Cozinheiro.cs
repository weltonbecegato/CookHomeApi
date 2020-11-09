using CookHome.Api.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Dominio
{
    public class Cozinheiro : ClienteBase
    {
        public string Linkedin { get; set; }
        public string Site { get; set; }
        public List<CozinheiroCulinaria> Culinarias { get; set; }

        public Cozinheiro()
        {

        }

        public Cozinheiro(CozinheiroModelo modelo)
        {
            Nome = modelo.Nome;
            SobreNome = modelo.SobreNome;
            Email = modelo.Email;
            Telefone = modelo.Telefone;
            Senha = modelo.Senha;
            Documento = modelo.Documento;
            Endereco = modelo.Endereco;
            Numero = modelo.Numero;
            Complemento = modelo.Complemento;
            Bairro = modelo.Bairro;
            Cidade = modelo.Cidade;
            Estado = modelo.Estado;
            Cep = modelo.Cep;
            Linkedin = modelo.Linkedin;
            Site = modelo.Site;
            Culinarias = modelo.Culinarias.Select(item => new CozinheiroCulinaria
            {
                CozinheiroId = this.Id,
                TipoCulinariaId = item
            }).ToList();
        }
    }
}
