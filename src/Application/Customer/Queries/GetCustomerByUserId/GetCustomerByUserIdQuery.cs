using Application.Common.Interfaces;
using Application.Common.Specifications;
using MediatR;

namespace Application.Customer.Queries.GetCustomerByUserId;

public record GetCustomerByUserIdQuery(Guid UserId) : IRequest<Guid>;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByUserIdQuery, Guid>
{
    private readonly IReadRepository<Domain.Entities.Customer> _customerRepository;
    public GetCustomerByIdQueryHandler(IReadRepository<Domain.Entities.Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<Guid> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FirstOrDefaultAsync(new CustomerByUserIdSpec(request.UserId), cancellationToken);
        return customer.Id;
    }
}
