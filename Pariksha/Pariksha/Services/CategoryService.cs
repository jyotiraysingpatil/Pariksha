using Pariksha.Entities;
using Pariksha.Repository;

namespace Pariksha.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repository;
        public CategoryService(ICategoryRepository repository)
        {
            this.repository = repository;
        }




        public async Task<bool> AddCategory(Categories category)=> await repository.AddCategory(category); 
        public async Task<bool> DeleteCategory(int cat_id)=> await repository.DeleteCategory(cat_id);   
        

        public async Task<List<Categories>> GetAll()=> await repository.GetAll();
        

        public async Task<bool> UpdateCategory(Categories category)=> await repository.UpdateCategory(category);    
        
    }
}
