using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.ViewModels;

namespace PAKDial.Interfaces.Repository
{
    public interface IListingPackagesRepository : IBaseRepository<ListingPackages, decimal>
    {
        ListingPackagesResponse GetListingPackages(ListingPackagesRequestModel request);
        VListingPackages GetById(decimal Id);
        decimal UpdatePackages(ListingPackageViewModel instance);
        decimal DeletePackage(decimal Id);
        bool CheckExistance(decimal? Id, string Name);
    }
}
