using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;


namespace PAKDial.Implementation.CommonServices 
{
    public class CategoryTypesService : ICategoryTypesService
    {
        private readonly ICategoryTypesRepository categoryTypesRepository;

        public CategoryTypesService(ICategoryTypesRepository categoryTypesRepository)
        {
            this.categoryTypesRepository = categoryTypesRepository;
        }
        public CategoryTypes FindById(decimal Id)
        {
            return categoryTypesRepository.Find(Id);
        }

        public IEnumerable<CategoryTypes> GetAll()
        {
            return categoryTypesRepository.GetAll();
        }
    }
}
