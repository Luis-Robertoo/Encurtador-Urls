using Encurtei.API.DTOs;
using Encurtei.API.Services.Interface;
using Encurtei.Data.Entities;
using Encurtei.Data.Repositories.Interfaces;

namespace Encurtei.API.Services;

public class EncurtadorService(
    ILinkRepository _linkRepository,
    IHttpContextAccessor _contextAccessor) 
    : IEncurtadorService
{

    public string GerarCodigoUrlEncurtada()
    {
        Random rnd = new Random();

        int tamanho = rnd.Next(5, 11);
        var codigo = Guid.NewGuid().ToString("N").Substring(0, tamanho);

        return codigo;
    }

    public async Task<ResponseDTO<Link>> GerarUrlEncurtada(string urlOriginal)
    {
        try
        {
            var link = await _linkRepository.ObterPorUrlOriginal(urlOriginal);
            if (link != null)
            {
                return new ResponseDTO<Link>(link, null);
            }

            string urlEncurtada = GerarURIEncurtada(_contextAccessor);

            link = new Link(
                urlOriginal,
                urlEncurtada,
                DateTime.Now,
                "QR CODE ainda não implementado!"
            );

            var linkCriado = await _linkRepository.Criar(link);

            return new ResponseDTO<Link>(linkCriado, null);

        }
        catch (Exception ex) 
        {
            return new ResponseDTO<Link>(null, ex);
        }
    }

    public async Task<ResponseDTO<string>> ObterUrlOriginal(string codigo)
    {
        try
        {
            var urlEncurtada = GerarURIEncurtada(_contextAccessor, codigo);

            var link = await _linkRepository.ObterPorUrlEncurtada(urlEncurtada);
            if(link is null)
            {
                return new ResponseDTO<string>(null, "Não encontrado!");
            }

            return new ResponseDTO<string>(link.UrlOriginal, null);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<string>(null, ex.Message);
        }
    }

    private string GerarURIEncurtada(IHttpContextAccessor _contextAccessor, string codigo = null)
    {
        codigo = codigo is null ? GerarCodigoUrlEncurtada() : codigo;

        var protocol = _contextAccessor.HttpContext.Request.Scheme;

        var routeApi = Environment.GetEnvironmentVariable("ROUTE_API") ?? string.Empty;
        routeApi = routeApi != string.Empty ? $"/{routeApi}" : string.Empty;

        var host = $"{_contextAccessor.HttpContext.Request.Host}{routeApi}";
        var urlEncurtada = $"{protocol}://{host}/{codigo}";

        return urlEncurtada;
    }
}
