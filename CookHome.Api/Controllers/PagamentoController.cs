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
    public class PagamentoController : ControllerBase
    {
        private readonly CookHomeContext _context;

        public PagamentoController(CookHomeContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public ActionResult ObterTodosPagamentos()
        {
            var pagamentos = _context.Pagamento.ToList();
            return Ok(pagamentos);
        }

        [HttpGet("{id}")]
        public ActionResult ObterPagamento(int id)
        {
            var Pagamento = _context.Pagamento.Where(pagamento => pagamento.Id == id).FirstOrDefault();
            return Ok(Pagamento);
        }

        [HttpPost]
        public ActionResult InserirPagamento(Pagamento pagamento)
        {
            _context.Pagamento.Add(pagamento);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public ActionResult AtualizarPagamento(Pagamento requisicao)
        {
            var pagamento = _context.Pagamento.Where(pagamento => pagamento.Id == requisicao.Id).FirstOrDefault();
            if (pagamento == null)
                return NotFound();

            pagamento.IdCliente = requisicao.IdCliente;
            pagamento.NumeroCartao = requisicao.NumeroCartao;
            pagamento.Vencimento = requisicao.Vencimento;
            pagamento.CVC = requisicao.CVC;
            pagamento.NomeCartao = requisicao.NomeCartao;
            pagamento.DocumentoCartao = requisicao.DocumentoCartao;
  


            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult ExcluirPagamento(int id)
        {
            var pagamento = _context.Pagamento.Where(pagamento => pagamento.Id == id).FirstOrDefault();
            if (pagamento == null)
                return NotFound();

            _context.Pagamento.Remove(pagamento);
            _context.SaveChanges();
            return Ok();
        }
    }
}