using Encurtei.Data.Entities;

namespace Encurtei.Data.Repositories.Interfaces;

public interface ILinkRepository
{
    Task<Link> Criar(Link link);
    Task<Link?> ObterPorUrlOriginal(string urlOriginal);
    Task<Link?> ObterPorUrlEncurtada(string urlEncurtada);
    Task<IEnumerable<Link>> ObterExpirados(int segundos);
    Task Deletar(int id);
    Task DeletarExpirados(int segundos);

}
