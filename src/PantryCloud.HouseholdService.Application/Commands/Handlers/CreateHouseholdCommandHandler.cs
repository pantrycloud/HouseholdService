using MediatR;
using ErrorOr;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Commands.Handlers;

public class CreateHouseholdCommandHandler(IHouseholdService householdService) : IRequestHandler<CreateHouseholdCommand, ErrorOr<CreateHouseholdResponseDto>>
{
    public async Task<ErrorOr<CreateHouseholdResponseDto>> Handle(CreateHouseholdCommand request, CancellationToken cancellationToken)
    {
        return await householdService.CreateHousehold(request.Request, cancellationToken);
    }
}