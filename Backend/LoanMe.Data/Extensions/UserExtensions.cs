using LoanMe.Data.Entities;

namespace LoanMe.Data.Extensions {
    public static class UserExtensions {
        public static bool IsLegalAge(this User user, short ageRequired) {
            var today = DateTime.Today;
            var dateOfBirth = user.DateOfBirth;
            var ageToday = today.Year - dateOfBirth.Year;

            return ageToday >= ageRequired;
        }

        public static bool IsMobileBlacklisted(this User user, IEnumerable<string> mobileNumbers) {
            var mobileNumber = user.MobileNumber;
            return mobileNumbers.Any(number => mobileNumber == number);
        }

        public static bool IsEmailDomainBlacklisted(this User user, IEnumerable<string> domains) {
            var emailDomain = user.EmailAddress
                .Split('@')
                .Last();

            return domains.Any(domain => emailDomain == domain);
        }
    }
}
