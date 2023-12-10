using Microsoft.AspNetCore.Mvc;
using NoNameLib.Api.Commands;
using NoNameLib.Application.Interfaces;

namespace NoNameLib.Api.Controllers;

public class InputTest
{
    public string Name { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class DispatcherController : Controller
{
    private readonly IDispatcher _dispatcher;
    private readonly IResultDispatcher _resultDispatcher;
    private readonly IAsyncResultDispatcher _asyncResultDispatcher;

    public DispatcherController(
        IDispatcher dispatcher,
        IResultDispatcher resultDispatcher,
        IAsyncResultDispatcher asyncResultDispatcher)
    {
        _dispatcher = dispatcher;
        _resultDispatcher = resultDispatcher;
        _asyncResultDispatcher = asyncResultDispatcher;
    }

    [HttpPost]
    public IActionResult SimplePost(
        [FromBody] InputTest command)
    {
        _dispatcher.Dispatch(command);
        var result = _resultDispatcher.Dispatch(command, typeof(string));
        return Ok(result.GetResult<string>());
    }

    [HttpPatch]
    [ProducesResponseType(200, Type=typeof(OutputTest))]
    public async Task<IActionResult> SimplePatch(
        [FromBody] InputTest command)
    {
        var result = await _asyncResultDispatcher.Dispatch(command, typeof(OutputTest));
        return Ok(result.GetResult<OutputTest>());
    }
}
