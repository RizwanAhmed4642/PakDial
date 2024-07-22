using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class HomeSecMainMenuCat
    {
        public decimal Id { get; set; }
        public decimal MainMenuCatId { get; set; }
        public decimal HomeSecCatId { get; set; }

        public HomeSectionCategory HomeSecCat { get; set; }
        public MainMenuCategory MainMenuCat { get; set; }
    }
}
