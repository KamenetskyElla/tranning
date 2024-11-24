namespace OrderDemo.FunctionalTest.Builder;
public class DateTimeBuilder
{
    // public IDateTime Build() => Clock.Initialize(TestDateTimeProvider.Instance);

    private sealed class TestDateTimeProvider : IDateTime
    {
        public static TestDateTimeProvider Instance { get; } = new(new DateTime(2022, 1, 1, 0, 0, 0));

        public DateTime Now { get; }
        public DateTime UtcNow { get; }
        public DateTime Today { get; }

        private TestDateTimeProvider(DateTimeOffset dateTimeOffset)
        {
            Now = dateTimeOffset.DateTime;
            UtcNow = dateTimeOffset.UtcDateTime;
            Today = dateTimeOffset.DateTime.Date;
        }
    }
}
