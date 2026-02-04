using LoanMe.Data.Abstractions;

namespace LoanMe.Data.Entities {
    public class ActiveLoan : IId {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public Guid ApplicationNumber { get; set; }
        public decimal LoanAmount { get; set; }
        public short LoanTerm { get; set; }
        public decimal RepaymentAmount { get; set; }
        public decimal TotalInterest { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual required User User { get; set; }
        public virtual required Product Product { get; set; }
    }
}
