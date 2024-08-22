using BNA.EF1.Application.Common.Interfaces;
using System.Linq.Expressions;

namespace BNA.EF1.Application.Common.Specifications.Base
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        protected BaseSpecification() { }
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;
        protected virtual void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderBy) => OrderBy = orderBy;
        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescending) => OrderByDescending = orderByDescending;
        protected virtual void ApplyGoupBy(Expression<Func<T, object>> groupBy) => GroupBy = groupBy;
        protected virtual void ApplyPaging(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsPagingEnabled = true;
        }
    }
}
