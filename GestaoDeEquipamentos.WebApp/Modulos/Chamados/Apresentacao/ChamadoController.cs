using Microsoft.AspNetCore.Mvc;

using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Infraestrutura;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Chamados.Infraestrutura;
using GestaoDeEquipamentos.WebApp.Modulos.Chamados.Dominio;

namespace GestaoDeEquipamentos.WebApp.Modulos.Chamados.Apresentacao;

public sealed class ChamadoController : Controller
{
    private readonly RepositorioChamadoEmArquivo repositorio;
    private readonly RepositorioEquipamentoEmArquivo repositorioEquipamento;

    public ChamadoController(RepositorioChamadoEmArquivo repositorio, RepositorioEquipamentoEmArquivo repositorioEquipamento)
    {
        this.repositorio = repositorio;
        this.repositorioEquipamento = repositorioEquipamento;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarChamadoViewModel> viewModels = new List<ListarChamadoViewModel>();

        foreach (Chamado chamado in repositorio.SelecionarTodos())
        {
            viewModels.Add(new ListarChamadoViewModel(
                chamado.Id,
                chamado.Titulo,
                chamado.Equipamento.Nome,
                chamado.Descricao,
                chamado.DataAbertura,
                chamado.DataFechamento
            ));
        }

        return View(viewModels);
    }

    [HttpGet]

    public ActionResult Cadastrar()
    {
        CadastrarChamadoViewModel viewModel = new CadastrarChamadoViewModel(
            string.Empty,
            string.Empty,
            0,
            DateTime.MinValue
        ) with
        { Equipamento = ObterEquipamentos() };

        return View(viewModel);
    }

    [HttpPost]

    public ActionResult Cadastrar(CadastrarChamadoViewModel viewModel)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(viewModel.EquipamentoId);

        if (equipamento == null)
        {
            return NotFound();
        }

        Chamado chamado = new Chamado(
            viewModel.Titulo ?? string.Empty,
            viewModel.Descricao ?? string.Empty,
            DateTime.Now
        )
        {
            Equipamento = equipamento
        };

        repositorio.Cadastrar(chamado);

        return RedirectToAction("Listar");
    }

    [HttpGet]

    public ActionResult Editar(int id)
    {
        Chamado? chamado = repositorio.SelecionarPorId(id);

        if (chamado == null)
        {
            return NotFound();
        }

        EditarChamadoViewModel viewModel = new EditarChamadoViewModel(
            chamado.Id,
            chamado.Titulo,
            chamado.Descricao,
            chamado.Equipamento.Id,
            chamado.DataAbertura,
            chamado.DataFechamento
        ) with
        { Equipamento = ObterEquipamentos() };

        return View(viewModel);
    }

    [HttpPost]

    public ActionResult Editar(EditarChamadoViewModel viewModel)
    {
        Equipamento? equipamento = repositorioEquipamento.SelecionarPorId(viewModel.EquipamentoId);

        if (equipamento == null)
        {
            return NotFound();
        }

        Chamado? chamadoAtualizado = new Chamado(
            viewModel.Titulo ?? string.Empty,
            viewModel.Descricao ?? string.Empty,
            viewModel.DataAbertura
        );


        bool conseguiuEditar = repositorio.Editar(viewModel.Id, chamadoAtualizado);

        if (!conseguiuEditar)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Chamado? chamado = repositorio.SelecionarPorId(id);

        if (chamado == null)
        {
            return NotFound();
        }

        ExcluirChamadoViewModel viewModel = new ExcluirChamadoViewModel(
            chamado.Id,
            chamado.Titulo,
            chamado.Descricao
        );

        return View(viewModel);
    }


    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(int id)
    {
        bool conseguiuExcluir = repositorio.Excluir(id);

        if (!conseguiuExcluir)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Listar));
    }

    private List<EquipamentoChamadoViewModel> ObterEquipamentos()
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarTodos();
        List<EquipamentoChamadoViewModel> viewModels = [];

        foreach (Equipamento equipamento in equipamentos)
        {
            viewModels.Add(new EquipamentoChamadoViewModel(equipamento.Id, equipamento.Nome));
        }

        return viewModels;
    }
}