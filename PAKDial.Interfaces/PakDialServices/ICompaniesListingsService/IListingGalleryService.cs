using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface IListingGalleryService
    {
        ListingGallery FindById(decimal Id);
        IEnumerable<ListingGallery> GetAll();
        List<ListingGallery> GetByListingId(decimal ListingId);
        decimal DeleteGalleryImage(string Url, decimal Id);
       


    }
}
