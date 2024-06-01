using RtuTools.Web.Components;
using RtuTools.Web.Interfaces;
using RtuTools.Web.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();

builder.Services
    .AddTransient<IExcelFileService>(x => new ExcelFileService(x.GetRequiredService<ILogger<ExcelFileService>>()));

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithCorrelationId()
    .Enrich.WithCorrelationIdHeader()
    .Enrich.WithThreadId()
    .Enrich.WithThreadName()
    .Enrich.WithProcessId()
    .Enrich.WithProcessName()
    .Enrich.WithEnvironmentName()
    .CreateLogger();

builder.Services.AddSingleton<ILoggerFactory>(_ => new Serilog.Extensions.Logging.SerilogLoggerFactory(logger, true));
builder.Services.AddSingleton(logger);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();