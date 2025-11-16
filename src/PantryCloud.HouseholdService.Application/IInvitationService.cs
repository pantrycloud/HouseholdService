using PantryCloud.HouseholdService.Application.Dtos;
using ErrorOr;

namespace PantryCloud.HouseholdService.Application;

public interface IInvitationService
{
    Task<ErrorOr<SendHouseholdInvitationResponseDto>> SendHouseholdInvitation(SendHouseholdInvitationRequestDto request,
        CancellationToken cancellationToken);
    
    Task<ErrorOr<AcceptHouseholdInvitationResponseDto>> AcceptHouseholdInvitation(AcceptHouseholdInvitationRequestDto request,
        CancellationToken cancellationToken);
}