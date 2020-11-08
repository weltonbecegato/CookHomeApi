using CookHome.Api.Dominio;
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
    public class CozinheiroController : ControllerBase
    {
        private readonly CookHomeContext _context;

        public CozinheiroController(CookHomeContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public ActionResult ObterTodosCozinheiros()
        {
            var cozinheiros = _context.Cozinheiro.ToList();
            return Ok(cozinheiros);
        }

        [HttpGet("{id}")]
        public ActionResult ObterCozinheiro(int id)
        {
            var cozinheiro = _context.Cozinheiro.Where(cozinheiro => cozinheiro.Id == id).FirstOrDefault();
            return Ok(cozinheiro);
        }

        [HttpPost]
        public ActionResult InserirCozinheiro(Cozinheiro cozinheiro)
        {
            _context.Cozinheiro.Add(cozinheiro);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public ActionResult AtualizarCozinheiro(Cozinheiro requisicao)
        {
            var cozinheiro = _context.Cozinheiro.Where(cozinheiro => cozinheiro.Id == requisicao.Id).FirstOrDefault();
            if (cozinheiro == null)
                return NotFound();

            cozinheiro.Nome = requisicao.Nome;
            cozinheiro.SobreNome = requisicao.SobreNome;
            cozinheiro.Email = requisicao.Email;
            cozinheiro.Telefone = requisicao.Telefone;
            cozinheiro.Senha = requisicao.Senha;
            cozinheiro.CidadeId = requisicao.CidadeId;
            cozinheiro.Documento = requisicao.Documento;
            cozinheiro.Latitude = requisicao.Latitude;
            cozinheiro.Longitude = requisicao.Longitude;


            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult ExcluirCozinheiro(int id)
        {
            var cozinheiro = _context.Cozinheiro.Where(cozinheiro => cozinheiro.Id == id).FirstOrDefault();
            if (cozinheiro == null)
                return NotFound();

            _context.Cozinheiro.Remove(cozinheiro);
            _context.SaveChanges();
            return Ok();
        }
    }
}