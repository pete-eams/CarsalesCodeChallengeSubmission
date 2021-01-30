using System;
using SalesPersonAllocator.DomainLogic.Interfaces;

namespace SalesPersonAllocator.DomainModels
{
    public class SalesPersonAssignmentHandler
    {
        private readonly IHandler _handler;
        private readonly CustomerPreferenceCondition _condition;
        public SalesPersonAssignmentHandler(
            CustomerPreferenceCondition condition, 
            IHandler handler)
        {
            _handler = handler;
            _condition = condition;
        }

        public bool MatchesCustomerPreference(CustomerPreference customerPreference)
            => _condition.MatchesCustomerPreference(customerPreference);
            
        public object Handle() 
            => _handler.Handle();
    }
    
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

    public class CustomerPreference : IEquatable<CustomerPreference>
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

        public bool Equals(CustomerPreference other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return CarPreference == other.CarPreference && LanguagePreference == other.LanguagePreference;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CustomerPreference) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) CarPreference, (int) LanguagePreference);
        }
    }

    public enum LanguagePreference
    {
        Greek,
        DoesNotSpeakGreek
    }

    public enum CarPreference
    {
        Sports,
        Family,
        Tradie,
        NoPreference
    }
}
