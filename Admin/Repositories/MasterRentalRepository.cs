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

        public async Task<MasterRentalModel> Add(MasterRentalModel model)
        {
            var sql = $@"
                INSERT INTO CarRentals (CustomerName, NoPolice, CarName, StartDate, EndDate)
                 VALUES (@CustomerName, @NoPolice, @CarName, @StartDate, @EndDate)";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<MasterRentalModel> Update(MasterRentalModel model)
        {
            var sql = $@"UPDATE CarRentals
                           SET [CustomerName] = @CustomerName,
                               [NoPolice] = @NoPolice,
                               [CarName] = @CarName,
                               [StartDate] = @StartDate,
                               [EndDate] = @EndDate
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
