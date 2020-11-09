using CookHome.Api.Dominio;
using CookHome.Api.Modelo;
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
    public class AuthController : ControllerBase
    {
        private readonly CookHomeContext _context;

        public AuthController(CookHomeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Autenticar(AutenticacaoModelo autenticacao)
        {
            var cliente = _context.Cliente.Where(cliente => cliente.Email == autenticacao.Email).FirstOrDefault();
            if (cliente != null)
            {
                if (cliente.Senha == autenticacao.Senha)
                {
                    cliente.Tipo = 1;
                    return Ok(cliente);
                }
                else
                {
                    ModelState.AddModelError("Senha", "Senha não confere");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                var cozinheiro = _context.Cozinheiro.Where(cozinheiro => cozinheiro.Email == autenticacao.Email).FirstOrDefault();
                if(cozinheiro != null)
                {
                    if (cozinheiro.Senha == autenticacao.Senha)
                    {
                        cliente.Tipo = 2;
                        return Ok(cliente);
                    }
                    else
                    {
                        ModelState.AddModelError("Senha", "Senha não confere");
                        return BadRequest(ModelState);
                    }
                }
            }

            return NotFound();
        }
    }
}
