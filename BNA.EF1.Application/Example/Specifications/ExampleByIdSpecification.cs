using BNA.EF1.Application.Common.Specifications.Base;
using BNA.EF1.Domain.Example;

namespace BNA.EF1.Application.Example.Specifications
{
    public sealed class ExampleByIdSpecification : BaseSpecification<ExampleClass>
    {
        public ExampleByIdSpecification(Guid id) : base(x => x.Id == id) { }
    }
}
