using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Model;

namespace ConsumirApi.Services;

public class ApiService(HttpClient httpClient)
{
    private const string EnderecoApi = "api/produtos/";

    public async Task<List<Produto>> GetAllProdutosAsync()
    {
        var response = await httpClient.GetAsync($"{EnderecoApi}");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<List<Produto>>())!;
    }

    public async Task<object> GetProdutoById(int id)
    {
        var res = await httpClient.GetAsync($"{EnderecoApi}/{id}");
        res.EnsureSuccessStatusCode();
        return (await res.Content.ReadFromJsonAsync<Produto>())!;
    }

    public async Task<HttpResponseMessage> PutProduto(int id, Produto produto)
    {
        return await httpClient.PutAsJsonAsync($"{EnderecoApi}/{id}", produto);
    }

    public async Task<HttpResponseMessage> DeleteProduto(int id)
    {
        return await httpClient.DeleteAsync($"{EnderecoApi}/{id}");
    }

    public async Task<HttpResponseMessage> PostProduto(Produto produto)
    {
        var json = JsonSerializer.Serialize(produto);
        HttpContent jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
        var res = await httpClient.PostAsync($"{EnderecoApi}", jsonContent);

        res.EnsureSuccessStatusCode();
        return res;
    }
}