using FluentAssertions;
using OrderDemo.FunctionalTest.Fixtures;
using OrdersDemo.Api.Models;
using OrdersDemo.Repository;
using OrdersDemo.Setup;
using System.Net;

namespace OrderDemo.FunctionalTest.Test;
[Collection("SharedCollection")]
public class OrderCreateTest : FunctionalTest // Change FunctionalTest to FunctionalTestBase
{
    private readonly string _apiUrl = "/orders";

    public OrderCreateTest(TestFactory testFactory)
        : base(testFactory)
    {
    }

    [Fact]
    public async Task ReturnsBadRequestWithValidationErrors_GivenZeroNonAllowedMasterEntity()
    {
        var nonAllowedMasterEntityIds = new List<int> { 0 };

        var request = new OrderDto
        {
            Street = "Test",
            City = "Test",
            //Name = _fixture.CreateTranslations(),           
        };

        var response = await _client.PostAsync(_apiUrl, request.ToStringContent());

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var result = await response.FromJsonAsync<ErrorResponseDto>();

        result.Should().NotBeNull();
        Assert.NotNull(result);
        result.Status.Should().Be((int)HttpStatusCode.BadRequest);
    }
    //[Fact]
    //public async Task ReturnsBadRequestWithValidationErrors_GivenNotUniqueNonAllowedMasterEntity()
    //{
    //    var nonAllowedMasterEntityIds = new List<int> { 1, 1 };

    //    var request = new SkillCreateDto
    //    {
    //        Name = _fixture.CreateTranslations(),
    //        NonAllowedMasterEntityIds = nonAllowedMasterEntityIds
    //    };

    //    var response = await _client.PostAsync(_apiUrl, request.ToStringContent());

    //    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    var result = await response.FromJsonAsync<ErrorResponseDto>();

    //    result.Should().NotBeNull();
    //    Assert.NotNull(result);
    //    result.Status.Should().Be((int)HttpStatusCode.BadRequest);
    //    result.Errors.Should().ContainKey(nameof(SkillCreateDto.NonAllowedMasterEntityIds));
    //}

    [Fact]
    public async Task ReturnsSuccess_Skill()
    {
        var nonAllowedMasterEntityIds = new List<int> { 1, 2 };
        var request = new OrderCreateDto
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "",
            Street = "Test",
            City = "Test",
            PostalCode = "Test",
            Country = "Test",
            //Items = new List<OrderItemDto>
            //{
            //    new OrderItemDto
            //    {
            //builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();
            //        Name = "Test",
            //        Price = 1
            //    }
            //}


        };

        var response = await _client.PostAsync(_apiUrl, request.ToStringContent());

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.FromJsonAsync<OrderDto>();

        result.Should().NotBeNull();
        // result!.Id.Should().NotBeEmpty();
        //result.Name.Should().BeEquivalentTo(request.Name);
        //result.NonAllowedMasterEntityIds.Should().BeEquivalentTo(nonAllowedMasterEntityIds);
    }
}
