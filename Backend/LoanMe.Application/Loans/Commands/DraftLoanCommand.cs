using FluentValidation;
using LoanMe.Data;
using LoanMe.Data.Entities;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanMe.Application.Loans.Commands {
    public class DraftLoanCommand : ICommand<Result<DraftLoanCommandResult>> {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public short Term { get; set; }
        public decimal LoanAmount { get; set; }
    }

    public class DraftLoanCommandResult {
        public string? RedirectURL { get; set; }
    }

    public class DraftLoanCommandValidator : AbstractValidator<DraftLoanCommand> {
        public DraftLoanCommandValidator() {
            RuleFor(param => param.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("First name is required.");

            RuleFor(param => param.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Last name is required.");

            RuleFor(param => param.DateOfBirth)
                .NotNull()
                .WithMessage("Date of birth is required.");

            RuleFor(param => param.MobileNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("Mobile number is required.");

            RuleFor(param => param.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("Invalid email address format.");

            RuleFor(param => param.Term)
                .GreaterThan(default(short))
                .WithMessage("Term is required and must be greater than zero.");

            RuleFor(param => param.LoanAmount)
                .GreaterThan(default(decimal))
                .WithMessage("Loan amount is required and must be greater than zero.");
        }
    }

    public class DraftLoanCommandHandler : IRequestHandler<DraftLoanCommand, Result<DraftLoanCommandResult>> {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<DraftLoanCommand> _validator;

        public DraftLoanCommandHandler(ApplicationDbContext dbContext, IValidator<DraftLoanCommand> validator) {
            _dbContext = dbContext;
            _validator = validator;
        }

        public virtual async Task<Result<DraftLoanCommandResult>> HandleAsync(DraftLoanCommand request) {
            await _validator.ValidateAndThrowAsync(request);

            var existingDraftLoan = await _dbContext.DraftLoans
                .FirstOrDefaultAsync(draftLoan => draftLoan.User.FirstName == request.FirstName
                    && draftLoan.User.LastName == request.LastName
                    && draftLoan.User.DateOfBirth == request.DateOfBirth
                    && !draftLoan.IsApplied);

            if (existingDraftLoan is not null) {
                return Result<DraftLoanCommandResult>
                    .Success(new() {
                        RedirectURL = $"http://localhost:3001/loans?id={existingDraftLoan.Id}"
                    });
            }

            var utcNow = DateTime.UtcNow;

            var user = new User {
                Title = request.Title,
                FirstName = request.FirstName!,
                LastName = request.LastName!,
                DateOfBirth = request.DateOfBirth,
                MobileNumber = request.MobileNumber,
                EmailAddress = request.EmailAddress!,
                CreatedAt = utcNow
            };

            var draftLoan = new DraftLoan {
                Term = request.Term,
                LoanAmount = request.LoanAmount,
                User = user,
                CreatedAt = utcNow
            };

            _dbContext.DraftLoans.Add(draftLoan);
            await _dbContext.SaveChangesAsync();

            return Result<DraftLoanCommandResult>
                .Success(new() {
                    RedirectURL = $"http://localhost:3001/loans?id={draftLoan.Id}"
                });
        }
    }
}
