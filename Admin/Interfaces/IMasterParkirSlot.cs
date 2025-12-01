using Models.Master;

namespace KandangMobil.Interfaces
{
    public interface IMasterParkirSlot
    {
        Task<IEnumerable<MasterParkirSlotModel>> Get();
        Task<MasterParkirSlotModel> Find(int Id);
        Task<MasterParkirSlotModel> Update(MasterParkirSlotModel model);
        Task<MasterParkirSlotModel> Remove(MasterParkirSlotModel model);
    }
}
