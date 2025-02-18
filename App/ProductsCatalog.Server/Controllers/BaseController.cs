using MediaExpert;
using Microsoft.AspNetCore.Mvc;
using MediaExpert.Abstractions.Application.Commands;
using MediaExpert.Abstractions.Application.Queries;

namespace ProductsCatalog.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly RequestDispatcher _requestDispatcher;

        protected BaseController(RequestDispatcher requestDispatcher)
        {
            _requestDispatcher = requestDispatcher ?? throw new ArgumentNullException(nameof(requestDispatcher));
        }
    }
}