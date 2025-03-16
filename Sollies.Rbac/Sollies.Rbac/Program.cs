using MudBlazor.Services;
using Sollies.Rbac.Client.Pages;
using Sollies.Rbac.Components;
using Sollies.Rbac.Shared.Comparers;
using Sollies.Rbac.Shared.DataAccess;
using Sollies.Rbac.Shared.Factories;
using Sollies.Rbac.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddLogging();
builder.Services.AddMemoryCache();
builder.Services.AddMudServices();
builder.Services.AddSingleton<ISalesforceService, SalesforceService>();
builder.Services.AddSingleton<IForceClientFactory, ForceClientFactory>();
builder.Services.AddTransient<IRetrieveData, RetrieveData>();
builder.Services.AddTransient<ICompareSetupEntityPermissions, CompareSetupEntityPermissions>();
builder.Services.AddTransient<ICompareUserPermissions, CompareUserPermissions>();
builder.Services.AddTransient<ICompareObjectPermissions, CompareObjectPermissions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Sollies.Rbac.Client._Imports).Assembly);

app.Run();
