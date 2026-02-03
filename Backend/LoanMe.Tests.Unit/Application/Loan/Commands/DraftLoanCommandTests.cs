using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using LoanMe.Application.Loans.Commands;
using LoanMe.Shared.Models;
using LoanMe.Tests.Unit.Abstractions;

namespace LoanMe.Tests.Unit.Application.Loan.Commands {
    public class DraftLoanCommandTests : BaseUnitTestWithValidator<DraftLoanCommandTests, DraftLoanCommand, 
        Result<DraftLoanCommandResult>, DraftLoanCommandHandler, DraftLoanCommandValidator> {
        private const string ExpectedRedirectURL = "http://steven-tan.dev";

        [Fact]
        public void DraftLoanCommand_Runs_Successfully() {
            Arrange(
                request => {
                    request.Title = "Mr.";
                    request.FirstName = "Steven";
                    request.LastName = "Tan";
                    request.DateOfBirth = new DateTime(1998, 06, 21);
                    request.MobileNumber = "12345678900";
                    request.EmailAddress = "steven@domain.com";
                    request.Term = 2;
                    request.LoanAmount = 5000m;
                },
                expected => {
                    expected.Successful = true;
                    expected.Data = new() {
                        RedirectURL = ExpectedRedirectURL
                    };
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
            });
        }

        [Fact]
        public void DraftLoanCommand_Validate_Required_Parameters_Throws_ValidationException() {
            Arrange(
                request => { },
                validationResult => {
                    validationResult.Errors.Add(new ValidationFailure("FirstName", "First name is required."));
                    validationResult.Errors.Add(new ValidationFailure("LastName", "Last name is required."));
                    validationResult.Errors.Add(new ValidationFailure("DateofBirth", "Date of birth is required."));
                    validationResult.Errors.Add(new ValidationFailure("EmailAddress", "Email address is required."));
                    validationResult.Errors.Add(new ValidationFailure("Term", "Term is required and must be greater than zero."));
                    validationResult.Errors.Add(new ValidationFailure("LoanAmount", "Loan amount is required and must be greater than zero."));
                }
            )
            .Act()
            .AssertThrows<ValidationException>(ex => {
                ex.Errors.Should()
                    .HaveCount(6);
            });
        }
    }
}
