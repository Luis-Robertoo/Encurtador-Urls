using Encurtei.API.DTOs;
using Encurtei.Data.Entities;

namespace Encurtei.API.Services.Interface;

public interface IEncurtadorService
{
    string GerarCodigoUrlEncurtada();
    Task<ResponseDTO<Link>> GerarUrlEncurtada(string urlOriginal);
    Task<ResponseDTO<string>> ObterUrlOriginal(string urlEncurtada);

}
