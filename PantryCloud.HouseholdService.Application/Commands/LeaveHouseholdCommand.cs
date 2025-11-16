using ErrorOr;
using MediatR;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Commands;

public record LeaveHouseholdCommand(LeaveHouseholdRequestDto Request) : IRequest<ErrorOr<LeaveHouseholdResponseDto>>;