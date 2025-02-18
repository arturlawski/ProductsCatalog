using MediaExpert;
using MediaExpert.Abstractions.Application.Commands;
using MediaExpert.Abstractions.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ProductsCatalog.Server.Controllers
{
    [Route("api/products")]
    public class ProductsController : BaseController
    {
        public ProductsController(RequestDispatcher requestDispatcher) : base(requestDispatcher){}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts(int startIndex, int limit)
        {
            var response = await _requestDispatcher.SendAsync(new GetProducts(startIndex, limit));
            return Ok(response.Products);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct(CreateProduct request)
        {
            var response = await _requestDispatcher.SendAsync(request);
            return Ok(response);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CountProducts()
        {
            var response = await _requestDispatcher.SendAsync(new CountProducts());
            return Ok(response.Limit);
        }
    }
}
