using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices
{
    public class ListingPackagesService : IListingPackagesService
    {
        private readonly IListingPackagesRepository listingPackagesRepository;
        private readonly IPackagePricesService packagePricesService;

        public ListingPackagesService(IListingPackagesRepository listingPackagesRepository,
            IPackagePricesService packagePricesService)
        {
            this.listingPackagesRepository = listingPackagesRepository;
            this.packagePricesService = packagePricesService;
        }

        public decimal Add(ListingPackageViewModel instance)
        {
            decimal Save = 0; 
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            var CheckExistance = listingPackagesRepository.CheckExistance(0,instance.Name);
            if (!CheckExistance)
            {
                ListingPackages listingPackages = new ListingPackages
                {
                    Name = instance.Name,
                    Description = instance.Description,
                    Months= instance.Months,
                    IsActive = true,
                    CreatedBy = instance.CreatedBy,
                    CreatedDate = instance.CreatedDate,
                    UpdatedBy = instance.UpdatedBy,
                    UpdatedDate = instance.UpdatedDate,
                };
                listingPackages.PackagePrices.Add(new PackagePrices
                {
                    Price = instance.NewPrice,
                    IsActive = true,
                    CreatedBy = instance.CreatedBy,
                    CreatedDate = instance.CreatedDate,
                    UpdatedBy = instance.UpdatedBy,
                    UpdatedDate = instance.UpdatedDate,
                });
                listingPackagesRepository.Add(listingPackages);
                Save = listingPackagesRepository.SaveChanges(); 
            }
            else
            {
                Save = -2; 
            }
            return Save;
        }

        public decimal UpdatePackages(ListingPackageViewModel instance)
        {
            decimal Result = 0;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            bool result = listingPackagesRepository.CheckExistance(instance.Id, instance.Name);
            if (!result)
            {
                Result = listingPackagesRepository.UpdatePackages(instance);
            }
            else
            {
                Result = -2;
            }
            return Result;
        }

        public decimal DeletePackage(decimal Id)
        {
            return listingPackagesRepository.DeletePackage(Id);
        }

        public ListingPackagesResponse GetListingPackages(ListingPackagesRequestModel request)
        {
            return listingPackagesRepository.GetListingPackages(request);
        }

        public ListingPackageViewModel GetById(decimal Id)
        {
            ListingPackageViewModel vListing = new ListingPackageViewModel();
            var GetAllById = listingPackagesRepository.GetById(Id);
            if(GetAllById != null)
            {
                vListing.Id = GetAllById.Id;
                vListing.Name = GetAllById.Name;
                vListing.Months = GetAllById.Months;
                vListing.Description = GetAllById.Description;
                vListing.PriceId = GetAllById.PackagePriceId;
                vListing.ActivePrice = GetAllById.Price;
                vListing.IsActive = GetAllById.IsActive;
            }
            return vListing;
        }
    }
}
