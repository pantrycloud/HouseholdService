using ErrorOr;
using PantryCloud.HouseholdService.Application.Dtos;
using PantryCloud.HouseholdService.Application.Queries;

namespace PantryCloud.HouseholdService.Application;

public interface IHouseholdManagementService
{
    public Task<ErrorOr<GetCurrentHouseholdResponseDto>> GetCurrentHousehold(CancellationToken cancellationToken);
    public Task<ErrorOr<CreateHouseholdResponseDto>> CreateHousehold(CreateHouseholdRequestDto request, CancellationToken cancellationToken);

    public Task<ErrorOr<LeaveHouseholdResponseDto>> LeaveHousehold(LeaveHouseholdRequestDto request, CancellationToken cancellationToken);
}