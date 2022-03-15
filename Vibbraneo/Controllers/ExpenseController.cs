using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Vibbraneo.API.Business.Interfaces;
using Vibbraneo.API.Models;

namespace Vibbraneo.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseBusiness business;

        public ExpenseController(IExpenseBusiness _business)
        {
            business = _business;
        }

        /// <summary>
        /// Get Expense registers
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
        /// Insert of an Expense register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Id Expense inserted</returns>
        [HttpPost]
        public IActionResult Insert([FromBody] InsertExpenseModel model)
        {
            var result = business.Insert(model);

            return ReturnObject.Return(success: true, obj: result);
        }

        /// <summary>
        /// Update an Expense register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True or Error</returns>
        [HttpPut]
        public IActionResult Update([FromBody] UpdateExpenseModel model)
        {
            var result = business.Update(model);

            return ReturnObject.Return(success: true, obj: result);
        }

        /// <summary>
        /// Delete an Expense register
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
