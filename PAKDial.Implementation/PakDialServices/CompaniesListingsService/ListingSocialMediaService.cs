using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class ListingSocialMediaService : IListingSocialMediaService
    {
        private readonly IListingSocialMediaRepository listingSocialMediaRepository;
        public ListingSocialMediaService(IListingSocialMediaRepository listingSocialMediaRepository)
        {
            this.listingSocialMediaRepository = listingSocialMediaRepository;
        }

        public ListingSocialMedia FindById(decimal Id)
        {
            return listingSocialMediaRepository.Find(Id);
        }

        public IEnumerable<ListingSocialMedia> GetAll()
        {
            return listingSocialMediaRepository.GetAll();
        }

        public List<ListingSocialMedia> GetByListingId(decimal ListingId)
        {
            return listingSocialMediaRepository.GetByListingId(ListingId);
        }

        public ListingSocialMedia GetListingSocialMedia(decimal MediaId, decimal ListingId)
        {
            return listingSocialMediaRepository.GetListingSocialMedia(MediaId,ListingId);
        }

        public List<ListingSocialMedia> GetListingSocialMediaId(decimal MediaId)
        {
            return listingSocialMediaRepository.GetListingSocialMediaId(MediaId);
        }
    }
}
