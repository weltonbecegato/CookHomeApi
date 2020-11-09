using CookHome.Api.Servicos.Modelo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace CookHome.Api.Dominio
{
    public class ClienteBase
    {
        [Key]
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
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        [NotMapped]
        public int Tipo { get; set; }

        [NotMapped]
        public string NomeCompleto
        {
            get
            {
                return $"{Nome} {SobreNome}";
            }
        }

        [NotMapped]
        public string EnderecoCompleto
        {
            get
            {
                return $"{Endereco}, {Numero}, {Bairro}, {Cidade} - {Estado}";
            }
        }

        [NotMapped]
        public Coordenada Coordenada
        {
            get
            {
                return new Coordenada(Latitude, Longitude);
            }
        }

        [NotMapped]
        public double Distancia
        {
            get; set;
        }

        public void Update(ClienteBase cliente)
        {
            Nome = cliente.Nome;
            SobreNome = cliente.SobreNome;
            Email = cliente.Endereco;
            Telefone = cliente.Telefone;
            Senha = cliente.Senha;
            Documento = cliente.Documento;
            Endereco = cliente.Endereco;
            Numero = cliente.Numero;
            Complemento = cliente.Complemento;
            Bairro = cliente.Bairro;
            Cidade = cliente.Cidade;
            Estado = cliente.Estado;
            Cep = cliente.Cep;
            Latitude = cliente.Latitude;
            Longitude = cliente.Longitude;
        }

        public bool TemCoordenadas()
        {
            return !String.IsNullOrEmpty(Latitude) && !String.IsNullOrEmpty(Longitude);
        }

        public Coordenada ObterCoordenadas()
        {
            return new Coordenada(Latitude, Longitude);
        }
    }
}
