using BNA.EF1.Application.Common.Interfaces;
using BNA.EF1.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BNA.EF1.Application.Common.Specifications
{
    public static class SpecificationEvaluator<T> where T : Entity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> source, ISpecification<T>? specification)
        {
            return GetQuery(source, specification, false, out int totalCount);
        }
        public static IQueryable<T> GetQuery(IQueryable<T> source, ISpecification<T>? specification, out int totalCount)
        {
            return GetQuery(source, specification, true, out totalCount);
        }

        private static IQueryable<T> GetQuery(IQueryable<T> source, ISpecification<T>? specification, bool processTotalCount, out int totalCount)
        {
            if (source == null)
                throw new ArgumentNullException("No data set was provided");

            totalCount = 0;

            var query = source;

            if (specification != null)
            {
                query = specification.Criteria != null ? query.Where(specification.Criteria) : query;

                query = specification.Includes.Aggregate(query,
                                        (current, include) => current.Include(include));

                if (specification.OrderBy != null)
                    query = query.OrderBy(specification.OrderBy);
                else if (specification.OrderByDescending != null)
                    query = query.OrderByDescending(specification.OrderByDescending);

                query = specification.GroupBy != null ? query.GroupBy(specification.GroupBy).SelectMany(x => x) : query;

                if (processTotalCount)
                {
                    totalCount = query.Count();
                }

                if (specification.IsPagingEnabled)
                    query = query.Skip(specification.PageSize * (specification.PageIndex - 1))
                                 .Take(specification.PageSize);
            }

            return query;
        }
    }
}
