using ConsumirApi.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace ConsumirApi.Controllers;

public class ProdutoController(ApiService apiService) : Controller
{
    private ApiService _apiService = apiService;

    public async Task<IActionResult> Index()
    {
        var produtos = await _apiService.GetAllProdutosAsync();
        return View(produtos);
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> GetAll()
    {
        var produtos = await _apiService.GetAllProdutosAsync();
        return View(produtos);
    }

    public async Task<IActionResult> Edit(int id, Produto produto)
    {
        await _apiService.PutProduto(id, produto);
        return RedirectToAction(nameof(System.Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _apiService.DeleteProduto(id);
        return RedirectToAction(nameof(Index));
    }
}