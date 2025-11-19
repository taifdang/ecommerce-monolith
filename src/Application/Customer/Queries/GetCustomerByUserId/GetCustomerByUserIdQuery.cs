using Application.Common.Interfaces;
using Application.Common.Specifications;
using Application.Customer.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Customer.Queries.GetCustomerByUserId;

public record GetCustomerByUserIdQuery(Guid UserId) : IRequest<CustomerDto>;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByUserIdQuery, CustomerDto>
{
    private readonly IReadRepository<Domain.Entities.Customer> _customerRepository;
    public GetCustomerByIdQueryHandler(IReadRepository<Domain.Entities.Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<CustomerDto> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FirstOrDefaultAsync(new CustomerByUserIdSpec(request.UserId), cancellationToken);
        return customer;
    }
}
