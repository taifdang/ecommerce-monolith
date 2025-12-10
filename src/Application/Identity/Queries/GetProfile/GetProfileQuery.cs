using Contracts.Responses;
using MediatR;

namespace Application.Identity.Queries.GetProfile;

public record GetProfileQuery() : IRequest<UserInfoResponse>;