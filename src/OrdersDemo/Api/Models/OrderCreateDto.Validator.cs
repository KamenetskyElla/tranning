using FluentValidation;

namespace OrdersDemo.Api.Models;

public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.PostalCode).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();

        RuleFor(x => x.Type).InclusiveBetween(1, 4);

        RuleForEach(x => x.Items).SetValidator(new OrderItemCreateDtoValidator());
    }
}
