using System;
using SalesPersonAllocator.DomainModels.Enums;

namespace SalesPersonAllocator.DomainModels
{
    public class CustomerPreferenceCondition
    {
        private readonly Predicate<CarPreference> _carPredicate;
        private readonly Predicate<LanguagePreference> _langPredicate;

        public CustomerPreferenceCondition(
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