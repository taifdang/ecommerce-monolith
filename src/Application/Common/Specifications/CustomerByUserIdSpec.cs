using Ardalis.Specification;

namespace Application.Common.Specifications;

public class CustomerByUserIdSpec : Specification<Domain.Entities.Customer>
{  
    public CustomerByUserIdSpec(Guid UserId)
    {
        Query.Where(x => x.UserId == UserId);

        //Query.Select(x => new CustomerDto(x.Id, x.FullName, x.Email, x.Phone, x.Address));
    }
}
