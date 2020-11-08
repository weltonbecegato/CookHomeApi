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
    public class CidadeController : ControllerBase
    {
        private readonly CookHomeContext _context;

        public CidadeController(CookHomeContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public ActionResult ObterTodasCidades()
        {
            var cidades = _context.Cidade.ToList();
            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public ActionResult ObterCidade(int id)
        {
            var Cidade = _context.Cidade.Where(cidade => cidade.Id == id).FirstOrDefault();
            return Ok(Cidade);
        }

    }
}