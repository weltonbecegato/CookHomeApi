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
    public class AgendamentoController : ControllerBase
    {
        private readonly CookHomeContext _context;

        public AgendamentoController(CookHomeContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public ActionResult ObterTodosAgendamentos()
        {
            var agendamentos = _context.Agendamento.ToList();
            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        public ActionResult ObterAgendamento(int id)
        {
            var Agendamento = _context.Agendamento.Where(agendamento => agendamento.Id == id).FirstOrDefault();
            return Ok(Agendamento);
        }


        [HttpGet("cliente/{clienteId}")]
        public ActionResult ObterAgendamentoPorCliente(int clienteId)
        {
            var Agendamento = _context.Agendamento.Where(agendamento => agendamento.IdCliente == clienteId && agendamento.Data > DateTime.Now).FirstOrDefault();
            return Ok(Agendamento);
        }


        [HttpPost]
        public ActionResult InserirAgendamento(Agendamento agendamento)
        {
            _context.Agendamento.Add(agendamento);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public ActionResult AtualizarAgendamento(Agendamento requisicao)
        {
            var agendamento = _context.Agendamento.Where(agendamento => agendamento.Id == requisicao.Id).FirstOrDefault();
            if (agendamento == null)
                return NotFound();

            agendamento.IdCliente = requisicao.IdCliente;
            agendamento.IdCozinheiro = requisicao.IdCozinheiro;
            agendamento.Data = requisicao.Data;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult ExcluirAgendamento(int id)
        {
            var agendamento = _context.Agendamento.Where(agendamento => agendamento.Id == id).FirstOrDefault();
            if (agendamento == null)
                return NotFound();

            _context.Agendamento.Remove(agendamento);
            _context.SaveChanges();
            return Ok();
        }
    }
}