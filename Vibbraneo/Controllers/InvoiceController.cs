using Microsoft.AspNetCore.Mvc;
using Vibbraneo.API.Business.Interfaces;
using Vibbraneo.API.Models;

namespace Vibbraneo.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceBusiness business;

        public InvoiceController(IInvoiceBusiness _business)
        {
            business = _business;
        }

        /// <summary>
        /// Get Invoice registers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] int? id)
        {
            var result = business.Get(id);

            return Ok(result);
        }

        /// <summary>
        /// Insert of an Invoice register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id Invoice inserted</returns>
        [HttpPost]
        public IActionResult Insert([FromBody] InsertInvoiceModel model)
        {
            var result = business.Insert(model);

            return ReturnObject.Return(success: true, obj: result);
        }

        /// <summary>
        /// Update an Invoice register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True or Error</returns>
        [HttpPut]
        public IActionResult Update([FromBody] UpdateInvoiceModel model)
        {
            var result = business.Update(model);

            return ReturnObject.Return(success: true, obj: result);
        }

        /// <summary>
        /// Delete an Invoice register
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True or Error</returns>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = business.Delete(id);

            return Ok(result);
        }
    }
}
