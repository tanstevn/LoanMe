using FluentAssertions;
using LoanMe.Application.Products.Queries;
using LoanMe.Shared.Models;
using LoanMe.Tests.Unit.Abstractions;

namespace LoanMe.Tests.Unit.Application.Products.Queries {
    public class GetAllProductsQueryTests : BaseUnitTest<GetAllProductsQueryTests, GetAllProductsQuery, 
        Result<ICollection<GetAllProductsQueryResult>>, GetAllProductsQueryHandler> {
        [Fact]
        public void GetAllProductsQuery_Runs_Successfully() {
            Arrange(
                request => { },
                expected => {
                    expected.Successful = true;
                    expected.Data = [
                        new() {
                            Id = 1,
                            Name = "Personal Loan",
                            Description = "A personal loan for various needs.",
                            MinimumTerm = 6,
                            MaximumTerm = 60,
                            MinLoanAmount = 1000m,
                            MaxLoanAmount = 50000m
                        },
                        new() {
                            Id = 2,
                            Name = "Home Loan",
                            Description = "A home loan for purchasing property.",
                            MinimumTerm = 12,
                            MaximumTerm = 360,
                            MinLoanAmount = 12000m,
                            MaxLoanAmount = 320000m
                        }
                    ];
                }
            )
            .Act()
            .Assert(result => {
                result.Successful
                .Should()
                .BeTrue();

                result.Data
                .Should()
                .NotBeNull();

                result.Data
                .Should()
                .HaveCount(2);
            });
        }
    }
}
