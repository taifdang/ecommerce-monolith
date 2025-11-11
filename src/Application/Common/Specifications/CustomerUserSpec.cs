using Application.Customer.Dtos;
using Ardalis.Specification;

namespace Application.Common.Specifications;

public class CustomerUserSpec : Specification<Domain.Entities.Customer, CustomerDto>
{  
    public CustomerUserSpec(Guid UserId)
    {
        Query.Where(x => x.UserId == UserId);

        Query.Select(x => new CustomerDto { Id = x.Id, FullName = x.FullName, Email = x.Email, Phone = x.Phone, Address = x.Address });
    }
}
