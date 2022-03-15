using Microsoft.AspNetCore.Mvc;
using Vibbraneo.API.Business.Interfaces;
using Vibbraneo.API.Models;

namespace Vibbraneo.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBusiness business;

        public CategoryController(ICategoryBusiness _business)
        {
            business = _business;
        }

        /// <summary>
        /// Get Category registers based on Name and/or Description
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="isActive"></param>
        /// <returns>List CategoryModel</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] int? id, string name, string description, bool isActive = true)
        {
            var result = business.Get(id, name, description, isActive);

            return Ok(result);
        }

        /// <summary>
        /// Insert of a Category register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id Category inserted</returns>
        [HttpPost]
        public IActionResult Insert([FromBody] InsertCategoryModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = business.Insert(model);

            return ReturnObject.Return(success: true, obj: result);
        }

        /// <summary>
        /// Update a Category register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True or Error</returns>
        [HttpPut]
        public IActionResult Update([FromBody] UpdateCategoryModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = business.Update(model);

            return ReturnObject.Return(success: true, obj: result);
        }
    }
}
