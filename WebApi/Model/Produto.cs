namespace WebApi.Model;

public class Produto(int id, string nome, decimal preco, int estoque)
{
    public int Id { get; set; } = id;
    public string Nome { get; set; } = nome;
    public decimal Preco { get; set; } = preco;
    public int Estoque { get; set; } = estoque;
    
}