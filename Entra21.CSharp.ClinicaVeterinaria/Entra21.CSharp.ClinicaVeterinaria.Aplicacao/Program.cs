using Entra21.CSharp.ClinicaVeterinaria.Repositorio;
using Entra21.CSharp.ClinicaVeterinaria.Repositorio.BancoDados;
using Entra21.CSharp.ClinicaVeterinaria.Servico;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Registrar todas as inije??es de dependencia
builder.Services.AddScoped<IRacaRepositorio, RacaRepositorio>();
builder.Services.AddScoped<IRacaServico, RacaServico>();
builder.Services.AddScoped<IVeterinarioServico, VeterinarioServico>();

builder.Services.AddDbContext<ClinicaVeterinariaContexto>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer")));

var app = builder.Build();

using(var scopo = app.Services.CreateScope())
{
    var contexto = scopo.ServiceProvider.GetService<ClinicaVeterinariaContexto>();
    contexto.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=index}/{id?}");
});

app.Run();
