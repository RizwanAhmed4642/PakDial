using PAKDial.Domains.DomainModels;
using PAKDial.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Interfaces.Repository
{
    public interface IListingsBusinessTypesRepository:IBaseRepository<ListingsBusinessTypes,decimal>
    {
        bool CheckExistance(ListingsBusinessTypes instance);
        bool BusinessTypeExistance(decimal BusinessId);
        List<ListingsBusinessTypes> GetByListingsId(decimal Id);


    }
}
