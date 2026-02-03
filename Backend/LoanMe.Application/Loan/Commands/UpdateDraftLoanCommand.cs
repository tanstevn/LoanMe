using FluentValidation;
using LoanMe.Data;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;

namespace LoanMe.Application.Loan.Commands {
    public class UpdateDraftLoanCommand : ICommand<Result<UpdateDraftLoanCommandResult>> {
        public long Id { get; set; }
        public DraftLoanCommand? Data { get; set; }
    }

    public class UpdateDraftLoanCommandResult {
        public string? RedirectURL { get; set; }
    }

    public class UpdateDraftLoanCommandValidator : AbstractValidator<UpdateDraftLoanCommand> {
        public UpdateDraftLoanCommandValidator() {
            RuleFor(param => param.Id)
                .GreaterThan(default(long))
                .WithMessage("Id must be greater than zero.");

            RuleFor(param => param.Data)
                .NotNull()
                .WithMessage("Data is required.")
                .SetValidator(new DraftLoanCommandValidator()!);
        }
    }

    public class UpdateDraftLoanCommandHandler : IRequestHandler<UpdateDraftLoanCommand, Result<UpdateDraftLoanCommandResult>> {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<UpdateDraftLoanCommand> _validator;

        public UpdateDraftLoanCommandHandler(ApplicationDbContext dbContext, IValidator<UpdateDraftLoanCommand> validator) {
            _dbContext = dbContext;
            _validator = validator;
        }

        public virtual async Task<Result<UpdateDraftLoanCommandResult>> HandleAsync(UpdateDraftLoanCommand request) {
            await _validator.ValidateAndThrowAsync(request);

            var draftLoan = await _dbContext.DraftLoans
                .FindAsync(request.Id);

            if (draftLoan is null) {
                throw new Exception($"There is no existing draft loan with id of: {request.Id}.");
            }

            var utcNow = DateTime.UtcNow;

            draftLoan.User.Title = request.Data!.Title;
            draftLoan.User.FirstName = request.Data!.FirstName!;
            draftLoan.User.LastName = request.Data!.LastName!;
            draftLoan.User.DateOfBirth = request.Data!.DateOfBirth;
            draftLoan.User.MobileNumber = request.Data!.MobileNumber;
            draftLoan.User.EmailAddress = request.Data!.EmailAddress!;
            draftLoan.User.ModifiedAt = utcNow;

            draftLoan.Term = request.Data!.Term;
            draftLoan.LoanAmount = request.Data!.LoanAmount;

            return Result<UpdateDraftLoanCommandResult>
                .Success(new() {
                    RedirectURL = $"http://localhost:3001/loan/calculator?id={draftLoan.Id}"
                });
        }
    }
}
