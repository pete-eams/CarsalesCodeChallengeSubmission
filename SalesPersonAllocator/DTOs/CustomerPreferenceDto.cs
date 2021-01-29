using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DTOs
{
    public class CustomerPreferenceDto
    {
        public CarPreferenceViewModel CarPreference { get; set; }
        
        public LanguagePreferenceViewModel LanguagePreference { get; set; }

        public CustomerPreference ToDomainEntity()
            => new CustomerPreference(
                (LanguagePreference) LanguagePreference,
                (CarPreference)CarPreference);
    }

    public enum LanguagePreferenceViewModel
    {
        Greek,
        DoesNotSpeakGreek,
        NoPreference
    }

    public enum CarPreferenceViewModel
    {
        Sports,
        Family,
        Tradie,
        NoPreference
    }
}
