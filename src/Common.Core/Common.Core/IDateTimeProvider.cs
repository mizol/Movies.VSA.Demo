namespace Common.Core
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }
    }
}
