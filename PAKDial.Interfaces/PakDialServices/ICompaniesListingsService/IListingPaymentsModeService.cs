using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface IListingPaymentsModeService
    {
        ListingPaymentsMode FindById(decimal Id);
        IEnumerable<ListingPaymentsMode> GetAll();
        List<ListingPaymentsMode> GetByListingId(decimal ListingId);
        List<ListingPaymentsMode> GetListingPaymentModeId(decimal ModeId);
        ListingPaymentsMode GetListingPaymentMode(decimal ModeId, decimal ListingId);
    }
}
