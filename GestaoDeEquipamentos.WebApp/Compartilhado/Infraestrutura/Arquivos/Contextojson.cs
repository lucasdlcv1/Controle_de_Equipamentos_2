using System.Text.Json;
using System.Text.Json.Serialization;
using GestaoDeEquipamentos.WebApp.Modulos.Equipamentos.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Fabricantes.Dominio;
using GestaoDeEquipamentos.WebApp.Modulos.Chamados.Dominio;

namespace GestaoDeEquipamentos.WebApp.Compartilhado.Infraestrutura.Arquivos;

public sealed class ContextoJson
{
    private readonly string caminhoArquivoDados;

    public List<Fabricante> Fabricantes { get; set; } = new List<Fabricante>();

    public List<Equipamento> Equipamentos { get; set; } = new List<Equipamento>();

    public List<Chamado> Chamados { get; set; } = new List<Chamado>();

    public ContextoJson()
    {
        string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorioAplicativo = Path.Join(caminhoAppData, "GestaoDeEquipamentos-Backend");

        Directory.CreateDirectory(caminhoDiretorioAplicativo);

        caminhoArquivoDados = Path.Join(caminhoDiretorioAplicativo, "dados.json");
    }

    public void Salvar()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, options);

        File.WriteAllText(caminhoArquivoDados, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivoDados))
        {
            Carregar(CarregarDadosPredefinidos());
            return;
        }

        string jsonString = File.ReadAllText(caminhoArquivoDados);

        if (string.IsNullOrWhiteSpace(jsonString))
        {
            Carregar(CarregarDadosPredefinidos());
            return;
        }

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo =
            JsonSerializer.Deserialize<ContextoJson>(jsonString, options);

        if (contextoSalvo == null || !contextoSalvo.PossuiDados())
            contextoSalvo = CarregarDadosPredefinidos();

        Carregar(contextoSalvo);
    }

    private void Carregar(ContextoJson contexto)
    {
        Fabricantes = contexto.Fabricantes;
        Equipamentos = contexto.Equipamentos;
        Chamados = contexto.Chamados;
    }

    public ContextoJson CarregarDadosPredefinidos()
    {
        ContextoJson contextoPredefinido = new ContextoJson();

        contextoPredefinido.Fabricantes.AddRange(new List<Fabricante>
        {
            new Fabricante("TechXuxa Equipamentos", "contato@techxuxa.com.br", "(11) 1234-5678") { Id = 1},
            new Fabricante("Negativo", "contato@negativo.com.br", "(12) 1334-5679") { Id = 2}

        });

        contextoPredefinido.Chamados.AddRange(new List<Chamado>
        {
            new Chamado("Problema com mouse", "Mouse não responde a cliques", DateTime.Now) { Id = 1 },
            new Chamado("Teclado com teclas quebradas", "Várias teclas do teclado estão com defeito", DateTime.Now) { Id = 2 }
        });

        return contextoPredefinido;
    }

    private bool PossuiDados()
    {
        return true;
    }
}