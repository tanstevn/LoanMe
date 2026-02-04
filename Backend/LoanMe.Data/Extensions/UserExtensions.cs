using LoanMe.Data.Entities;

namespace LoanMe.Data.Extensions {
    public static class UserExtensions {
        public static bool IsLegalAge(this User user, short ageRequired) {
            var today = DateTime.Today;
            var dateOfBirth = user.DateOfBirth;
            var ageToday = today.Year - dateOfBirth.Year;

            return ageToday >= ageRequired;
        }
    }
}
