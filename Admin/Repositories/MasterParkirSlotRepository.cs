using KandangMobil.DBContext;
using Dapper;
using KandangMobil.Interfaces;
using Models.Master;

namespace KandangMobil.Repositories
{
    public class MasterParkirSlotRepository : IMasterParkirSlot
    {
        private readonly DapperDbContext _DapperDbContext;
        public MasterParkirSlotRepository(DapperDbContext dapperDbContext)
        {
            _DapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<MasterParkirSlotModel>> Get()
        {
            var sql = "SELECT * FROM MasterParkirSlot";
            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryAsync<MasterParkirSlotModel>(sql);
        }

        public async Task<MasterParkirSlotModel> Find(int Id)
        {
            var sql = "SELECT * FROM MasterParkirSlot WHERE Id = @Id";
            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterParkirSlotModel>(sql, new { Id });
        }

        public async Task<MasterParkirSlotModel> Add(MasterParkirSlotModel model)
        {
            var sql = $@"
                INSERT INTO MasterParkirSlot (SlotNumber, IsOccupied, Status)
                 VALUES (@SlotNumber, @IsOccupied, @Status)";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<MasterParkirSlotModel> Update(MasterParkirSlotModel model)
        {
            var sql = $@"UPDATE MasterParkirSlot
                           SET [SlotNumber] = @SlotNumber,
                               [IsOccupied] = @IsOccupied,
                               [Status] = @Status
                          WHERE
                              Id=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
        public async Task<MasterParkirSlotModel> Remove(MasterParkirSlotModel model)
        {
            var sql = $@"
                        DELETE FROM
                            MasterParkirSlot
                        WHERE
                            [Id]=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
    }
}
