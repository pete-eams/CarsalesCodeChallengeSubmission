using System;

namespace SalesPersonAllocator.DomainModels
{
    public class CustomerPreference : IEquatable<CustomerPreference>
    {
        private readonly CarPreference _carPreference;
        private readonly LanguagePreference _languagePreference;
        
        public CustomerPreference(
            LanguagePreference languagePreference,
            CarPreference carPreference)
        {
            _carPreference = carPreference;
            _languagePreference = languagePreference;
        }

        public bool Equals(CustomerPreference other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _carPreference == other._carPreference && _languagePreference == other._languagePreference;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CustomerPreference) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) _carPreference, (int) _languagePreference);
        }
    }

    public enum LanguagePreference
    {
        Greek,
        DoesNotSpeakGreek,
        NoPreference
    }

    public enum CarPreference
    {
        Sports,
        Family,
        Tradie,
        NoPreference
    }
}
