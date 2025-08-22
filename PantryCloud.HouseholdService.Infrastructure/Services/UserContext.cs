using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PantryCloud.HouseholdService.Application;

namespace PantryCloud.HouseholdService.Infrastructure.Services;

public sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

    public Guid UserId =>
        Guid.TryParse(_user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
                      _user?.FindFirstValue(JwtRegisteredClaimNames.Sub), out var id)
            ? id
            : throw new UnauthorizedAccessException("User ID not found in token");

    public string? Email =>
        _user?.FindFirstValue(JwtRegisteredClaimNames.Email);
}