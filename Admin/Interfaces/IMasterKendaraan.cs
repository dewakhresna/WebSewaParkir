using Models.Master;

namespace KandangMobil.Interfaces
{
    public interface IMasterKendaraan
    {
        Task<IEnumerable<MasterKendaraanModel>> Get();
        Task<MasterKendaraanModel> Find(Guid uid);
        Task<MasterKendaraanModel> Add(MasterKendaraanModel model);
        Task<MasterKendaraanModel> Update(MasterKendaraanModel model);
        Task<MasterKendaraanModel> Remove(MasterKendaraanModel model);
    }
}
