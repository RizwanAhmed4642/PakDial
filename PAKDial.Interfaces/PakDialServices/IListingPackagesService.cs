using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using System;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices
{
    public interface IListingPackagesService
    {
        ListingPackagesResponse GetListingPackages(ListingPackagesRequestModel request);
        decimal Add(ListingPackageViewModel instance);
        decimal UpdatePackages(ListingPackageViewModel instance);
        decimal DeletePackage(decimal Id);
        ListingPackageViewModel GetById(decimal Id);
    }
}
