using Dapper;
using Microsoft.Data.SqlClient;
using SharpLibrary.DataAccess.Interfaces;

namespace SharpLibrary.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SqlConnection _connection;


        public GenericRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {typeof(T).Name}s";
            return await _connection.QueryAsync<T>(sql);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var sql = $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public async Task AddAsync(T entity)
        {
            var insertQuery = $"INSERT INTO {typeof(T).Name}s VALUES (@Entity)";
            await _connection.ExecuteAsync(insertQuery, new { Entity = entity });
        }

        public async Task UpdateAsync(T entity)
        {
            var updateQuery = $"UPDATE {typeof(T).Name}s SET ... WHERE Id = @Id";
            await _connection.ExecuteAsync(updateQuery, entity);
        }

        public async Task DeleteAsync(object id)
        {
            var deleteQuery = $"DELETE FROM {typeof(T).Name}s WHERE Id = @Id";
            await _connection.ExecuteAsync(deleteQuery, new { Id = id });
        }
    }
}

