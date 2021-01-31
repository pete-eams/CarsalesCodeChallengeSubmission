using DomainLogic.DomainModels.Enums;

namespace DomainLogic.DomainModels
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
