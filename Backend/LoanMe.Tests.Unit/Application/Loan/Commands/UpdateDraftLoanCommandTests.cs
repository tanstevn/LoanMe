using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using LoanMe.Application.Loan.Commands;
using LoanMe.Shared.Models;
using LoanMe.Tests.Unit.Abstractions;

namespace LoanMe.Tests.Unit.Application.Loan.Commands {
    public class UpdateDraftLoanCommandTests : BaseUnitTestWithValidator<UpdateDraftLoanCommandTests, UpdateDraftLoanCommand,
        Result<UpdateDraftLoanCommandResult>, UpdateDraftLoanCommandHandler, UpdateDraftLoanCommandValidator> {
        private const string ExpectedRedirectURL = "http://steven-tan.dev";

        [Fact]
        public void UpdateDraftLoanCommand_Runs_Successfully() {
            Arrange(
                request => {
                    request.Id = 1;
                    request.Data = new() {
                        Title = "Mr.",
                        FirstName = "Lester",
                        LastName = "Tan",
                        DateOfBirth = new DateTime(1998, 06, 21),
                        MobileNumber = "12345678900",
                        EmailAddress = "steven@domain.com",
                        Term = 3,
                        LoanAmount = 2500m
                    };
                },
                expected => {
                    expected.Data = new() {
                        RedirectURL = ExpectedRedirectURL
                    };
                }
            )
            .Act()
            .Assert();
        }

        [Fact]
        public void UpdateDraftLoanCommand_Validate_Required_Parameter_Id_Throws_ValidationException() {
            Arrange(
                request => {
                    request.Id = 0;
                    request.Data = new() {
                        Title = "Mr.",
                        FirstName = "Lester",
                        LastName = "Tan",
                        DateOfBirth = new DateTime(1998, 06, 21),
                        MobileNumber = "12345678900",
                        EmailAddress = "steven@domain.com",
                        Term = 3,
                        LoanAmount = 2500m
                    };
                },
                validationResult => {
                    validationResult.Errors.Add(new ValidationFailure("Id", "Id must be greater than zero."));
                }
            )
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors.Should()
                    .HaveCount(1);
            });
        }

        [Fact]
        public void UpdateDraftLoanCommand_Validate_Required_Parameter_Data_As_Null_Throws_ValidationException() {
            Arrange(
                request => {
                    request.Id = 1;
                    request.Data = null;
                },
                validationResult => {
                    validationResult.Errors.Add(new ValidationFailure("Data", "Data is required."));
                }
            )
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors.Should()
                .HaveCount(1);
            });
        }

        [Fact]
        public void UpdateDraftLoanCommand_Validate_Required_Parameter_Data_With_No_FirstName_Throws_ValidationException() {
            Arrange(
                request => {
                    request.Id = 1;
                    request.Data = new() {
                        Title = "Mr.",
                        LastName = "Tan",
                        DateOfBirth = new DateTime(1998, 06, 21),
                        MobileNumber = "12345678900",
                        EmailAddress = "steven@domain.com",
                        Term = 3,
                        LoanAmount = 2500m
                    };
                },
                validationResult => {
                    validationResult.Errors.Add(new ValidationFailure("FirstName", "First name is required."));
                }
            )
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors.Should()
                    .HaveCount(1);
            });
        }
    }
}
