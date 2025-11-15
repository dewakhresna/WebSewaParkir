using KandangMobil.DBContext;
using KandangMobil.Interfaces;
using Dapper;
using Models.Master;

namespace KandangMobil.Repositories
{
    public class MasterKendaraanRepository : IMasterKendaraan
    {
        private readonly DapperDbContext _DapperDbContext;

        public MasterKendaraanRepository(DapperDbContext dapperDbContext)
        {
            _DapperDbContext = dapperDbContext;
        }

        public async Task<IEnumerable<MasterKendaraanModel>> Get()
        {
            var sql = $@"SELECT *
                            FROM
                               MasterKendaraan";

            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryAsync<MasterKendaraanModel>(sql);
        }
        public async Task<MasterKendaraanModel> Find(Guid uid)
        {
            var sql = $@"SELECT *
                            FROM
                               MasterKendaraan
                            WHERE
                              [Id]=@uid";

            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterKendaraanModel>(sql, new { uid });
        }
        public async Task<MasterKendaraanModel> Add(MasterKendaraanModel model)
        {
            var sql = $@"INSERT INTO MasterKendaraan
                                (Merek,
                                 Tipe)
                                VALUES
                                (@Merek,
                                 @Tipe)";

            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
        public async Task<MasterKendaraanModel> Update(MasterKendaraanModel model)
        {
            var sql = $@"UPDATE MasterKendaraan
                           SET [Merek] = @Merek,
                               [Tipe] = @Tipe
                          WHERE
                              Id=@Id";

            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
        public async Task<MasterKendaraanModel> Remove(MasterKendaraanModel model)
        {
            var sql = $@"
                        DELETE FROM
                            MasterKendaraan
                        WHERE
                            [Id]=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
    }
}
