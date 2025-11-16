using MediatR;
using ErrorOr;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Commands;

public record AcceptHouseholdInvitationCommand(AcceptHouseholdInvitationRequestDto Request) : IRequest<ErrorOr<AcceptHouseholdInvitationResponseDto>>;