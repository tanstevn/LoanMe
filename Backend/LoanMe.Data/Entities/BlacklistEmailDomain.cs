using LoanMe.Data.Abstractions;

namespace LoanMe.Data.Entities {
    public class BlacklistEmailDomain : IId {
        public long Id { get; set; }
        public string Domain { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
