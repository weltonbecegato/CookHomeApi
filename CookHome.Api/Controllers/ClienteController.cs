using CookHome.Api.Configuracao;
using CookHome.Api.Dominio;
using CookHome.Api.Servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookHome.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly CookHomeContext _context;
        private readonly IGoogleMapsServico _googleMapsServico;


        public ClienteController(CookHomeContext context, IGoogleMapsServico googleMapsServico)
        {
            _context = context;
            _googleMapsServico = googleMapsServico;
        }

        [HttpGet("lista")]
        public ActionResult ObterTodosClientes()
        {
            var clientes = _context.Cliente.Include(c => c.Cidade).ToList();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public ActionResult ObterCliente(int id)
        {
            var cliente = _context.Cliente.Include(c => c.Cidade).Where(cliente => cliente.Id == id).FirstOrDefault();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> InserirCliente(Cliente cliente)
        {
            var localizacao = await _googleMapsServico.ObterGeolocalizacao(cliente.Cep);
            if (localizacao != null)
            {
                cliente.Latitude = localizacao.Latitude;
                cliente.Longitude = localizacao.Longitude;
            }

            _context.Cliente.Add(cliente);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public ActionResult AtualizarCliente(Cliente requisicao)
        {
            var cliente = _context.Cliente.Where(cliente => cliente.Id == requisicao.Id).FirstOrDefault();
            if (cliente == null)
                return NotFound();

            cliente.Nome = requisicao.Nome;
            cliente.SobreNome = requisicao.SobreNome;
            cliente.Email = requisicao.Email;
            cliente.Telefone = requisicao.Telefone;
            cliente.Senha = requisicao.Senha;
            cliente.CidadeId = requisicao.CidadeId;
            cliente.Documento = requisicao.Documento;
            cliente.Latitude = requisicao.Latitude;
            cliente.Longitude = requisicao.Longitude;


            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult ExcluirCliente(int id)
        {
            var cliente = _context.Cliente.Where(cliente => cliente.Id == id).FirstOrDefault();
            if (cliente == null)
                return NotFound();

            _context.Cliente.Remove(cliente);
            _context.SaveChanges();
            return Ok();
        }
    }
}
