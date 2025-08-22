namespace PantryCloud.HouseholdService.Application;

public interface IUserContext
{
    Guid UserId { get; }
    string? Email { get; }
}