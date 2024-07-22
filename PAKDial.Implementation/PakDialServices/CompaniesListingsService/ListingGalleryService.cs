using Microsoft.AspNetCore.Hosting;
using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class ListingGalleryService : IListingGalleryService
    {
        private readonly IListingGalleryRepository listingGalleryRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public ListingGalleryService(IListingGalleryRepository listingGalleryRepository , IHostingEnvironment hostingEnvironment)
        {
            this.listingGalleryRepository = listingGalleryRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        
        public decimal DeleteGalleryImage(string Url, decimal Id)
        {
            var path = Path.Combine(new string[]
                        {
                           hostingEnvironment.WebRootPath

                        });
            return listingGalleryRepository.DeleteGalleryImages(Url,Id, path);

        }

        public ListingGallery FindById(decimal Id)
        {
            return listingGalleryRepository.Find(Id);
        }

        public IEnumerable<ListingGallery> GetAll()
        {
            return listingGalleryRepository.GetAll();
        }

        public List<ListingGallery> GetByListingId(decimal ListingId)
        {
            return listingGalleryRepository.GetByListingId(ListingId);
        }
    }
}
