using FluentValidation;

namespace OrdersDemo.Api.Models;

public class OrderItemCreateDtoValidator : AbstractValidator<OrderItemCreateDto>
{
    public OrderItemCreateDtoValidator()
    {
        RuleFor(x=>x.Name).NotEmpty();
        RuleFor(x=>x.Price).GreaterThan(0);
    }
}
