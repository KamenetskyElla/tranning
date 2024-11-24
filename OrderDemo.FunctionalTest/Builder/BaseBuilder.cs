namespace OrderDemo.FunctionalTest.Builder;

public abstract class BaseBuilder<TBuilder, T>
    where TBuilder : BaseBuilder<TBuilder, T>, new()
    where T : class
{
    // protected readonly IDateTime _dateTime = new DateTimeBuilder().Build();
    //protected readonly Fixture _fixture = new();

    protected abstract T BuildObject();

    public T Build()
    {
        var entity = BuildObject();
        return entity;
    }

    public T Build(Action<TBuilder> action)
    {
        action((TBuilder)this);
        var entity = BuildObject();
        return entity;
    }

    public static T Create()
    {
        var builder = new TBuilder();
        var entity = builder.Build();
        return entity;
    }

    public static T Create(Action<TBuilder> action)
    {
        var builder = new TBuilder();
        var entity = builder.Build(action);
        return entity;
    }
}
