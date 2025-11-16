using ErrorOr;
using MediatR;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Commands.Handlers;

public class LeaveHouseholdCommandHandler(IHouseholdManagementService householdManagementService) : IRequestHandler<LeaveHouseholdCommand, ErrorOr<LeaveHouseholdResponseDto>>
{
    public async Task<ErrorOr<LeaveHouseholdResponseDto>> Handle(LeaveHouseholdCommand request, CancellationToken cancellationToken)
    {
        return await householdManagementService.LeaveHousehold(request.Request, cancellationToken);
    }
}