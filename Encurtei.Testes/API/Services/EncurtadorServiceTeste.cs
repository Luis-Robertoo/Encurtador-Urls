using Encurtei.API.Services;
using Encurtei.API.Services.Interface;
using Encurtei.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Encurtei.Testes.API.Services;

public class EncurtadorServiceTeste
{

    private readonly Mock<ILinkRepository> _linkRepository;
    private readonly Mock<IHttpContextAccessor> _contextAccessor;

    private readonly IEncurtadorService encurtadorService;

    public EncurtadorServiceTeste()
    {
        _linkRepository = new Mock<ILinkRepository>();
        _contextAccessor = new Mock<IHttpContextAccessor>();

        encurtadorService = new EncurtadorService(_linkRepository.Object, _contextAccessor.Object);
    }


    [Fact]
    public void Deve_Gerar_Codigo_Url_Encurtada()
    {
        //Arrange


        //Act
        var codigo = encurtadorService.GerarCodigoUrlEncurtada();


        //Assert
        Assert.Null(codigo);
        Assert.NotEmpty(codigo);
        Assert.InRange(codigo.Length, 5, 10);
    }
}
