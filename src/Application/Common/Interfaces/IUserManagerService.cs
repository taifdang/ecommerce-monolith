using Contracts.Requests;
using Contracts.Responses;

namespace Application.Common.Interfaces;

public interface IUserManagerService
{
    public Task<List<UserInfoResponse>> GetList(CancellationToken cancellationToken);
    public Task AssignRole(AssignRoleRequest request, CancellationToken cancellationToken);
    //public Task Update(UserUpdateRequest request, CancellationToken cancellationToken);
    public Task Delete(string userId);
}
