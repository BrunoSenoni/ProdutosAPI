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
    public class ProdutosController : ControllerBase    {
        private readonly AppDbContext _appDBContext;
        public ProdutosController(AppDbContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        [HttpGet("categoria/{CategoriaId}")]
        public IActionResult GetProdutosPorCategoria(int CategoriaId)
        {
            var found = _appDBContext.Produtos.Where(p => p.CategoriaId == CategoriaId).Select(p => p);

            return Ok(found);
           
        }
        [HttpGet("descricao/{Descricao}")]
        public IActionResult GetProdutosPorDescricao(string Descricao)
        {
            var found = _appDBContext.Produtos.Where(p => p.Descricao == Descricao).Select(p => p);

            return Ok(found);

        }
        [HttpGet("situacao/{Situacao}")]
        public IActionResult GetProdutosPorDescricao(bool Situacao)
        {
            var found = _appDBContext.Produtos.Where(p => p.Situacao == Situacao).Select(p => p);

            return Ok(found);

        }
        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            return Ok(new
            {
                data = await _appDBContext.Produtos.ToListAsync(),
                success = true

            }
            );
                
                
             
        }

        [HttpPost]
        public async Task<IActionResult> InserirProduto(Produto Prod)
        {
            //checar se o id do produto enviado no payload ja está na tabela de produto
            var IdExists = await _appDBContext.Produtos.FindAsync(Prod.Id);
            if (IdExists is not null)
            {
                return BadRequest();
            }
            //checar se o id da categoria enviado no payload existe na tabela de categoria
            var CategoriaCheck = await _appDBContext.Categorias.FindAsync(Prod.CategoriaId);
            if (CategoriaCheck is null)
            {
                return BadRequest();
            }
            _appDBContext.Produtos.Add(Prod);
            await _appDBContext.SaveChangesAsync();
            return Ok(new
            {
                data = await _appDBContext.Produtos.ToListAsync(),
                success = true

            }
            );



        }
        [HttpPut]
        public async Task<IActionResult> AtualizarProduto(int id, Produto Prod)
        {


            if (id != Prod.Id)
            {
                return BadRequest();

            }
           
            _appDBContext.Entry(Prod).State = EntityState.Modified;

            await _appDBContext.SaveChangesAsync();

            return Ok(new {
                success = true
            });
        }
    }
}
