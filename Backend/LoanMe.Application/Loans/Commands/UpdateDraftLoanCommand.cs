using FluentValidation;
using LoanMe.Data;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;

namespace LoanMe.Application.Loans.Commands {
    public class UpdateDraftLoanCommand : ICommand<Result<UpdateDraftLoanCommandResult>> {
        public long DraftLoanId { get; set; }
        public DraftLoanCommand? Data { get; set; }
    }

    public class UpdateDraftLoanCommandResult { }

    public class UpdateDraftLoanCommandValidator : AbstractValidator<UpdateDraftLoanCommand> {
        public UpdateDraftLoanCommandValidator() {
            RuleFor(param => param.DraftLoanId)
                .GreaterThan(default(long))
                .WithMessage("DraftLoanId must be greater than zero.");

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
                .FindAsync(request.DraftLoanId);

            if (draftLoan is null) {
                throw new Exception($"There is no existing draft loan with id of: {request.DraftLoanId}.");
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
            draftLoan.ModifiedAt = utcNow;

            await _dbContext.SaveChangesAsync();

            return Result<UpdateDraftLoanCommandResult>
                .Success(default!);
        }
    }
}
