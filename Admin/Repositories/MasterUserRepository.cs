using Admin.DBContext;
using Dapper;
using KandangMobil.Interfaces;
using Models.Master;

namespace KandangMobil.Repositories
{
    public class MasterUserRepository : IMasterUser
    {
        private readonly DapperDbContext _DapperDbContext;
        public MasterUserRepository(DapperDbContext dapperDbContext)
        {
            _DapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<MasterUserModel>> Get()
        {
            var sql = "SELECT * FROM Users";

            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryAsync<MasterUserModel>(sql);
        }
        public async Task<MasterUserModel> Find(int Id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";

            using var connection = _DapperDbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterUserModel>(sql, new { Id = Id });
        }
        public async Task<MasterUserModel> Add(MasterUserModel model)
        {
            var sql = $@"
                INSERT INTO Users (Name, Email, Telp, Password)
                 VALUES (@Name, @Email, @Telp, @Password)";

            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
        public async Task<MasterUserModel> Update(MasterUserModel model)
        {
            var sql = $@"UPDATE Users
                           SET [Name] = @Name,
                               [Email] = @Email,
                               [Telp] = @Telp,
                               [Password] = @Password
                          WHERE
                              Id=@Id";

            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }
        public async Task<MasterUserModel> Remove(MasterUserModel model)
        {
            var sql = $@"
                        DELETE FROM
                            Users
                        WHERE
                            [Id]=@Id";
            using var connection = _DapperDbContext.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

    }
}
