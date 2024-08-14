namespace WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Models;
    using WebApi.Services.Interfaces;
    using WebApi.Services.Models.LogToFile;

    [ApiController]
    [Route("[controller]")]
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        public LogController(ILogService logService, IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
        }

        [HttpPost("savetolog")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult WriteToLog([FromBody] SaveToLogRequest request)
        {
            var initialBuyData = _mapper.Map<LogToFileData>(request);
            _logService.LogToFile(initialBuyData);

            return Ok();
        }
    }
}
