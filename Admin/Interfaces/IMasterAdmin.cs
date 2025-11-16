using Models.Master;

namespace KandangMobil.Interfaces
{
    public interface IMasterAdmin
    {
        Task<IEnumerable<MasterAdminModel>> Get();
        Task<MasterAdminModel> Login(string email);
        Task<MasterAdminModel> Find(int Id);
        Task<MasterAdminModel> UpdateProfile(MasterAdminModel model);
        Task<MasterAdminModel> UpdatePassword(MasterAdminModel model);
    }
}
