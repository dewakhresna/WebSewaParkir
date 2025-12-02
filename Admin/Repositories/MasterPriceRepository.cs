using KandangMobil.DBContext;
using Dapper;
using KandangMobil.Interfaces;
using Models.Master;

namespace KandangMobil.Repositories
{
    public class MasterPriceRepository : IMasterPrice
    {
        private readonly DapperDbContext _DapperDbContext;
        public MasterPriceRepository(DapperDbContext dapperDbContext)
        {
            _DapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<MasterPriceModel>> Get()
        {
            var sql = "SELECT * FROM MasterPrice";
            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryAsync<MasterPriceModel>(sql);
        }

        public async Task<MasterPriceModel> Find(int Id)
        {
            var sql = "SELECT * FROM MasterPrice WHERE Id = @Id";
            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterPriceModel>(sql, new { Id });
        }

        public async Task<MasterPriceModel> Add(MasterPriceModel model)
        {
            var sql = $@"
                INSERT INTO MasterPrice (Price, Duration, Description)
                 VALUES (@Price, @Duration, @Description)";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<MasterPriceModel> Update(MasterPriceModel model)
        {
            var sql = $@"UPDATE MasterPrice
                           SET [Price] = @Price,
                               [Duration] = @Duration,
                               [Description] = @Description,
                          WHERE
                              Id=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<MasterPriceModel> Remove(MasterPriceModel model)
        {
            var sql = $@"
                        DELETE FROM
                            MasterPrice
                        WHERE
                            [Id]=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
    }
}
