using FluentValidation;

namespace OrdersDemo.Api.Models;

public class OrderUpdateDtoValidator1 : AbstractValidator<OrderUpdateDto>
{
    public OrderUpdateDtoValidator1()
    {
        RuleFor(x => x.City).NotEmpty();//.GreaterThan(0);
                                        // RuleFor(x => x.Street)//.Validate();
    }
}

