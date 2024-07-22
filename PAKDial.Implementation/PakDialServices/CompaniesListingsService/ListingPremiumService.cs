using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ResponseModels.SalePersonsOrders;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Interfaces.Repository.ICompaniesListingsRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Implementation.PakDialServices.CompaniesListingsService
{
    public class ListingPremiumService : IListingPremiumService
    {
        private readonly IListingPremiumRepository listingPremiumRepository;
        public ListingPremiumService(IListingPremiumRepository listingPremiumRepository)
        {
            this.listingPremiumRepository = listingPremiumRepository;
        }

        public decimal Add(ListingPremium listingPremium)
        {
            return listingPremiumRepository.AddAssignPackage(listingPremium);
        }

        public decimal Update(ListingPremium listingPremium)
        {
            return listingPremiumRepository.UpdateAssignPackage(listingPremium);
        }
        public decimal Delete(decimal Id)
        {
            return listingPremiumRepository.DeleteAssignPackage(Id);
        }
        public ListingPremium FindById(decimal Id)
        {
            return listingPremiumRepository.Find(Id);
        }

        public AssignListingPackageResponse Get(AssignListingPackageRequestModel request)
        {
            return listingPremiumRepository.Get(request);
        }

        public IEnumerable<ListingPremium> GetAll()
        {
            return listingPremiumRepository.GetAll();
        }

        public List<ListingPremium> GetByListingId(decimal ListingId)
        {
            return listingPremiumRepository.GetByListingId(ListingId);
        }

        public List<ListingPremium> GetListingPremium(decimal PackageId)
        {
            return listingPremiumRepository.GetListingPremium(PackageId);
        }

        public List<ListingPremium> GetListingPremium(decimal ModeId, decimal PackageId)
        {
            return listingPremiumRepository.GetListingPremium(ModeId,PackageId);
        }

        public ListingPremium GetListingPremium(decimal ModeId, decimal PackageId, decimal ListingId)
        {
            return listingPremiumRepository.GetListingPremium(ModeId, PackageId, ListingId);
        }

        public List<ListingPremium> GetListingPremiumModeId(decimal ModeId)
        {
            return listingPremiumRepository.GetListingPremium(ModeId);
        }

        public GetPackagesandModesResponse GetPakagesModes()
        {
            return listingPremiumRepository.GetPakagesModes();
        }


        // Sales Executive //Sale Manager Packages List On Load
        public GetModeandPackagesResponse GetModeandPackagesResponses()
        {
            return listingPremiumRepository.GetModeandPackagesResponses();
        }
        public GetModeandPackagesResponse GetPaymentsModeResponses()
        {
            return listingPremiumRepository.GetPaymentsModeResponses();
        }
        public VLoadSalesExectivePackagesResponse GetLoadSalesManagers(VLoadSalesExectivePackagesRequest request)
        {
            return listingPremiumRepository.GetLoadSalesManagers(request);
        }

        public AddSeOrderResponse AddOrdersSaleManager(VMSaleExCreate instance)
        {
            return listingPremiumRepository.AddOrdersSaleManager(instance);
        }

        public SeOrderReportResponse GetSEReportSale(decimal InvoiceId)
        {
            return listingPremiumRepository.GetSEReportSale(InvoiceId);
        }
        public VMSaleExCollect GetSePendingOrdersfor(decimal InvoiceId)
        {
            return listingPremiumRepository.GetSePendingOrdersfor(InvoiceId);
        }
        public UpdateSeOrderResponse CollectOrdersSaleManager(VMSaleExCollect instance)
        {
            return listingPremiumRepository.CollectOrdersSaleManager(instance);
        }

        //Teller Order List On Load
        public VLoadTellerPackagesResponse GetLoadTellers(VLoadTellerPackagesRequest request)
        {
            return listingPremiumRepository.GetLoadTellers(request);
        }

        public VMTellerDeposited GetTellerOrderNotDeposit(decimal InvoiceId)
        {
            return listingPremiumRepository.GetTellerOrderNotDeposit(InvoiceId);
        }

        public UpdateTellerOrderResponse UpdateTellerOrder(VMTellerDeposited instance)
        {
            return listingPremiumRepository.UpdateTellerOrder(instance);
        }

        // Call Center Orders
        public VLoadCallCenterPackagesResponse GetLoadCallCenter(VLoadCallCenterPackagesRequest request)
        {
            return listingPremiumRepository.GetLoadCallCenter(request);
        }

        public AddCallCenterOrderResponse AddOrdersCallCenter(VMCallCenterExCreate instance)
        {
            return listingPremiumRepository.AddOrdersCallCenter(instance);
        }

        public CallCenterOrderReportResponse GetCCOrderReport(decimal InvoiceId)
        {
            return listingPremiumRepository.GetCCOrderReport(InvoiceId);
        }

        // Order Transfer To Zone Manager
        public VLoadZoneManagerTransferResponse GetLoadZoneTransferOrders(VLoadZoneManagerTransferRequest request)
        {
            return listingPremiumRepository.GetLoadZoneTransferOrders(request);
        }

        public List<SpGetEmployeeByZoneManager> GetEmployeesByZoneManagerId(string ManagerId)
        {
            return listingPremiumRepository.GetEmployeesByZoneManagerId(ManagerId);
        }

        public VMZoneManagerTransfer GetNotAssignedOrders(decimal InvoiceId)
        {
            return listingPremiumRepository.GetNotAssignedOrders(InvoiceId);
        }

        public UpdateAssigningOrderResponse AssigningOrders(VMZoneManagerTransfer instance)
        {
            return listingPremiumRepository.AssigningOrders(instance);
        }

        // Order For Super Admin and Admin
        public VLoadSubAdminOrdersPackagesResponse GetLoadSubAdmin(VLoadAllOrderPackagesRequest request)
        {
            return listingPremiumRepository.GetLoadSubAdmin(request);
        }

        public AdminSubOrderReportResponse GetSubAdminReport(decimal InvoiceId)
        {
            return listingPremiumRepository.GetSubAdminReport(InvoiceId);
        }
        public VMSubAdminOrdersUpdate GetSubAdminOrderById(decimal InvoiceId)
        {
            return listingPremiumRepository.GetSubAdminOrderById(InvoiceId);
        }
        public UpdateSubOrderResponse UpdateSubOrders(SubAdminOrders instance)
        {
            return listingPremiumRepository.UpdateSubOrders(instance);
        }

        public List<AutoUpdateExpiredOrder> AutoUpdateExpiredOrders()
        {
            return listingPremiumRepository.AutoUpdateExpiredOrders();
        }
    }
}
