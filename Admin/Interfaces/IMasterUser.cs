using Models.Master;

namespace KandangMobil.Interfaces
{
    public interface IMasterUser
    {
        Task<IEnumerable<MasterUserModel>> Get();
        Task<MasterUserModel> Find(int Id);
        Task<MasterUserModel> Add(MasterUserModel model);
        Task<MasterUserModel> Update(MasterUserModel model);
        Task<MasterUserModel> Remove(MasterUserModel model);
    }
}
