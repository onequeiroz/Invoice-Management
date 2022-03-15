using Microsoft.AspNetCore.Mvc.Filters;

namespace Vibbraneo.API
{
    public class CustomException : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ReturnObject.ExceptionResponse(false, context.Exception.Message, context.Exception.InnerException.Message, null);
        }
    }
}
