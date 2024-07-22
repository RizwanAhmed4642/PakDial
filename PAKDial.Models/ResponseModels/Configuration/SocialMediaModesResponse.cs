using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Domains.ResponseModels.Configuration
{
  public  class SocialMediaModesResponse
    {
        public int RowCount { get; set; }
        public IEnumerable<SocialMediaModes> SocialMediaModes { get; set; }
    }
}
