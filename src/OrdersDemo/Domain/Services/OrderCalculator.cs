using OrdersDemo.Domain.Contracts;
using System.Reflection;

namespace OrdersDemo.Domain.Services;

    class Test
{
    public Test()
    {
        var order = OrderCalculator.Instance;
    }
}

public class OrderCalculator : IOrderCalculator
{
    private static readonly Lazy<List<IPriceCalculator>> _list = new(FindAllCalculators, LazyThreadSafetyMode.ExecutionAndPublication);

    public static readonly OrderCalculator Instance = new();
    private readonly IEnumerable<IPriceCalculator> _calculators;

    private OrderCalculator() 
    {
        _calculators = _list.Value;
    } 

    public OrderCalculator(IEnumerable<IPriceCalculator> calculators)
    {
        _calculators = calculators;
    }

    public decimal CalculateDiscount(Order order)
    {
        var calculator = _calculators.FirstOrDefault(x => x.Type == order.Customer.Type);

        if (calculator == null)
        {
            throw new NotSupportedException($"There is no calculator for type: {order.Customer.Type}");
        }

        return calculator.CalculateDiscount(order);
    }

    private static List<IPriceCalculator> FindAllCalculators()
    {
        var calculatorInterface = typeof(IPriceCalculator);
        return calculatorInterface.Assembly
            .GetTypes()
            .Where(type => calculatorInterface != type && calculatorInterface.IsAssignableFrom(type))
            .SelectMany(type => type.GetFields(BindingFlags.Public | BindingFlags.Static)
                            .Where(fi => type.IsAssignableFrom(fi.FieldType))
                            .Select(fi => (IPriceCalculator)fi.GetValue(null)!))
            .ToList();
    }
}
