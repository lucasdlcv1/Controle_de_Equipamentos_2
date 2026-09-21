using Dapper;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Dominio;
using Microsoft.Data.SqlClient;

public sealed class RepositorioEquipamentoEmSQL : IRepositorioEquipamento
{
    private string connectionString;

    public RepositorioEquipamentoEmSQL(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void Cadastrar(Equipamento novoRegistro)
    {
        const string query =
        """
        INSERT INTO TBEquipamentos (Nome, PrecoAquisicao, DataFabricacao, FabricanteId)
        OUTPUT INSERTED.Id
        VALUES (@Nome, @PrecoAquisicao, @DataFabricacao, @FabricanteId)

        """;

        using SqlConnection conexao = new(connectionString);

        conexao.QuerySingle<int>(query, new
        {
            Nome = novoRegistro.Nome,
            PrecoAquisicao = novoRegistro.PrecoAquisicao,
            DataFabricacao = novoRegistro.DataFabricacao,
            FabricanteId = novoRegistro.Fabricante.Id
        });
    }

    public bool Editar(int idSelecionado, Equipamento entidadeAtualizada)
    {
        const string query =
        """
       UPDATE TBEquipamentos
       SET
       Nome = @Nome,
       PrecoAquisicao = @PrecoAquisicao,
       DataFabricacao = @DataFabricacao,
       FabricanteId = @FabricanteID
       WHERE Id = @Id
       """;

        using SqlConnection conexao = new(connectionString);

        int quantidadeRegistrosAlterados = conexao.Execute(query, new
        {
            Id = idSelecionado,
            entidadeAtualizada.Nome,
            entidadeAtualizada.PrecoAquisicao,
            entidadeAtualizada.DataFabricacao,
            FabricanteId = entidadeAtualizada.Fabricante.Id,
        }
         );

        return quantidadeRegistrosAlterados == 1;
    }

    public bool Excluir(int idSelecionado)
    {
        const string query = "DELETE  FROM TBEquipamentos WHERE Id = @Id";

        using SqlConnection conexao = new(connectionString);

        int quantidadeRegistrosExcluidos = conexao.Execute(query, new { Id = idSelecionado });

        return quantidadeRegistrosExcluidos == 1;
    }

    public Equipamento? SelecionarPorId(int idSelecionado)
    {
        const string query =
       """
        SELECT e.[Id]
        ,e.[Nome]
        ,e.[PrecoAquisicao]
        ,e.[DataFabricacao]
        ,e.[FabricanteId],
        f.[Id],
        f.[Nome],
        f.[Email],
        f.[Telefone]
        FROM [TBEquipamentos] e
        JOIN TBFabricantes f ON f.Id = e.FabricanteId
        WHERE e .Id =@Id
        """;
        using SqlConnection conexao = new(connectionString);

        return conexao.Query<Equipamento, Fabricante, Equipamento>(
            query,
            MapearEquipamentoCompleto,
            new { Id = idSelecionado }
            ).SingleOrDefault();
    }

    public List<Equipamento> SelecionarTodos()
    {
        const string query =
        """
        SELECT e.[Id]
        ,e.[Nome]
        ,e.[PrecoAquisicao]
        ,e.[DataFabricacao]
        ,e.[FabricanteId],
        f.[Id],
        f.[Nome],
        f.[Email],
        f.[Telefone]
        FROM [TBEquipamentos] e
        JOIN TBFabricantes f ON f.Id = e.FabricanteId
        ORDER BY e.Id
        """;
        using SqlConnection conexao = new(connectionString);

        return conexao.Query<Equipamento, Fabricante, Equipamento>(
            query,
            MapearEquipamentoCompleto).ToList();

    }

    private static Equipamento MapearEquipamentoCompleto(Equipamento equipamento, Fabricante fabricante)
    {
        equipamento.Fabricante = fabricante;
        return equipamento;
    }
}