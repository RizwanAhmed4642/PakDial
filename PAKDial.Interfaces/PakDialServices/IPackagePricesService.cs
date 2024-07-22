using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using System.Collections.Generic;

namespace PAKDial.Interfaces.PakDialServices
{
    public interface IPackagePricesService
    {
        PackagePricesResponse GetPackagePrices(PackagePricesRequestModel request);
        List<VPackagePrices> GetAllPackagePrices(decimal Id);
        VPackagePrices GetPackagePriceById(decimal Id);
    }
}
