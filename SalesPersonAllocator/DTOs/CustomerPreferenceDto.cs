﻿using SalesPersonAllocator.DomainModels;
using SalesPersonAllocator.DomainModels.Enums;

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
}
