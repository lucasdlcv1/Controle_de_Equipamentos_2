using GestaoDeEquipamentos.WebApp.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;

namespace GestaoDeEquipamentos.WebApp.Modulos.Chamados.Dominio;

public sealed class Chamado : EntidadeBase
{
    public string Titulo { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public DateTime DataAbertura { get; set; } = DateTime.Now;

    public DateTime? DataFechamento { get; set; } = DateTime.Now;

    public Equipamento Equipamento { get; set; } = new Equipamento();

    public Chamado() { }

    public Chamado(string titulo, string descricao, DateTime dataAbertura) : this()
    {
        Titulo = titulo;
        Descricao = descricao;
        DataAbertura = dataAbertura;
        DataFechamento = null;
        Equipamento = new Equipamento();
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Chamado chamadoAtualizado = (Chamado)entidadeAtualizada;

        Titulo = chamadoAtualizado.Titulo;
        Descricao = chamadoAtualizado.Descricao;
        DataAbertura = chamadoAtualizado.DataAbertura;
    }
}