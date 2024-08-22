using BNA.EF1.Application.Common.Interfaces;
using BNA.EF1.Application.Common.Pagination;
using BNA.EF1.Application.Common.Specifications;
using BNA.EF1.Domain.Common;
using BNA.EF1.Domain.Common.Interfaces;
using BNA.EF1.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BNA.EF1.Infrastructure.Repositories
{
    public sealed class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity is IAuditableEntity)
                    UpdateAuditableEntity((IAuditableEntity)entity, isCreational: true);

                await _dbSet.AddAsync(entity).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> BulkDeleteAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default)
        {
            return SpecificationEvaluator<T>
                .GetQuery(_dbSet, specification)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public Task<int> BulkUpdateAsync(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls, ISpecification<T>? specification = null, CancellationToken cancellationToken = default)
        {
            return SpecificationEvaluator<T>
                .GetQuery(_dbSet, specification)
                .ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entityToRemove = await _dbSet.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

                if (entityToRemove == null)
                    throw new Exception("The entity to remove does not exist");

                _dbSet.Remove(entityToRemove);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbSet, specification).ToListAsync(cancellationToken);
        }

        public Task GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbSet, specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PaginatedList<T>> GetPaginatedAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default)
        {
            try
            {
                if (specification != null && !specification.IsPagingEnabled)
                    throw new Exception("No paging was specified");

                int totalCount;

                var query = SpecificationEvaluator<T>.GetQuery(_dbSet, specification, out totalCount);

                var items = totalCount > 0 ? await query.ToListAsync(cancellationToken).ConfigureAwait(false) : null;

                var paginatedList = new PaginatedList<T>(totalCount, specification!.PageIndex, specification.PageSize, items);

                return paginatedList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity is IAuditableEntity)
                    UpdateAuditableEntity((IAuditableEntity)entity);

                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateAuditableEntity(IAuditableEntity entity, bool isCreational = false)
        {
            if (isCreational)
                entity.CreateDate = DateTime.Now;
            else
                entity.UpdateDate = DateTime.Now;
        }
    }
}