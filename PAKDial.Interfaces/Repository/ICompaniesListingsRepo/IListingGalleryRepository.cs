using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository.ICompaniesListingsRepo
{
    public interface IListingGalleryRepository : IBaseRepository<ListingGallery,decimal>
    {
        List<ListingGallery> GetByListingId(decimal ListingId);
        decimal AddGalleryImages(List<ListingGallery> Entity);
        decimal UpdateGalleryImages(List<ListingGallery> Entity, decimal Id);
        decimal DeleteGalleryImages(string Url , decimal Id ,string path);
        List<ListingGallery> GetSelectedVluByListingId(decimal ListingId);



    }
}
