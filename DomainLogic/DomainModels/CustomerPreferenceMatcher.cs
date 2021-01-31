using DomainLogic.DomainModels.Enums;
using System;

namespace DomainLogic.DomainModels
{
    public class CustomerPreferenceMatcher
    {
        private readonly Predicate<CarPreference> _carPredicate;
        private readonly Predicate<LanguagePreference> _langPredicate;

        public CustomerPreferenceMatcher(
            Predicate<CarPreference> carPredicate,
            Predicate<LanguagePreference> langPredicate)
        {
            _carPredicate = carPredicate;
            _langPredicate = langPredicate;
        }


        public bool MatchesCustomerPreference(CustomerPreference preference)
        {
            return _carPredicate.Invoke(preference.CarPreference) &&
                   _langPredicate.Invoke(preference.LanguagePreference);
        }
    }
}