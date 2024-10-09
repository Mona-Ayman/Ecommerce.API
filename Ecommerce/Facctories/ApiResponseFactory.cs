using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace Ecommerce.Facctories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            //get all errors in modelState
            var errors = context.ModelState.Where(error => error.Value.Errors.Any())
                .Select(error => new ValidationError
                {
                    field = error.Key,
                    Errors = error.Value.Errors.Select(e => e.ErrorMessage)
                });

            //create custom response
            var response = new ValidationErrorResponse
            {
                statusCode = (int)HttpStatusCode.BadRequest,
                errorMessage = "Validation Failed",
                Errors = errors
            };
            return new BadRequestObjectResult(response);
        }
    }
}
