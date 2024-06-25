namespace Common.Core
{
    public record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new(string.Empty, "The value can not be null.");

        /// <summary>
        /// It define an implicit conversion from Error to Result.
        /// So as a result Error.NullValue will automaticly will be converted into Result.Failure()
        /// </summary>
        /// <param name="error"></param>
        public static implicit operator Result(Error error) => Result.Failure(error);

        public Result ToResult() => Result.Failure(this);
    }
}
