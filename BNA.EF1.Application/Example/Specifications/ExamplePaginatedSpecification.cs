using BNA.EF1.Application.Common.Specifications.Base;
using BNA.EF1.Domain.Example;
using System.Linq.Expressions;

namespace BNA.EF1.Application.Example.Specifications
{
    public sealed class ExamplePaginatedSpecification : BaseSpecification<ExampleClass>
    {
        public ExamplePaginatedSpecification(Expression<Func<ExampleClass, bool>> criteria, int pageIndex, int pageSize) : base(criteria)
        {
            ApplyPaging(pageIndex, pageSize);
        }
    }
}
