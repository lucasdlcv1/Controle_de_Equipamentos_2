namespace GestaoDeEquipamentos.WebApp.Modulos.Chamados.Apresentacao;


public record EquipamentoChamadoViewModel(
    int Id,
    string Nome
);
public record ListarChamadoViewModel(
    int Id,
    string Titulo,
    string NomeEquipamento,
    string Descricao,
    DateTime DataAbertura,
    DateTime? DataFechamento
);

public record CadastrarChamadoViewModel(
    string Titulo,
    string? Descricao,
    int EquipamentoId,
    DateTime DataAbertura
)
{
    public List<EquipamentoChamadoViewModel> Equipamento { get; init; } = [];
}

public record EditarChamadoViewModel(
    int Id,
    string Titulo,
    string? Descricao,
    int EquipamentoId,
    DateTime DataAbertura,
    DateTime? DataFechamento
)
{
    public List<EquipamentoChamadoViewModel> Equipamento { get; init; } = [];
};

public record ExcluirChamadoViewModel(int Id, string Titulo, string Descricao);