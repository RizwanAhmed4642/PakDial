using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class PremiumStatus
    {
        public PremiumStatus()
        {
            ListingPremium = new HashSet<ListingPremium>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }

        public ICollection<ListingPremium> ListingPremium { get; set; }
    }
}
