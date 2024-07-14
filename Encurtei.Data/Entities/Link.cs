
namespace Encurtei.Data.Entities;

public class Link
{
    public int Id { get; private set; }
    public string UrlOriginal { get; private set; }
    public string UrlEncurtada { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public string QrCode { get; private set; }

    private Link() { }

    public Link(string urlOriginal, string urlEncurtada, DateTime criadoEm, string qrCode)
    {
        UrlEncurtada = urlEncurtada;
        UrlOriginal = urlOriginal;
        CriadoEm = criadoEm;
        QrCode = qrCode;
    }
}
