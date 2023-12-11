using Microsoft.AspNetCore.Mvc;
using NoNameLib.Api.Commands;
using NoNameLib.Domain.Interfaces;
using NoNameLib.Domain.Utils;

namespace NoNameLib.Api.Controllers
{
    public class OutputTestQueryFilter : QueryFilter
    {
        public string Name { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class QueryHandlerCotroller : Controller
    {
        private readonly IQuery<OutputTest> _query;
        private readonly IQueryFiltered<OutputTest, OutputTestQueryFilter> _queryFiltered;

        private readonly IAsyncQuery<OutputTest> _asyncQuery;
        private readonly IAsyncQueryFiltered<OutputTest, OutputTestQueryFilter> _asyncQueryFiltered;

        public QueryHandlerCotroller(
            IQuery<OutputTest> query,
            IQueryFiltered<OutputTest, OutputTestQueryFilter> queryFiltered,
            IAsyncQuery<OutputTest> asyncQuery,
            IAsyncQueryFiltered<OutputTest, OutputTestQueryFilter> asyncQueryFiltered)
        {
            this._query = query;
            this._queryFiltered = queryFiltered;
            this._asyncQuery = asyncQuery;
            this._asyncQueryFiltered = asyncQueryFiltered;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _query.Get();
            await _asyncQuery.Get();
            return Ok(result);
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetFiltered(
            [FromQuery] OutputTestQueryFilter filters)
        {
            var result = _queryFiltered.Get(filters);
            await _asyncQueryFiltered.Get(filters);
            return Ok(result);
        }
    }
}
