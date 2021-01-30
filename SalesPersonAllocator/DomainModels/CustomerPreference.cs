using SalesPersonAllocator.DomainModels.Enums;

namespace SalesPersonAllocator.DomainModels
{
    public class CustomerPreference
    {
        public CarPreference CarPreference { get; }
        public LanguagePreference LanguagePreference { get; }

        public CustomerPreference(
            LanguagePreference languagePreference,
            CarPreference carPreference)
        {
            CarPreference = carPreference;
            LanguagePreference = languagePreference;
        }
    }
}
