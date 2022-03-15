using Microsoft.AspNetCore.Mvc;
using Vibbraneo.API.Business.Interfaces;
using Vibbraneo.API.Models;

namespace Vibbraneo.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyBusiness business;

        public CompanyController(ICompanyBusiness _business)
        {
            business = _business;
        }

        /// <summary>
        /// Get Company registers based on Name, CNPJ and/or Corporate Name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="cnpj"></param>
        /// <param name="corporateName"></param>
        /// <returns>List CompanyModel</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] int? id, string name, string cnpj, string corporateName)
        {
            var result = business.Get(id, name, cnpj, corporateName);

            return Ok(result);
        }

        /// <summary>
        /// Insert of a Company register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id Company inserted</returns>
        [HttpPost]
        public IActionResult Insert([FromBody] InsertCompanyModel model)
        {
            var result = business.Insert(model);

            return ReturnObject.Return(success: true, obj: result);
        }

        /// <summary>
        /// Update a Company register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True or Error</returns>
        [HttpPut]
        public IActionResult Update([FromBody] UpdateCompanyModel model)
        {
            var result = business.Update(model);

            return ReturnObject.Return(success: true, obj: result);
        }

        [HttpGet]
        public IActionResult teste()
        {
            int x = business.RemoveDuplicates(new int[] { 1, 1, 2 });
            return Ok(x);
        }
    }
}
