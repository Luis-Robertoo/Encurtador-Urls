using Encurtei.API.DTOs;
using Encurtei.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Encurtei.API.Controllers;

[ApiController]
[Route("/[controller]")]


public class EncurtadorController : ControllerBase
{
    private readonly IEncurtadorService _encurtadorService;

    public EncurtadorController(IEncurtadorService encurtadorService)
    {
        _encurtadorService = encurtadorService;
    }

    [HttpPost("/encurtar")]
    public async Task<IActionResult> Post([FromBody] UrlRequestDTO request)
    {
        return Ok(await _encurtadorService.GerarUrlEncurtada(request.UrlOriginal));
    }

    [HttpGet("/{codigo}")]
    public async Task<IActionResult> Redirecionar([FromRoute] string codigo)
    {
        var response = await _encurtadorService.ObterUrlOriginal(codigo);
        if(response.Error != null)
        {
            return NotFound(response.Error);
        }
        return Redirect(response.Data);
    }

}
