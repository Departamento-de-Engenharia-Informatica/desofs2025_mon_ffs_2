using System.Linq.Expressions;

namespace AMAPP.API.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T objModel);
        Task AddAsync(T objModel);
        void AddRange(IEnumerable<T> objModel);
        T? GetById(int id);
        Task<T?> GetByIdAsync(int id);
        T? Get(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        int Count();
        Task<int> CountAsync();
        void Update(T objModel);
        Task UpdateAsync(T objModel);
        void Remove(T objModel);
        Task RemoveAsync(T objModel);
        void Dispose();
    }
}
