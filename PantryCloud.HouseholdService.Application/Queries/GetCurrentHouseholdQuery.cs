using MediatR;
using ErrorOr;
using PantryCloud.HouseholdService.Application.Dtos;

namespace PantryCloud.HouseholdService.Application.Queries;

public record GetCurrentHouseholdQuery : IRequest<ErrorOr<GetCurrentHouseholdResponseDto>>;