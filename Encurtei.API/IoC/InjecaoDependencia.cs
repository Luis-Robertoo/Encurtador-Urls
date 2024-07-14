using Encurtei.API.Services;
using Encurtei.API.Services.Interface;
using Encurtei.Data.Context;
using Encurtei.Data.Repositories;
using Encurtei.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Encurtei.API.IoC;

public static class InjecaoDependencia
{
    private static IServiceCollection _services;
    public static IConfiguration _configuration;

    public static void Configurar(IServiceCollection services, IConfiguration configuration)
    {
        _services = services;
        _configuration = configuration;

        InjetarServicos();
        InjetarRepositories();
        ConfigurarBancoDeDados();
    }

    private static void InjetarServicos()
    {   
        _services.AddHttpContextAccessor();
        _services.AddScoped<IEncurtadorService, EncurtadorService>();
    }

    private static void InjetarRepositories()
    {
        _services.AddScoped<ILinkRepository, LinkRepository>();
    }

    private static void ConfigurarBancoDeDados() 
    {
        var connection = _configuration.GetValue<string>("ConnectionString");
        if (connection == null || connection == "")
        {
            connection = Environment.GetEnvironmentVariable("CONNECTION");
        }

        _services.AddDbContext<AppDBContext>(
            options => options.UseSqlServer(connection));
    }

}
