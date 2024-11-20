using Microsoft.AspNetCore.Mvc;
using OrdersDemo.Api.Handlers;
using OrdersDemo.Api.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrdersDemo.Api;

[ApiController]
public class OrderController : ControllerBase
{
    [HttpGet("/orders")]
    [SwaggerOperation(Summary = "List all orders.", Tags = new[] { "Order" })]
    public async Task<ActionResult<List<OrderListDto>>> GetAll(
        [FromServices] OrderGetAllHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(cancellationToken);

        return Ok(result);
    }

    [HttpGet("/orders/{id}")]
    [SwaggerOperation(Summary = "Get an order details.", Tags = new[] { "Order" })]
    public async Task<ActionResult<OrderDto>> Get(int id,
        [FromServices] OrderGetHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost("/orders")]
    [SwaggerOperation(Summary = "Create an order.", Tags = new[] { "Order" })]
    public async Task<ActionResult<OrderDto>> Create(OrderCreateDto createDto,
        [FromServices] OrderCreateHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(createDto, cancellationToken);

        return Ok(result);
    }

    [HttpPut("/orders/{id}")]
    [SwaggerOperation(Summary = "Update order.", Tags = new[] { "Order" })]
    public async Task<ActionResult<OrderDto>> Update(int id, OrderUpdateDto updateDto,
        [FromServices] OrderUpdateHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(id, updateDto, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("/orders/{id}")]
    [SwaggerOperation(Summary = "Delete order.", Tags = new[] { "Order" })]
    public async Task<ActionResult<OrderDto>> Delete(int id,
        [FromServices] OrderDeleteHandler handler,
        CancellationToken cancellationToken)
    {
        await handler.HandleAsync(id, cancellationToken);

        return Ok();
    }

    [HttpPost("/orders/{id}")]
    [SwaggerOperation(Summary = "Complete order.", Tags = new[] { "Order" })]
    public async Task<ActionResult<OrderDto>> Complete(int id,
        [FromServices] OrderCompleteHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost("/orders/{id}/items")]
    [SwaggerOperation(Summary = "Add item to order.", Tags = new[] { "Order Item" })]
    public async Task<ActionResult<OrderDto>> AddItem(int id, OrderItemCreateDto createDto,
        [FromServices] OrderItemCreateHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(id, createDto, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("/orders/{id}/items/{itemId}")]
    [SwaggerOperation(Summary = "Delete item from order.", Tags = new[] { "Order Item" })]
    public async Task<ActionResult> DeleteItem(int id, int itemId,
        [FromServices] OrderItemDeleteHandler handler,
        CancellationToken cancellationToken)
    {
        await handler.HandleAsync(id, itemId, cancellationToken);

        return Ok();
    }
}
