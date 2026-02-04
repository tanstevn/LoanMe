using LoanMe.Data;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanMe.Application.Products.Queries {
    public class GetAllProductsQuery : IQuery<Result<IEnumerable<GetAllProductsQueryResult>>> { }

    public class GetAllProductsQueryResult {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public short MinimumTerm { get; set; }
        public short MaximumTerm { get; set; }
        public decimal MinLoanAmount { get; set; }
        public decimal MaxLoanAmount { get; set; }
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<GetAllProductsQueryResult>>> {
        private readonly ApplicationDbContext _dbContext;

        public GetAllProductsQueryHandler(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public virtual async Task<Result<IEnumerable<GetAllProductsQueryResult>>> HandleAsync(GetAllProductsQuery request) {
            var products = await _dbContext.Products
                .Select(p => new GetAllProductsQueryResult {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    MinimumTerm = p.LoanTermMinimum,
                    MaximumTerm = p.LoanTermMaximum,
                    MinLoanAmount = p.LoanAmountMinimum,
                    MaxLoanAmount = p.LoanAmountMaximum
                })
                .ToListAsync();

            return Result<IEnumerable<GetAllProductsQueryResult>>
                .Success(products);
        }
    }
}
