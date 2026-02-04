using LoanMe.Data.Abstractions;

namespace LoanMe.Data.Entities {
    public class BlacklistMobile : IId {
        public long Id { get; set; }
        public string MobileNumber { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
