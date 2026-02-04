using FluentValidation;
using LoanMe.Data;
using LoanMe.Data.Entities;
using LoanMe.Data.Extensions;
using LoanMe.Infrastructure.Mediator.Abstractions;
using LoanMe.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanMe.Application.Loans.Commands {
    public class ApplyLoanCommand : ICommand<Result<ApplyLoanCommandResult>> {
        public long DraftLoanId { get; set; }
        public long ProductId { get; set; }
        public decimal RepaymentAmount { get; set; }
        public decimal EstablishmentFee { get; set; }
        public decimal TotalInterest { get; set; }
        public DateTime? Date { get; set; }
    }

    public class ApplyLoanCommandResult {
        public string? ApplicationNumber { get; set; }
    }

    public class ApplyLoanCommandValidator : AbstractValidator<ApplyLoanCommand> {
        public ApplyLoanCommandValidator() {
            RuleFor(param => param.DraftLoanId)
                .GreaterThan(default(long))
                .WithMessage("Draft loan id is required and must be greater than zero");

            RuleFor(param => param.ProductId)
                .GreaterThan(default(long))
                .WithMessage("Product id is required and must be greater than zero");

            RuleFor(param => param.RepaymentAmount)
                .GreaterThan(default(decimal))
                .WithMessage("Repayment amount is required and must be greater than zero");

            RuleFor(param => param.EstablishmentFee)
                .GreaterThanOrEqualTo(default(decimal))
                .WithMessage("Establishment fee must be greater than or equal to zero");

            RuleFor(param => param.TotalInterest)
                .GreaterThanOrEqualTo(default(decimal))
                .WithMessage("Total interest must be greater than or equal to zero");
        }
    }

    public class ApplyLoanCommandHandler : IRequestHandler<ApplyLoanCommand, Result<ApplyLoanCommandResult>> {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<ApplyLoanCommand> _validator;

        public ApplyLoanCommandHandler(ApplicationDbContext dbContext, IValidator<ApplyLoanCommand> validator) {
            _dbContext = dbContext;
            _validator = validator;
        }

        public virtual async Task<Result<ApplyLoanCommandResult>> HandleAsync(ApplyLoanCommand request) {
            await _validator.ValidateAndThrowAsync(request);

            var draftLoan = await _dbContext.DraftLoans
                .FindAsync(request.DraftLoanId);

            if (draftLoan is null) {
                throw new Exception($"There is no existing draft loan with id of: {request.DraftLoanId}");
            }

            if (draftLoan.IsApplied) {
                throw new Exception($"This draft loan with id {draftLoan.Id} have already been applied");
            }

            var blacklistedMobiles = await _dbContext.BlacklistMobiles
                .Select(list => list.MobileNumber)
                .ToListAsync();

            var blacklistedDomains = await _dbContext.BlackListEmailDomains
                .Select(list => list.Domain)
                .ToListAsync();

            var user = draftLoan.User;

            if (!user.IsLegalAge(ageRequired: 18)) {
                throw new Exception("User is not in legal age to request for a loan");
            }

            if (user.IsMobileBlacklisted(blacklistedMobiles)) {
                throw new Exception("User mobile number is blacklisted");
            }

            if (user.IsEmailDomainBlacklisted(blacklistedDomains)) {
                throw new Exception("User email domain is blacklisted");
            }

            var product = await _dbContext.Products
                .FindAsync(request.ProductId);

            if (product is null) {
                throw new Exception($"There is no existing product with id of: {request.ProductId}");
            }

            var appNumber = Guid.NewGuid();

            var activeLoan = new ActiveLoan {
                User = draftLoan.User,
                Product = product,
                ApplicationNumber = appNumber,
                LoanAmount = draftLoan.LoanAmount,
                LoanTerm = draftLoan.Term,
                RepaymentAmount = request.RepaymentAmount,
                TotalInterest = request.TotalInterest,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.ActiveLoans.Add(activeLoan);
            draftLoan.IsApplied = true;
            await _dbContext.SaveChangesAsync();

            return Result<ApplyLoanCommandResult>
                .Success(new() {
                    ApplicationNumber = appNumber.ToString()
                });
        }
    }
}
