using LoanMe.Application.Products.Queries;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoanMe.Api.Controllers {
    [Route("api/product")]
    public class ProductController : BaseController {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<Result<IEnumerable<GetAllProductsQueryResult>>> GetAllProducts([FromQuery] GetAllProductsQuery query) {
            return await _mediator.SendAsync(query);
        }
    }
}
