using Models.Master;

namespace KandangMobil.Interfaces
{
    public interface IMasterRental
    {
        Task<IEnumerable<MasterRentalModel>> Get();
        Task<MasterRentalModel> Find(int Id);
        Task<List<MasterRentalModel>> FindByUser(int UserId);
        Task<MasterRentalModel> Add(MasterRentalModel model);
        Task<MasterRentalModel> Update(MasterRentalModel model);
        Task<MasterRentalModel> UpdateUser(MasterRentalModel model);
        Task<MasterRentalModel> Remove(MasterRentalModel model);
    }
}
