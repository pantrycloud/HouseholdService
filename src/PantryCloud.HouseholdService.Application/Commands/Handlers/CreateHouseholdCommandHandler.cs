using MediatR;
using ErrorOr;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Commands.Handlers;

public class CreateHouseholdCommandHandler(IHouseholdManagementService householdManagementService) : IRequestHandler<CreateHouseholdCommand, ErrorOr<CreateHouseholdResponseDto>>
{
    public async Task<ErrorOr<CreateHouseholdResponseDto>> Handle(CreateHouseholdCommand request, CancellationToken cancellationToken)
    {
        return await householdManagementService.CreateHousehold(request.Request, cancellationToken);
    }
}