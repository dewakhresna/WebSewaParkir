using KandangMobil.DBContext;
using Dapper;
using KandangMobil.Interfaces;
using Models.Master;

namespace KandangMobil.Repositories
{
    public class MasterSubscriptionsRepository : IMasterSubscriptions
    {
        private readonly DapperDbContext _DapperDbContext;
        public MasterSubscriptionsRepository(DapperDbContext dapperDbContext)
        {
            _DapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<MasterSubscriptionsModel>> Get()
        {
            var sql = "SELECT * FROM MasterSubscriptions";
            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryAsync<MasterSubscriptionsModel>(sql);
        }

        public async Task<MasterSubscriptionsModel> Find(int Id)
        {
            var sql = "SELECT * FROM MasterSubscriptions WHERE UserId = @Id";
            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterSubscriptionsModel>(sql, new { Id });
        }

        public async Task<MasterSubscriptionsModel> Add(MasterSubscriptionsModel model)
        {
            var sql = $@"
                INSERT INTO MasterSubscriptions (UserId, CarRentalId, ParkirSlotId, Time, EndDate, LastPaymentDate, Price, PaymentMethod, PaymentProof, Status)
                 VALUES (@UserId, @CarRentalId, @ParkirSlotId, @Time, @EndDate, @LastPaymentDate, @Price, @PaymentMethod, @PaymentProof, @Status)";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<MasterSubscriptionsModel> Update(MasterSubscriptionsModel model)
        {
            var sql = $@"UPDATE MasterSubscriptions
                           SET [UserId] = @UserId,
                               [CarRentalId] = @CarRentalId,
                               [ParkirSlotId] = @ParkirSlotId,
                               [Time] = @Time,
                               [EndDate] = @EndDate, 
                               [LastPaymentDate] = @LastPaymentDate,
                               [Price] = @Price,
                               [PaymentMethod] = @PaymentMethod,
                               [PaymentProof] = @PaymentProof, 
                               [Status] = @Status
                          WHERE
                              Id=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<MasterSubscriptionsModel> Remove(MasterSubscriptionsModel model)
        {
            var sql = $@"
                        DELETE FROM
                            MasterSubscriptions
                        WHERE
                            [Id]=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
    }
}
