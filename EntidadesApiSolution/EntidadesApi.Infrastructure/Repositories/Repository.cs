using EntidadesApi.Domain.Interfaces;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace EntidadesApi.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly string _filePath;
        protected List<T> _entities;

        public Repository(string filePath)
        {
            _filePath = filePath;
            LoadData();
        }

        protected void LoadData()
        {
            if (!File.Exists(_filePath))
            {
                _entities = new List<T>();
                SaveData();
            }
            else
            {
                var json = File.ReadAllText(_filePath);
                _entities = string.IsNullOrEmpty(json)
                    ? new List<T>()
                    : JsonConvert.DeserializeObject<List<T>>(json);
            }
        }

        protected void SaveData()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonConvert.SerializeObject(_entities, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_entities.AsQueryable().Where(predicate).ToList());
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException($"Entity {typeof(T).Name} does not have an Id property.");

            return await Task.FromResult(_entities.FirstOrDefault(e =>
                (Guid)idProperty.GetValue(e) == id));
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException($"Entity {typeof(T).Name} does not have an Id property.");

            if ((Guid)idProperty.GetValue(entity) == Guid.Empty)
                idProperty.SetValue(entity, Guid.NewGuid());

            _entities.Add(entity);
            SaveData();
            return await Task.FromResult(entity);
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException($"Entity {typeof(T).Name} does not have an Id property.");

            var id = (Guid)idProperty.GetValue(entity);
            var index = _entities.FindIndex(e => (Guid)idProperty.GetValue(e) == id);

            if (index == -1)
                return await Task.FromResult(false);

            _entities[index] = entity;
            SaveData();
            return await Task.FromResult(true);
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException($"Entity {typeof(T).Name} does not have an Id property.");

            var entity = _entities.FirstOrDefault(e => (Guid)idProperty.GetValue(e) == id);
            if (entity == null)
                return await Task.FromResult(false);

            _entities.Remove(entity);
            SaveData();
            return await Task.FromResult(true);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await Task.FromResult(_entities.Count);

            return await Task.FromResult(_entities.AsQueryable().Where(predicate).Count());
        }

        public virtual async Task<IEnumerable<T>> GetPagedAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            IQueryable<T> query = _entities.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            return await Task.FromResult(
                query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList());
        }
    }
}
