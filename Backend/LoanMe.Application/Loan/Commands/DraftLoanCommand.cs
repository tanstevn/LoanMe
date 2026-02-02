namespace LoanMe.Application.Loan.Commands {
    public class DraftLoanCommand {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public short Term { get; set; }
        public decimal AmountRequired { get; set; }
    }
}
