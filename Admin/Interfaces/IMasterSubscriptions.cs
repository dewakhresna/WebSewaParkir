using Models.Master;

namespace KandangMobil.Interfaces
{
    public interface IMasterSubscriptions
    {
        Task<IEnumerable<MasterSubscriptionsModel>> Get();
        Task<MasterSubscriptionsModel> Find(int Id);
        Task<List<MasterSubscriptionsModel>> FindByUser(int UserId);
        Task<MasterSubscriptionsModel> Add(MasterSubscriptionsModel model);
        Task<MasterSubscriptionsModel> Update(MasterSubscriptionsModel model);
        Task ConfirmTransaction(int paymentId, int status);
        Task<MasterSubscriptionsModel> Remove(MasterSubscriptionsModel model);
    }
}
