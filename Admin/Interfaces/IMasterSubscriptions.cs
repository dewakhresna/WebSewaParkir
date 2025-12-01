using Models.Master;

namespace KandangMobil.Interfaces
{
    public interface IMasterSubscriptions
    {
        Task<IEnumerable<MasterSubscriptionsModel>> Get();
        Task<MasterSubscriptionsModel> Find(int Id);
        Task<MasterSubscriptionsModel> Add(MasterSubscriptionsModel model);
        Task<MasterSubscriptionsModel> Update(MasterSubscriptionsModel model);
        Task<MasterSubscriptionsModel> Remove(MasterSubscriptionsModel model);
    }
}
