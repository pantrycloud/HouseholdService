using MediatR;
using ErrorOr;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Queries.Handlers;

public class GetCurrentHouseholdQueryHandler(IHouseholdService householdService) :  IRequestHandler<GetCurrentHouseholdQuery, ErrorOr<GetCurrentHouseholdResponseDto>>
{

    public async Task<ErrorOr<GetCurrentHouseholdResponseDto>> Handle(GetCurrentHouseholdQuery request, CancellationToken cancellationToken)
    {
        return await householdService.GetCurrentHousehold(cancellationToken);
    }
}