using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Domains.SqlViewModels;
using PAKDial.Domains.UserEndViewModel;
using PAKDial.Domains.ViewModels;

namespace PAKDial.Repository.BaseRepository
{
    public partial class PAKDialSolutionsContext
    {
        public DbQuery<VEmployees> VEmployees { get; set; }
        public DbQuery<VCustomers> VCustomers { get; set; }
        public DbQuery<VEmpAddress> VEmpAddress { get; set; }
        public DbQuery<VRouteControl> VRouteControl { get; set; }
        public DbQuery<VUserPermissions> VUserPermissions { get; set; }
        public DbQuery<VRolePermissions> VRolePermissions { get; set; }
        public DbQuery<VSystemUser> VSystemUser { get; set; }
        public DbQuery<VStateProvince> VStateProvince { get; set; }
        public DbQuery<VCity> VCity { get; set; }
        public DbQuery<VCityArea> VCityArea { get; set; }
        public DbQuery<VUserLoginRUMenu> VUserLoginRUMenu { get; set; }
        public DbQuery<VSubMenuCategory> VSubMenuCategory { get; set; }
        public DbQuery<VHomeSecMainMenuCat> VHomeSecMainMenuCat { get; set; }
        public DbQuery<VListingPackages> VListingPackages { get; set; }
        public DbQuery<VPackagePrices> VPackagePrices { get; set; }
        public DbQuery<VMainCategory> VMainCategory { get; set; }
        public DbQuery<VCompanyListings> VCompanyListings { get; set; }
        public DbQuery<VAssignListingPackages> VAssignListingPackages { get; set; }
        public DbQuery<VActiveZones> VActiveZones { get; set; }
        public DbQuery<VDesignation> VDesignation { get; set; }
        public DbQuery<VLoadSalesExectivePackages> VLoadSalesExectivePackages { get; set; }
        public DbQuery<VLoadSaleRptandCcRpt> VLoadSaleRptandCcRpt { get; set; }
        public DbQuery<VCombineRegions> VCombineRegions  { get; set; }
        public DbQuery<VActiveCompanyListing> VActiveCompanyListing { get; set; }
        public DbQuery<VLoadTellerOrdersDeposited> VLoadTellerOrdersDeposited { get; set; }
        public DbQuery<VLoadCallCenterOrdersPackages> VLoadCallCenterOrdersPackages { get; set; }
        public DbQuery<VLoadZoneManagerOrdersTransfer> VLoadZoneManagerOrdersTransfer { get; set; }
        public DbQuery<VLoadSubAdminOrdersPackages> VLoadSubAdminOrdersPackages { get; set; }
        public DbQuery<VLoadHomePopularService> VLoadHomePopularService { get; set; }
        public DbQuery<VHomeListingSearch> VHomeListingSearch { get; set; }
        public DbQuery<VClientLogins> VClientLogins { get; set; }
        public DbQuery<VSearchBehaviourResults> VSearchBehaviourResults { get; set; }
        public DbQuery<VCatAndCompanyListingsSearch> VCatAndCompanyListingsSearch { get; set; }
        public DbQuery<VSubCategoryKeywordsSearch> VSubCategoryKeywordsSearch { get; set; }
    }
}
