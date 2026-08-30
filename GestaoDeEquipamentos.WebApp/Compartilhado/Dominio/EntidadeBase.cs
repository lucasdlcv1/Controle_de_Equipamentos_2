using System;

namespace GestaoDeEquipamentos.WebApp.Dominio;

public abstract class EntidadeBase
{
    public int Id { get; set; }

    public abstract void Atualizar(EntidadeBase entidadeAtualizada);
}
