using Microsoft.AspNetCore.Mvc;
using PA.Contracts.Interfaces;
using PA.Contracts.Models.Reporters;

namespace PA.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RequestController(IPovorotClient client, IPovorotEngineReporter reporter) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(DateTimeOffset date, CancellationToken cancellationToken)
    {
        var dateStart = new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, date.Offset);
        var dateEnd = new DateTimeOffset(date.Year, date.Month, date.Day, 23, 59, 59, date.Offset);
        return Ok(await client.GetEmptySlots(dateStart, dateEnd, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddRequestDto request, CancellationToken cancellationToken)
    {
        await reporter.NotifyAsync(request, cancellationToken);
        return NoContent();
    }
}