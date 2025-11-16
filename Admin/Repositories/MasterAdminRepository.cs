using KandangMobil.DBContext;
using Dapper;
using KandangMobil.Interfaces;
using Models.Master;

namespace KandangMobil.Repositories
{
    public class MasterAdminRepository : IMasterAdmin
    {
        private readonly DapperDbContext _DapperDbContext;
        public MasterAdminRepository(DapperDbContext dapperDbContext)
        {
            _DapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<MasterAdminModel>> Get()
        {
            var sql = "SELECT * FROM Admins";

            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryAsync<MasterAdminModel>(sql);
        }
        public async Task<MasterAdminModel?> Login(string email)
        {
            var sql = "SELECT * FROM Admins WHERE Email = @Email";

            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterAdminModel>(sql, new { Email = email });
        }
        public async Task<MasterAdminModel> Find(int Id)
        {
            var sql = "SELECT * FROM Admins WHERE Id = @Id";
            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterAdminModel>(sql, new { Id });
        }
        public async Task<MasterAdminModel> UpdateProfile(MasterAdminModel model)
        {
            var sql = $@"UPDATE Admins
                           SET [Name] = @Name,
                               [Email] = @Email,
                               [Photo] = @Photo,
                          WHERE
                              Id=@Id";

            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
        public async Task<MasterAdminModel> UpdatePassword(MasterAdminModel model)
        {
            var sql = $@"UPDATE Admins
                           SET [Password] = @Password,
                          WHERE
                              Id=@Id";

            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
    }
}
