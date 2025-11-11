using Application.Customer.Dtos;
using Application.Customer.Queries.GetCustomerByUserId;
using Microsoft.AspNetCore.Mvc;
using Shared.Web;

namespace Api.Controllers.Customers;

[Route(BaseApiPath + "/customer")]
public class CustomersController(ICurrentUserProdvider currentUserProdvider) : BaseController
{
    private readonly ICurrentUserProdvider _currentUserProdvider = currentUserProdvider;

    [HttpGet]
    public async Task<CustomerDto> GetCurrentCustomer()
    {
        var userId = _currentUserProdvider.GetCurrentUserId();
        var customer = await Mediator.Send(new GetCustomerByUserIdQuery { UserId = userId ?? Guid.Empty});
        return customer;
    }
}
