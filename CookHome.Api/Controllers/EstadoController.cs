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
    public class EstadoController : ControllerBase
    {
        private readonly CookHomeContext _context;

        public EstadoController(CookHomeContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public ActionResult ObterTodosEstados()
        {
            var estados = _context.Estado.ToList();
            return Ok(estados);
        }

        [HttpGet("{id}")]
        public ActionResult ObterEstado(int id)
        {
            var Estado = _context.Estado.Where(estado => estado.Id == id).FirstOrDefault();
            return Ok(Estado);
        }
   
    }
}