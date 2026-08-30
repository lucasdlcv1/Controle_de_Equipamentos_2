using GestaoDeEquipamentos.WebApp.Compartilhado.Apresesntacao;
using GestaoDeEquipamentos.WebApp.Compartilhado.Infraestrutura;

var builder = WebApplication.CreateBuilder(args);

// Configura a infraestrutura (Arquivos, Banco de dados, Caches, Logs)
builder.Services.AdicionarCamadaDeInfraestrutura();

// Configura o MVC / Apresentacao
builder.Services.AdicionarCamadaDeApresentacao();

var app = builder.Build();

// Midlewares
app.UseRouting();
app.MapDefaultControllerRoute();

app.UseStaticFiles();

// Executa o servidor
app.Run();
