
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using Shared;
using System.Net;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProductResultDTO), (int)HttpStatusCode.OK)]
    public class ApiController : ControllerBase
    {

    }
}
