using MediatR;
using ErrorOr;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Commands.Handlers;

public class AcceptHouseholdInvitationCommandHandler(IInvitationService invitationService) : IRequestHandler<AcceptHouseholdInvitationCommand, ErrorOr<AcceptHouseholdInvitationResponseDto>>
{
    public async Task<ErrorOr<AcceptHouseholdInvitationResponseDto>> Handle(AcceptHouseholdInvitationCommand request, CancellationToken cancellationToken)
    {
        return await invitationService.AcceptHouseholdInvitation(request.Request, cancellationToken);
    }
}