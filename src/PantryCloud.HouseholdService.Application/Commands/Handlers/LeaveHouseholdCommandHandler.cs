using ErrorOr;
using MediatR;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Commands.Handlers;

public class LeaveHouseholdCommandHandler(IHouseholdService householdService) : IRequestHandler<LeaveHouseholdCommand, ErrorOr<LeaveHouseholdResponseDto>>
{
    public async Task<ErrorOr<LeaveHouseholdResponseDto>> Handle(LeaveHouseholdCommand request, CancellationToken cancellationToken)
    {
        return await householdService.LeaveHousehold(request.Request, cancellationToken);
    }
}