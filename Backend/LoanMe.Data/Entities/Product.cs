using LoanMe.Data.Abstractions;

namespace LoanMe.Data.Entities {
    public class Product : IId {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public short LoanTermMinimum { get; set; }
        public short LoanTermMaximum { get; set; }
    }
}
