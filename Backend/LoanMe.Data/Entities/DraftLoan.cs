using LoanMe.Data.Abstractions;

namespace LoanMe.Data.Entities {
    public class DraftLoan : IId {
        public long Id { get; set; }
        public long UserId { get; set; }
        public required short Term { get; set; }
        public required decimal LoanAmount { get; set; }
        public bool IsApplied { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual required User User { get; set; }
    }
}
