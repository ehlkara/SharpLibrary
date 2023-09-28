using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using Dapper;
using SharpLibrary.DataAccess.Interfaces;
using SharpLibrary.Models.Entities;

namespace SharpLibrary.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly IDbConnection _connection;
        private readonly string _tableName;

        public GenericRepository(IDbConnection connection)
        {
            _connection = connection;
            _tableName = GetTableName();
        }

        private string GetTableName()
        {
            var tableAttr = typeof(T)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .SingleOrDefault() as TableAttribute;
            return tableAttr?.Name ?? typeof(T).Name;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM {_tableName} WHERE DeletedDate IS NULL AND IsActive = 1";
            return await _connection.QueryAsync<T>(query);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var query = $"SELECT * FROM {_tableName} WHERE Id = @Id AND DeletedDate IS NULL AND IsActive = 1";
            return await _connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.UtcNow;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "Id" && !Attribute.IsDefined(p, typeof(NotMappedAttribute)));

            var columnNames = string.Join(", ", properties.Select(p => $"[{p.Name}]"));
            var paramNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            var query = $@"INSERT INTO {_tableName} ({columnNames}) VALUES ({paramNames});";

            await _connection.ExecuteAsync(query, entity);
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "Id" && !Attribute.IsDefined(p, typeof(NotMappedAttribute)));

            var setClause = string.Join(", ", properties.Select(p => $"[{p.Name}] = @{p.Name}"));

            var query = $@"UPDATE {_tableName} SET {setClause} WHERE Id = @Id";

            await _connection.ExecuteAsync(query, entity);
        }

        public async Task DeleteAsync(object id)
        {
            var query = $@"UPDATE {_tableName} 
                        SET DeletedDate = @DeletedDate, IsActive = 0 
                        WHERE Id = @Id";
            await _connection.ExecuteAsync(query, new { Id = id, DeletedDate = DateTime.UtcNow });
        }
    }
}

