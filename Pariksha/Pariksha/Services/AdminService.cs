using Pariksha.Repository;
using Pariksha.Entities;
namespace Pariksha.Services
{
    public class AdminService :IAdminService
    {
        private readonly IAdminRepository adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository; 
        }
        public async Task<List<Admins>> GetAll()=> await adminRepository.GetAll();  
        public async Task<bool> AddAdmin(Admins admin) => await adminRepository.AddAdmin(admin);    
        public async Task<bool> UpdateAdmin(Admins admin)=>await adminRepository.UpdateAdmin(admin);    
        public async Task<bool> DeleteAdmin(int admin_id)=>await adminRepository.DeleteAdmin(admin_id);


    }
}
