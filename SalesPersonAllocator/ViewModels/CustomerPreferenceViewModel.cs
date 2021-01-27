using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.ViewModels
{
    public class CustomerPreferenceViewModel
    {
        public CarPreferenceViewModel CarPreference { get; set; }
        
        public LanguagePreferenceViewModel LanguagePreference { get; set; }

        public CustomerPreference ToDomainEntity()
            => new CustomerPreference(
                (CarPreference) CarPreference, 
                (LanguagePreference) LanguagePreference);
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
