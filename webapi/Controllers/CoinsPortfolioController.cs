using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.ChangeFromInitialBuy;
using WebApi.Services.Interfaces;
using WebApi.Services.Models.ChangeFromInitialBuy;

namespace Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CoinsPortfolioController : ControllerBase
{
    private readonly ILogger<CoinsPortfolioController> _logger;
    private readonly IMapper _mapper;
    private readonly ICoinloreService _coinloreService;

    public CoinsPortfolioController(ILogger<CoinsPortfolioController> logger, IMapper mapper, ICoinloreService coinloreService)
    {
        _logger = logger;
        _mapper = mapper;
        _coinloreService = coinloreService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<OverallChangeFromInitalBuyResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculatePercentageFromInitalBuyAsync([FromBody] ChangeFormInitialBuyRequest request, CancellationToken ct)
    {
        var changeFromInitialBuyDataList = _mapper.Map<InitialBuyData>(request);
        var initalBuy = await _coinloreService.CalculateChangeFromInitialBuyAsync(changeFromInitialBuyDataList, ct);

        return Ok(_mapper.Map<OverallChangeFromInitalBuyResponse>(initalBuy));
    }

}
