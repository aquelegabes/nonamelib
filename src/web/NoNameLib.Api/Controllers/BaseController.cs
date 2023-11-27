// wip
using Microsoft.AspNetCore.Mvc;
using NoNameLib.Api.Commands;

namespace NoNameLib.Api.Controllers;

public abstract class BaseController<TModel> : Controller
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
    [ValidateAntiForgeryToken]
    public virtual IActionResult Create(
        [FromBody] TModel model)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [ValidateAntiForgeryToken]
    [Route("/{id}")]
    public virtual IActionResult Edit(
        [FromRoute] object id,
        [FromBody] TModel model)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [ValidateAntiForgeryToken]
    [Route("/{id}")]
    public virtual IActionResult Delete(
        [FromRoute] object id)
    {
        throw new NotImplementedException();
    }
}
