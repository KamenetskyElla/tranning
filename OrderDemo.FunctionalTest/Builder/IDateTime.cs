namespace OrderDemo.FunctionalTest.Builder;
internal interface IDateTime
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    DateTime Today { get; }
}
