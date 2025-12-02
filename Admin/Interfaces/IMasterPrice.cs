using Models.Master;

namespace KandangMobil.Interfaces
{
    public interface IMasterPrice
    {
        Task<IEnumerable<MasterPriceModel>> Get();
        Task<MasterPriceModel> Find(int Id);
        Task<MasterPriceModel> Add(MasterPriceModel model);
        Task<MasterPriceModel> Update(MasterPriceModel model);
        Task<MasterPriceModel> Remove(MasterPriceModel model);
    }
}
