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
        var codigo = Guid.NewGuid().ToString().Replace("-","").Substring(0, tamanho);

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

            var codigo = GerarCodigoUrlEncurtada();

            var protocol = _contextAccessor.HttpContext.Request.Scheme;
            var host = _contextAccessor.HttpContext.Request.Host;

            var urlEncurtada = $"{protocol}://{host}/{codigo}";

            Console.WriteLine($"Protocolo {protocol}");
            Console.WriteLine($"host {host}");
            Console.WriteLine($"codigo {codigo}");
            Console.WriteLine($"{urlEncurtada}");

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
            var protocol = _contextAccessor.HttpContext.Request.Scheme;
            var host = _contextAccessor.HttpContext.Request.Host;

            Console.WriteLine($"Protocolo {protocol}");
            Console.WriteLine($"host {host}");
            Console.WriteLine($"codigo {codigo}");

            var urlEncurtada = $"{protocol}://{host}/{codigo}";

            Console.WriteLine($"{urlEncurtada}");

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
}
