using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.Mapper.CompanyListingMappers
{
  public static class CompanyListingProfileMapper
    {
        #region UpdateCompanyListingPro

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Destination"></param>
        /// <returns></returns>
        public static CompanyListingProfile MapCompanyListingProfile(CompanyListingProfile Source , CompanyListingProfile Destination)
        {
            Destination.Id = Source.Id;
            Destination.YearEstablished = Source.YearEstablished;
            Destination.AnnualTurnOver = Source.AnnualTurnOver;
            Destination.NumberofEmployees = Source.NumberofEmployees;
            Destination.ProfessionalAssociation = Source.ProfessionalAssociation;
            Destination.Certification = Source.Certification;
            Destination.BriefAbout = Source.BriefAbout;
            Destination.LocationOverview = Source.LocationOverview;
            Destination.ProductAndServices = Source.ProductAndServices;
            Destination.UpdatedBy = Source.UpdatedBy;
            Destination.UpdatedDate = Source.UpdatedDate;
            
            return Destination;
        }

        #endregion

    }
}
