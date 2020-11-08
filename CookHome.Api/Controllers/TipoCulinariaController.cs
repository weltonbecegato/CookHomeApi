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
    public class TipoCulinariaController : ControllerBase
    {
        private readonly CookHomeContext _context;

        public TipoCulinariaController(CookHomeContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public ActionResult ObterTodosTiposCulinarias()
        {
            var TiposCulinarias = _context.TipoCulinaria.ToList();
            return Ok(TiposCulinarias);
        }

        [HttpGet("{id}")]
        public ActionResult ObterTipoCulinaria(int id)
        {
            var tipoCulinaria = _context.TipoCulinaria.Where(tipoCulinaria => tipoCulinaria.Id == id).FirstOrDefault();
            return Ok(tipoCulinaria);
        }
    }
}