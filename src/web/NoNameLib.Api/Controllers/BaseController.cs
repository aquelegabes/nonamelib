//using Microsoft.AspNetCore.Mvc;
//using NoNameLib.Api.Commands;
//using NoNameLib.Api.Queries;
//using NoNameLib.Api.Interfaces;

//namespace NoNameLib.Api.Controllers
//{
//    public abstract class BaseController<T1> : Controller
//        where T1 : class, IIdentifiable, new()
//    {

//        protected BaseController()
//        {
            
//        }

//        [HttpGet]
//        [Route("/{id}")]
//        public virtual IActionResult Details(
//            [FromRoute] object id)
//        {
//            try
//            {
//                var result = _baseService.DetailsAs<T1>(id);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public virtual IActionResult Create(
//            [FromBody] T1 model)
//        {
//            try
//            {
//                var result = _baseService.CreateAs<T1>(model);
//                var createdUri = new Uri(Path.Combine(this.HttpContext.Request.Path, model.Id.ToString()));
//                return Created(createdUri, result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }

//        [HttpPut]
//        [ValidateAntiForgeryToken]
//        [Route("/{id}")]
//        public virtual IActionResult Edit(
//            [FromRoute] object id,
//            [FromBody] T1 model)
//        {
//            try
//            {
//                var result = _baseService.EditAs<T1>(id, model);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }

//        [HttpDelete]
//        [ValidateAntiForgeryToken]
//        [Route("/{id}")]
//        public virtual IActionResult Delete(
//            [FromRoute] object id)
//        {
//            try
//            {
//                var result = _baseService.Delete(id);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }
//    }
//}
