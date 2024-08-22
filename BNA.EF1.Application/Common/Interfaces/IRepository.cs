using BNA.EF1.Application.Common.Pagination;
using BNA.EF1.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BNA.EF1.Application.Common.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        public Task<T?> GetAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default);

        public Task<IEnumerable<T>> GetAllAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default);

        public Task<PaginatedList<T>> GetPaginatedAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default);

        public Task AddAsync(T entity, CancellationToken cancellationToken = default);

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        public Task<int> BulkUpdateAsync(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls, ISpecification<T>? specification = null, CancellationToken cancellationToken = default);

        public Task<int> BulkDeleteAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default);
    
    }
}
