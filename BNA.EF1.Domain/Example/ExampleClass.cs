using BNA.EF1.Domain.Common;
using BNA.EF1.Domain.Example.Errors;
using ErrorOr;

namespace BNA.EF1.Domain.Example
{
    public sealed class ExampleClass : Entity
    {
#pragma warning disable CS8618
        private ExampleClass() { }
#pragma warning restore CS8618

        private ExampleClass(string exampleField,
            string internalInfo,
            Guid? id) : base(id)
        {
            ExampleField = exampleField;
            InternalInfo = internalInfo;
        }

        public static ErrorOr<ExampleClass> Create(string exampleField,
            string internalInfo,
            Guid? id = null)
        {
            if (string.IsNullOrEmpty(exampleField))
                return ExampleClassErrors.NullOrEmptyExampleField;

            if (internalInfo != "Internal")
                return ExampleClassErrors.InvalidInternalInfo;

            return new ExampleClass(exampleField, internalInfo, id);
        }

        public string ExampleField { get; }

        public string InternalInfo { get; }
    }
}
