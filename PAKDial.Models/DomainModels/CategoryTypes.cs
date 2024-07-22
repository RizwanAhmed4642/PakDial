using System;
using System.Collections.Generic;

namespace PAKDial.Domains.DomainModels
{
    public partial class CategoryTypes
    {
        public CategoryTypes()
        {
            MainMenuCategory = new HashSet<MainMenuCategory>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }

        public ICollection<MainMenuCategory> MainMenuCategory { get; set; }
    }
}
