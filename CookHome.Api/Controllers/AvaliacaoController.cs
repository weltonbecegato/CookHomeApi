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
    public class AvaliacaoController : ControllerBase
    {
        private readonly CookHomeContext _context;

        public AvaliacaoController(CookHomeContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public ActionResult ObterTodosAvaliacao()
        {
            var avaliacao = _context.Avaliacao.ToList();
            return Ok(avaliacao);
        }

        [HttpGet("{id}")]
        public ActionResult ObterAvaliacao(int id)
        {
            var Avaliacao = _context.Avaliacao.Where(avaliacao => avaliacao.Id == id).FirstOrDefault();
            return Ok(Avaliacao);
        }

        [HttpPost]
        public ActionResult InserirAvaliacao(Avaliacao avaliacao)
        {
            _context.Avaliacao.Add(avaliacao);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public ActionResult AtualizarAvaliacao(Avaliacao requisicao)
        {
            var avaliacao = _context.Avaliacao.Where(avaliacao => avaliacao.Id == requisicao.Id).FirstOrDefault();
            if (avaliacao == null)
                return NotFound();

            avaliacao.IdAgendamento = requisicao.IdAgendamento;
            avaliacao.Nota = requisicao.Nota;
            avaliacao.Data = requisicao.Data;
            avaliacao.Observacao = requisicao.Observacao;
  
  


            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult ExcluirAvaliacao(int id)
        {
            var avaliacao = _context.Avaliacao.Where(avaliacao => avaliacao.Id == id).FirstOrDefault();
            if (avaliacao == null)
                return NotFound();

            _context.Avaliacao.Remove(avaliacao);
            _context.SaveChanges();
            return Ok();
        }
    }
}