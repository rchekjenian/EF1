using ErrorOr;


namespace BNA.EF1.Domain.Example.Errors
{
    public static class ExampleClassErrors
    {
        public static Error NullOrEmptyExampleField = Error.Validation(code: "ExampleClass.NullOrEmptyExampleField",
            description: "The ExampleFiled provided is null or empty");

        public static Error InvalidInternalInfo = Error.Validation(code: "ExampleClass.InvalidInternalInfo",
            description: "The internal info should be Internal");
    }
}
