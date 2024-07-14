using Encurtei.Data.Context;
using Encurtei.Data.Entities;
using Encurtei.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Encurtei.Data.Repositories;

public class LinkRepository : ILinkRepository
{
    private readonly AppDBContext _context;

    public LinkRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<Link> Criar(Link link)
    {
        var result = await _context.Link.AddAsync(link);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<Link?> ObterPorUrlOriginal(string urlOriginal)
    {
        var result = await _context.Link.FirstOrDefaultAsync(l => l.UrlOriginal.ToLower().Equals(urlOriginal.ToLower()));
        return result;
    }

    public async Task<Link?> ObterPorUrlEncurtada(string urlEncurtada)
    {
        var result = await _context.Link.FirstOrDefaultAsync(l => l.UrlEncurtada.ToLower().Equals(urlEncurtada.ToLower()));
        return result;
    }

    public async Task<IEnumerable<Link>> ObterExpirados(int segundos)
    {
        var dataAtual = DateTime.Now;
        var links = await _context.Link.Where(l => l.CriadoEm.AddSeconds(segundos) < dataAtual).ToListAsync();
        return links;
    }

    public async Task Deletar(int id)
    {
        var link = await _context.Link.FirstOrDefaultAsync(l => l.Id.Equals(id));
        if (link is null)
        {
            return;
        }

        _context.Link.Remove(link);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarExpirados(int segundos)
    {
        var expirados = await ObterExpirados(segundos);
        if(!expirados.Any())
        {
            return;
        }

        _context.Link.RemoveRange(expirados);
        await _context.SaveChangesAsync();
    }
}
