
using GestaoDeEquipamentos.WebApp.Compartilhado.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Dominio;

namespace GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;

public sealed class Equipamento : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;

    public Fabricante Fabricante { get; set; } = null!;

    public decimal PrecoAquisicao { get; set; } = 0m;

    public DateTime DataFabricacao { get; set; } = default;

    public Equipamento() { }

    public Equipamento(string nome, Fabricante fabricante, decimal preco, DateTime datafabricacao) : this()
    {
        Nome = nome;
        Fabricante = fabricante;
        PrecoAquisicao = preco;
        DataFabricacao = datafabricacao;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Equipamento equipamentoAtualizado = (Equipamento)entidadeAtualizada;

        Nome = equipamentoAtualizado.Nome;
        Fabricante = equipamentoAtualizado.Fabricante;
        PrecoAquisicao = equipamentoAtualizado.PrecoAquisicao;
        DataFabricacao = equipamentoAtualizado.DataFabricacao;
    }
}