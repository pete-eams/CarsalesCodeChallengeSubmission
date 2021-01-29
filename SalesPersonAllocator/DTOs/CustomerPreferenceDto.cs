using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DTOs
{
    public class CustomerPreferenceDto
    {
        public CarPreferenceDto CarPreference { get; set; }
        
        public LanguagePreferenceDto LanguagePreference { get; set; }

        public CustomerPreference ToDomainEntity()
            => new CustomerPreference(
                (LanguagePreference) LanguagePreference,
                (CarPreference)CarPreference);
    }

    public enum LanguagePreferenceDto
    {
        Greek,
        DoesNotSpeakGreek
    }

    public enum CarPreferenceDto
    {
        Sports,
        Family,
        Tradie,
        NoPreference
    }
}
