namespace LoanMe.Application.Loan.Commands {
    public class ApplyLoanCommand {
        public long Id { get; set; }
        public decimal RepaymentAmount { get; set; }
        public decimal EstablishmentFee { get; set; }
        public decimal TotalInterest { get; set; }
    }
}
