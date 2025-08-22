using MediatR;
using ErrorOr;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Commands;

public record CreateHouseholdCommand(CreateHouseholdRequestDto Request) : IRequest<ErrorOr<CreateHouseholdResponseDto>>;