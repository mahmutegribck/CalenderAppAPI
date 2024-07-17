namespace CalenderApp.Application.Exceptions
{
    public class FluentValidationException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public FluentValidationException(IReadOnlyDictionary<string, string[]> errors) : base("Birden Fazla Hata Oluştu.") => Errors = errors;
    }
}
