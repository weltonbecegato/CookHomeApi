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
        private readonly IEnderecoServico _googleMapsServico;


        public ClienteController(CookHomeContext context, IEnderecoServico googleMapsServico)
        {
            _context = context;
            _googleMapsServico = googleMapsServico;
        }

        [HttpGet("lista")]
        public ActionResult ObterTodosClientes()
        {
            var clientes = _context.Cliente.ToList();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public ActionResult ObterCliente(int id)
        {
            var cliente = _context.Cliente.Where(cliente => cliente.Id == id).FirstOrDefault();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> InserirCliente(Cliente cliente)
        {
            var consulta = _context.Cliente.Where(c => c.Email == cliente.Email).FirstOrDefault();
            if (consulta != null)
            {
                ModelState.AddModelError("email", "email já cadastrado");
                return BadRequest(ModelState);
            }

            var localizacao = await _googleMapsServico.ObterGeolocalizacao(cliente.EnderecoCompleto);
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

            cliente.Update(requisicao);
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
