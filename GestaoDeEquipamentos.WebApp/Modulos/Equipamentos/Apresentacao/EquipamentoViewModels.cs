using System.ComponentModel.DataAnnotations;

namespace GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Apresentacao;

public record FabricanteEquipamentoViewModel(
    int Id,
    string Nome
);
public record ListarEquipamentoViewModel(
    int Id,
    string Nome,
    string NomeFabricante,
    string Preco,
    DateTime DataFabricacao

);

public record CadastrarEquipamentoViewModel(
    string? Nome,
    string NomeFabricante,
    string Preco,
    int FabricanteId,
    DateTime DataFabricacao
)
{
    public List<FabricanteEquipamentoViewModel> Fabricante { get; init; } = [];
}

public record EditarEquipamentoViewModel(
    int Id,
    string? Nome,
     string NomeFabricante,
    string Preco,
    int FabricanteId,
    DateTime DataFabricacao
)
{
    public List<FabricanteEquipamentoViewModel> Fabricante { get; init; } = [];
};

public record ExcluirEquipamentoViewModel(int Id, string Nome);
