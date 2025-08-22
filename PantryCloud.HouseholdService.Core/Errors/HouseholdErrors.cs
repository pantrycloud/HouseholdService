using ErrorOr;

namespace PantryCloud.HouseholdService.Core.Errors;

public static class HouseholdErrors
{
    public static Error UserAlreadyInHousehold => Error.Conflict(
        code: "Household.Creation.UserAlreadyInHousehold",
        description: "User is a member of a household and cannot create a new one."
    );
}