using Pariksha.Entities;

namespace Pariksha.Repository
{
    public interface IAdminRepository
    {
        public Task<bool> AddAdmin(Admins admins);
        public Task<bool> UpdateAdmin(Admins admins);
        public Task<bool> DeleteAdmin(int admin_id);    
            Task <List<Admins>> GetAll();
    }
}
