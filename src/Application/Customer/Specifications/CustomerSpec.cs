using Application.Customer.Queries;
using Ardalis.Specification;

namespace Application.Customer.Specifications;

public class CustomerSpec : Specification<Domain.Entities.Customer>
{
    public CustomerSpec() { }

    public CustomerSpec ByUserId(Guid UserId)
    {
        Query.Where(x => x.UserId == UserId);   
        return this;
    }
}

public class CustomerProjectionSpec : Specification<Domain.Entities.Customer, CustomerDto>
{
    public CustomerProjectionSpec()
    {
        Query.Select(x => new CustomerDto
        {
            Id = x.Id,
            FullName = x.FullName,
            Email = x.Email,
            Address = x.Address,
            Phone = x.Phone,
        });
    }
}
