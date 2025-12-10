using Application.Common.Interfaces;
using Contracts.Responses;
using MediatR;

namespace Application.Identity.Queries.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, UserInfoResponse>
{
    private readonly IIdentityService _identityService;

    public GetProfileQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<UserInfoResponse> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.GetProfile();
    }
}
