using LoanMe.Data;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanMe.Application.Products.Queries {
    public class GetAllProductsQuery : IQuery<Result<ICollection<GetAllProductsQueryResult>>> { }

    public class GetAllProductsQueryResult {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public short MinimumTerm { get; set; }
        public short MaximumTerm { get; set; }
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<ICollection<GetAllProductsQueryResult>>> {
        private readonly ApplicationDbContext _dbContext;

        public GetAllProductsQueryHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public virtual async Task<Result<ICollection<GetAllProductsQueryResult>>> HandleAsync(GetAllProductsQuery request) {
            var products = await _dbContext.Products
                .Select(p => new GetAllProductsQueryResult {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    MinimumTerm = p.LoanTermMinimum,
                    MaximumTerm = p.LoanTermMaximum
                })
                .ToListAsync();

            return Result<ICollection<GetAllProductsQueryResult>>
                .Success(products);
        }
    }
}
