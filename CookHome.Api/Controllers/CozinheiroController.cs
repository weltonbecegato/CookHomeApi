using CookHome.Api.Dominio;
using CookHome.Api.Modelo;
using CookHome.Api.Servicos;
using CookHome.Api.Servicos.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace CookHome.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CozinheiroController : ControllerBase
    {
        private readonly CookHomeContext _context;
        private readonly IEnderecoServico _enderecoServico;

        public CozinheiroController(CookHomeContext context, IEnderecoServico enderecoServico)
        {
            _context = context;
            _enderecoServico = enderecoServico;
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
        public async Task<ActionResult> InserirCozinheiro(CozinheiroModelo requisicao)
        {
            var consulta = _context.Cozinheiro.Where(c => c.Email == requisicao.Email).FirstOrDefault();
            if (consulta != null)
            {
                ModelState.AddModelError("email", "email já cadastrado");
                return BadRequest(ModelState);
            }

            var cozinheiro = new Cozinheiro(requisicao);
            var localizacao = await _enderecoServico.ObterGeolocalizacao(cozinheiro.EnderecoCompleto);
            if (localizacao != null)
            {
                cozinheiro.Latitude = localizacao.Latitude;
                cozinheiro.Longitude = localizacao.Longitude;
            }

            _context.Cozinheiro.Add(cozinheiro);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("buscaCozinheiro")]
        public async Task<ActionResult> BuscarCozinheiros(BuscaCozinheiroModelo pesquisa)
        {
            var cliente = await _context.Cliente.Where(c => c.Id == pesquisa.IdCliente).FirstOrDefaultAsync();
            var cozinheiros = _context.Cozinheiro.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa.Nome))
                cozinheiros = cozinheiros.Where(c => c.Nome.Contains(pesquisa.Nome) || c.SobreNome.Contains(pesquisa.Nome));

            if (pesquisa.TipoCulinarias != null && pesquisa.TipoCulinarias.Count > 0)
            {
                var listaCozinheiro = await _context.CozinheiroCulinaria.Where(c => pesquisa.TipoCulinarias.Contains(c.TipoCulinariaId)).ToListAsync();
                var ids = listaCozinheiro.Select(s => s.CozinheiroId);

                cozinheiros = cozinheiros.Where(c => ids.Contains(c.Id));
            }

            var lista = await cozinheiros.ToListAsync();
            var resultado = new List<Cozinheiro>();
            if (pesquisa.Distancia > 0 && cliente.TemCoordenadas())
            {
                foreach (var cozinheiro in lista)
                {
                    if (cozinheiro.TemCoordenadas())
                    {
                        var distancia = await _enderecoServico.CalcularDistancia(cliente.ObterCoordenadas(), cozinheiro.ObterCoordenadas());
                        if (distancia < pesquisa.Distancia)
                        {
                            cozinheiro.Distancia = distancia;
                            resultado.Add(cozinheiro);
                        }
                    }
                }
            }
            else
            {
                resultado = lista;
            }
            

            return Ok(resultado.OrderBy(r => r.Distancia).ToList());
        }

        [HttpPut]
        public ActionResult AtualizarCozinheiro(Cozinheiro requisicao)
        {
            var consulta = _context.Cozinheiro.Where(c => c.Email == requisicao.Email).FirstOrDefault();
            if (consulta != null)
            {
                ModelState.AddModelError("email", "email já cadastrado");
                return BadRequest(ModelState);
            }

            var cozinheiro = _context.Cozinheiro.Where(cozinheiro => cozinheiro.Id == requisicao.Id).FirstOrDefault();
            if (cozinheiro == null)
                return NotFound();

            cozinheiro.Update(requisicao);
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