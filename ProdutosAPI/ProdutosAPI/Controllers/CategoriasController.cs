using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutosAPI.Context;
using ProdutosAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosAPI.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _appDBContext;
        public CategoriasController(AppDbContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        
        [HttpGet("{Nome}")]
        public IActionResult GetCategoriaPorNome(string Nome)
        {
            var found = _appDBContext.Categorias.Where(p => p.Nome == Nome).Select(p => p);

            return Ok(found);

        }
        [HttpGet("ativo/{Situacao}")]
        public IActionResult GetCategoriaPorSituacao(bool Situacao)
        {
            var found = _appDBContext.Categorias.Where(p => p.Situacao == Situacao).Select(p => p);

            return Ok(found);

        }
        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            return Ok(new
            {
                data = await _appDBContext.Categorias.ToListAsync(),
                success = true
            }
            );



        }

        [HttpPost]
        public async Task<IActionResult> InserirCategoria(Categoria Categoria)
        {
            var IdExists = await _appDBContext.Categorias.FindAsync(Categoria.Id);
            if (IdExists is not null)
            {
                return BadRequest();
            }
            
            _appDBContext.Categorias.Add(Categoria);
            await _appDBContext.SaveChangesAsync();
            return Ok(new
            {
                data = await _appDBContext.Categorias.ToListAsync(),
                success = true

            }
            );



        }
        [HttpPut]
        public async Task<IActionResult> AtualizarCategoria(int id, Categoria categoria)
        {


            if (id != categoria.Id)
            {
                return BadRequest();

            }

            _appDBContext.Entry(categoria).State = EntityState.Modified;

            await _appDBContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = categoria
            });
        }
    }
}

