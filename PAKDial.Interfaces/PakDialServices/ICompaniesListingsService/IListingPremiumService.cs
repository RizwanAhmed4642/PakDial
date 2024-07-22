using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ResponseModels.SalePersonsOrders;
using PAKDial.Domains.StoreProcedureModel;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.PakDialServices.ICompaniesListingsService
{
    public interface IListingPremiumService
    {
        decimal Add(ListingPremium listingPremium);
        decimal Update(ListingPremium listingPremium);
        decimal Delete(decimal Id);
        ListingPremium FindById(decimal Id);
        IEnumerable<ListingPremium> GetAll();
        AssignListingPackageResponse Get(AssignListingPackageRequestModel request);
        List<ListingPremium> GetByListingId(decimal ListingId);
        List<ListingPremium> GetListingPremiumModeId(decimal ModeId);
        List<ListingPremium> GetListingPremium(decimal PackageId);
        List<ListingPremium> GetListingPremium(decimal ModeId, decimal PackageId);
        ListingPremium GetListingPremium(decimal ModeId, decimal PackageId, decimal ListingId);
        GetPackagesandModesResponse GetPakagesModes();

        // Sales Executive //Sale Manager Packages List On Load
        GetModeandPackagesResponse GetModeandPackagesResponses();
        GetModeandPackagesResponse GetPaymentsModeResponses();
        VLoadSalesExectivePackagesResponse GetLoadSalesManagers(VLoadSalesExectivePackagesRequest request);
        AddSeOrderResponse AddOrdersSaleManager(VMSaleExCreate instance);
        SeOrderReportResponse GetSEReportSale(decimal InvoiceId);
        VMSaleExCollect GetSePendingOrdersfor(decimal InvoiceId);
        UpdateSeOrderResponse CollectOrdersSaleManager(VMSaleExCollect instance);
        //Teller Order List On Load
        VLoadTellerPackagesResponse GetLoadTellers(VLoadTellerPackagesRequest request);
        VMTellerDeposited GetTellerOrderNotDeposit(decimal InvoiceId);
        UpdateTellerOrderResponse UpdateTellerOrder(VMTellerDeposited instance);

        // Call Center List On Load
        VLoadCallCenterPackagesResponse GetLoadCallCenter(VLoadCallCenterPackagesRequest request);
        AddCallCenterOrderResponse AddOrdersCallCenter(VMCallCenterExCreate instance);
        CallCenterOrderReportResponse GetCCOrderReport(decimal InvoiceId);

        // Order Transfer To Zone Manager
        VLoadZoneManagerTransferResponse GetLoadZoneTransferOrders(VLoadZoneManagerTransferRequest request);
        List<SpGetEmployeeByZoneManager> GetEmployeesByZoneManagerId(string ManagerId);
        VMZoneManagerTransfer GetNotAssignedOrders(decimal InvoiceId);
        UpdateAssigningOrderResponse AssigningOrders(VMZoneManagerTransfer instance);

        // Order For Super Admin and Admin
        VLoadSubAdminOrdersPackagesResponse GetLoadSubAdmin(VLoadAllOrderPackagesRequest request);
        AdminSubOrderReportResponse GetSubAdminReport(decimal InvoiceId);
        VMSubAdminOrdersUpdate GetSubAdminOrderById(decimal InvoiceId);
        UpdateSubOrderResponse UpdateSubOrders(SubAdminOrders instance);

        List<AutoUpdateExpiredOrder> AutoUpdateExpiredOrders();
    }
}
