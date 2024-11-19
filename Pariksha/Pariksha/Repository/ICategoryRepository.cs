using Pariksha.Entities;

namespace Pariksha.Repository
{
    public interface ICategoryRepository

    {      public Task<List<Categories>> GetAll();
        public Task<bool> AddCategory(Categories category);
        public Task<bool> UpdateCategory(Categories category);
        public Task<bool> DeleteCategory(int cat_id);
    }
}
