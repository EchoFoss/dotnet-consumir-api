using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Infra.Db;
using WebApi.Model;

namespace WebApi.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProdutos()
    {
        var produto = await _context.Produtos.ToListAsync();

        return Ok(produto);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProdutoById([FromRoute] int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

        if (produto is null)
        {
            return NotFound();
        }

        return Ok(produto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditProduto([FromRoute] int id, [FromBody] Produto produtoEdit)
    {

        var produtoFound = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

        if (produtoFound is null)
        {
            return NotFound();
        }

        produtoFound.Nome = produtoEdit.Nome;
        produtoFound.Estoque = produtoEdit.Estoque;
        produtoFound.Preco = produtoEdit.Preco;

        _context.Entry(produtoFound).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> PostProduto([FromBody] Produto newProduto)
    {
        await _context.Produtos.AddAsync(newProduto);
        await _context.SaveChangesAsync();
        return Created();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduto([FromRoute] int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        if (produto is null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return Ok();
    }
}