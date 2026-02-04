using LoanMe.Data.Abstractions;

namespace LoanMe.Data.Entities {
    public class User : IId {
        public long Id { get; set; }
        public string? Title { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public string? MobileNumber { get; set; }
        public required string EmailAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual DraftLoan? DraftLoan { get; set; }
        public virtual ActiveLoan? ActiveLoan { get; set; }
    }
}
