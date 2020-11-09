using CookHome.Api.Dominio;
using CookHome.Api.Modelo;
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
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoServico _enderecoServico;

        public EnderecoController(IEnderecoServico enderecoServico)
        {
            _enderecoServico = enderecoServico;
        }

        [HttpGet]
        public async Task<ActionResult> ObterEndereco(string cep)
        {
            var endereco = await _enderecoServico.ObterEnderecoPorCep(cep);
            if (endereco == null)
                return NotFound();

            return Ok(endereco);
        }
    }
}
