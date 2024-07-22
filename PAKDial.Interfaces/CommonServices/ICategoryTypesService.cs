using PAKDial.Domains.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.CommonServices
{
    public interface ICategoryTypesService
    {
        CategoryTypes FindById(decimal Id);
        IEnumerable<CategoryTypes> GetAll();

    }
}
