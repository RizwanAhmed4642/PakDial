using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface IListingPaymentsModeRepository : IBaseRepository<ListingPaymentsMode,decimal>
    {
        List<ListingPaymentsMode> GetByListingId(decimal ListingId);
        List<ListingPaymentsMode> GetListingPaymentModeId(decimal ModeId);
        ListingPaymentsMode GetListingPaymentMode(decimal ModeId, decimal ListingId);
    }
}
