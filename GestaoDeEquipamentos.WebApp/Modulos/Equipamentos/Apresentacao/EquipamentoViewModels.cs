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
    decimal Preco,
    DateOnly DataFabricacao

);

public record CadastrarEquipamentoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string? Nome,

    [Required(ErrorMessage = "O campo \"Fabricante\" é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "Você deve selecionar um fabricante.")]
    int FabricanteId,

    [Required(ErrorMessage = "O campo \"Preço\" é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Preço\" deve ser maior que zero.")]
    decimal Preco,

    DateOnly DataFabricacao,
    string NomeFabricante
)
{
    public List<FabricanteEquipamentoViewModel> Fabricante { get; init; } = [];
}

public record EditarEquipamentoViewModel(
    int Id,

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string? Nome,

    [Required(ErrorMessage = "O campo \"Fabricante\" é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "Você deve selecionar um fabricante.")]
    int FabricanteId,

    [Required(ErrorMessage = "O campo \"Preço\" é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Preço\" deve ser maior que zero.")]
    decimal Preco,

    DateOnly DataFabricacao,
    string NomeFabricante
)
{
    public List<FabricanteEquipamentoViewModel> Fabricante { get; init; } = [];
};

public record ExcluirEquipamentoViewModel(int Id, string Nome);
