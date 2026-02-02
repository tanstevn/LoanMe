namespace LoanMe.Application.Loan.Commands {
    public class UpdateDraftLoanCommand {
        public long Id { get; set; }
        public DraftLoanCommand? Data { get; set; }
    }
}
