using Microsoft.AspNetCore.Mvc;
using Vibbraneo.API.Business.Interfaces;

namespace Vibbraneo.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsBusiness business;

        public ReportsController(IReportsBusiness _business)
        {
            business = _business;
        }

        /// <summary>
        /// Gets a report with the total invoice value by month, according to the year reference parameter
        /// </summary>
        /// <param name="yearRef"></param>
        /// <returns>List TotalInvoiceValueByMonth</returns>
        [HttpGet]
        public IActionResult TotalInvoiceValueByMonth([FromQuery] int yearRef)
        {
            var result = business.TotalInvoiceValueByMonth(yearRef);

            return Ok(result);
        }

        /// <summary>
        /// Gets a report with the total expense value by month, according to the year reference parameter
        /// </summary>
        /// <param name="yearRef"></param>
        /// <returns>List TotalExpenseValueByMonth</returns>
        [HttpGet]
        public IActionResult TotalExpenseValueByMonth([FromQuery] int yearRef)
        {
            var result = business.TotalExpenseValueByMonth(yearRef);

            return Ok(result);
        }

        /// <summary>
        /// Gets a report with the total expense value by category, according to the year reference parameter
        /// </summary>
        /// <param name="yearRef"></param>
        /// <returns>List TotalValueByCategory</returns>
        [HttpGet]
        public IActionResult TotalExpenseValueByCategory([FromQuery] int yearRef)
        {
            var result = business.TotalExpenseValueByCategory(yearRef);

            return Ok(result);
        }
    }
}
