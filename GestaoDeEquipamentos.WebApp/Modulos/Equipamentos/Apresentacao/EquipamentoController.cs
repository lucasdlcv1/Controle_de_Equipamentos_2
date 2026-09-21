using Microsoft.AspNetCore.Mvc;

using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Infraestrutura;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Infraestrutura;

namespace GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Apresentacao;

public sealed class EquipamentoController : Controller
{
    private readonly IRepositorioEquipamento repositorio;
    private readonly IRepositorioFabricante repositorioFabricante;

    public EquipamentoController(IRepositorioEquipamento repositorio, IRepositorioFabricante repositorioFabricante)
    {
        this.repositorio = repositorio;
        this.repositorioFabricante = repositorioFabricante;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarEquipamentoViewModel> viewModels = new List<ListarEquipamentoViewModel>();

        List<Equipamento> equipamentos = repositorio.SelecionarTodos();

        foreach (Equipamento equipamento in equipamentos)
        {
            viewModels.Add(new ListarEquipamentoViewModel(
                equipamento.Id,
                equipamento.Nome,
                equipamento.Fabricante.Nome,
                equipamento.PrecoAquisicao,
                equipamento.DataFabricacao
            ));
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarEquipamentoViewModel viewModel = new CadastrarEquipamentoViewModel(
            string.Empty,
            0,
            0m,
            DateTime.MinValue,
            string.Empty
        ) with
        { Fabricante = ObterFabricantes() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarEquipamentoViewModel viewModel)
    {

        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(viewModel.FabricanteId);

        Equipamento equipamento = new Equipamento(
            viewModel.Nome ?? string.Empty,
            fabricante,
            viewModel.Preco,
            viewModel.DataFabricacao
        );

        repositorio.Cadastrar(equipamento);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Equipamento? equipamento = repositorio.SelecionarPorId(id);

        if (equipamento == null)
        {
            return NotFound();
        }

        EditarEquipamentoViewModel viewModel = new EditarEquipamentoViewModel(
            equipamento.Id,
            equipamento.Nome,
            equipamento.Fabricante.Id,
            equipamento.PrecoAquisicao,
             equipamento.DataFabricacao,
            equipamento.Fabricante.Nome

        ) with
        { Fabricante = ObterFabricantes() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarEquipamentoViewModel viewModel)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(viewModel.FabricanteId);

        if (fabricante == null)
        {
            return NotFound();
        }

        Equipamento? equipamentoAtualizado = new Equipamento(
            viewModel.Nome ?? string.Empty,
            fabricante,
            viewModel.Preco,
            viewModel.DataFabricacao
        );

        bool conseguiuEditar = repositorio.Editar(viewModel.Id, equipamentoAtualizado);

        if (!conseguiuEditar)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(int id)
    {
        Equipamento? equipamento = repositorio.SelecionarPorId(id);

        if (equipamento == null)
        {
            return NotFound();
        }

        ExcluirEquipamentoViewModel viewModel = new ExcluirEquipamentoViewModel(
            equipamento.Id,
            equipamento.Nome
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

    private List<FabricanteEquipamentoViewModel> ObterFabricantes()
    {
        List<Fabricante> fabricantes = repositorioFabricante.SelecionarTodos();

        List<FabricanteEquipamentoViewModel> viewModels = [];

        foreach (Fabricante fabricante in fabricantes)
        {
            viewModels.Add(new FabricanteEquipamentoViewModel(
                fabricante.Id,
                fabricante.Nome
            ));
        }

        return viewModels;
    }

}
