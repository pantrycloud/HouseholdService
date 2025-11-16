using MediatR;
using ErrorOr;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Commands;

public record SendHouseholdInvitationCommand(SendHouseholdInvitationRequestDto Request) : IRequest<ErrorOr<SendHouseholdInvitationResponseDto>>;