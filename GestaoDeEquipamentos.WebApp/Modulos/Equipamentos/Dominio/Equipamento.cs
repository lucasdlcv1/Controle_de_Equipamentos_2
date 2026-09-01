
using GestaoDeEquipamentos.WebApp.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Dominio;

namespace GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;

public sealed class Equipamento : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;

    public Fabricante Fabricante { get; set; } = null!;

    public decimal Preco { get; set; } = 0m;

    public DateOnly DataFabricacao { get; set; } = default;

    public Equipamento() { }

    public Equipamento(string nome, Fabricante fabricante, decimal preco, DateOnly datafabricacao) : this()
    {
        Nome = nome;
        Fabricante = fabricante;
        Preco = preco;
        DataFabricacao = datafabricacao;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Equipamento equipamentoAtualizado = (Equipamento)entidadeAtualizada;

        Nome = equipamentoAtualizado.Nome;
        Fabricante = equipamentoAtualizado.Fabricante;
        Preco = equipamentoAtualizado.Preco;
        DataFabricacao = equipamentoAtualizado.DataFabricacao;
    }
}