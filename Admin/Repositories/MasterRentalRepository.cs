using KandangMobil.DBContext;
using Dapper;
using KandangMobil.Interfaces;
using Models.Master;

namespace KandangMobil.Repositories
{
    public class MasterRentalRepository : IMasterRental
    {
        private readonly DapperDbContext _DapperDbContext;
        public MasterRentalRepository(DapperDbContext dapperDbContext)
        {
            _DapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<MasterRentalModel>> Get()
        {
            var sql = "SELECT * FROM CarRentals";
            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryAsync<MasterRentalModel>(sql);
        }

        public async Task<MasterRentalModel> Find(int Id)
        {
            var sql = "SELECT * FROM CarRentals WHERE Id = @Id";
            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterRentalModel>(sql, new { Id });
        }
        public async Task<List<MasterRentalModel>> FindByUser(int UserId)
        {
            var sql = "SELECT * FROM CarRentals WHERE UserId = @UserId";
            using var connection = _DapperDbContext.CreateConnection();
            var result = await connection.QueryAsync<MasterRentalModel>(sql, new { UserId });
            return result.ToList();
        }

        public async Task<MasterRentalModel> Add(MasterRentalModel model)
        {
            var sql = $@"
                INSERT INTO CarRentals (NoPolice, IdKendaraan, UserId)
                 VALUES (@NoPolice, @IdKendaraan, @UserId)";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<MasterRentalModel> Update(MasterRentalModel model)
        {
            var sql = $@"UPDATE CarRentals
                           SET [NoPolice] = @NoPolice,
                               [IdKendaraan] = @IdKendaraan,
                               [UserId] = @UserId
                          WHERE
                              Id=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<MasterRentalModel> UpdateUser(MasterRentalModel model)
        {
            var sql = $@"UPDATE CarRentals
                           SET [NoPolice] = @NoPolice,
                               [IdKendaraan] = @IdKendaraan
                          WHERE
                              Id=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<MasterRentalModel> Remove(MasterRentalModel model)
        {
            var sql = $@"
                        DELETE FROM
                            CarRentals
                        WHERE
                            [Id]=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
    }
}
