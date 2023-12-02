// wip
using Microsoft.AspNetCore.Mvc;
using NoNameLib.Api.Commands;
using NoNameLib.Domain.Interfaces;

namespace NoNameLib.Api.Controllers;

public abstract class BaseController<TModel> : Controller
    where TModel : class
{
    protected BaseController()
    {

    }

    [HttpGet]
    [Route("/{id}")]
    public virtual IActionResult Details(
        [FromRoute] object id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public virtual IActionResult Create(
        [FromBody] TModel model)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Route("/{id}")]
    public virtual IActionResult Edit(
        [FromRoute] object id,
        [FromBody] TModel model)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("/{id}")]
    public virtual IActionResult Delete(
        [FromRoute] object id)
    {
        throw new NotImplementedException();
    }
}
