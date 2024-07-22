using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices
{
    public class PackagePricesService : IPackagePricesService
    {
        private readonly IPackagePricesRepository packagePricesRepository;

        public PackagePricesService(IPackagePricesRepository packagePricesRepository)
        {
            this.packagePricesRepository = packagePricesRepository;
        }

        public List<VPackagePrices> GetAllPackagePrices(decimal Id)
        {
            return packagePricesRepository.GetAllPackagePrices(Id);
        }

        public VPackagePrices GetPackagePriceById(decimal Id)
        {
            return packagePricesRepository.GetPackagePriceById(Id);
        }

        public PackagePricesResponse GetPackagePrices(PackagePricesRequestModel request)
        {
            return packagePricesRepository.GetPackagePrices(request);
        }
    }
}
