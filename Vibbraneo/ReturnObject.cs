using Microsoft.AspNetCore.Mvc;
using Vibbraneo.API.Models;

namespace Vibbraneo.API
{
    public static class ReturnObject
    {
        public static ActionResult Return(bool success, string message = null, string internalMessage = null, object obj = null)
        {
            return Return(success, message, internalMessage, obj, 200);
        }

        public static ActionResult ExceptionResponse(bool success, string message, string internalMessage, object obj)
        {
            var statusCode = 500;

            return Return(success, message, internalMessage, obj, statusCode);
        }

        public static ActionResult InvalidModelStateResponse(bool success, string message, string internalMessage = null, object obj = null)
        {
            var statusCode = 400;

            return Return(success, message, internalMessage, obj, statusCode);
        }

        public static ActionResult Return(bool success, string message, string internalMessage, object obj, int statusCode)
        {
            var resultModel = new ReturnObjectModel() { ErrorResponse = new ErrorResponseModel[1] };

            if (success)
            {
                resultModel.Status = 1;
                resultModel.Object = obj;
            }
            else
            {
                resultModel.Status = 0;
                resultModel.ErrorResponse[0] = new ErrorResponseModel()
                {
                    Message = message,
                    InternalMessage = internalMessage,
                };
            }

            return new ObjectResult(resultModel) { StatusCode = statusCode };
        }

    }
}
