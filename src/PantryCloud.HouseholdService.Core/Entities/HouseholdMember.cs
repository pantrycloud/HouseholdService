using PantryCloud.HouseholdService.Core.Enums;

namespace PantryCloud.HouseholdService.Core.Entities;

public class HouseholdMember
{
    public Guid Id { get; set; }
    public Guid HouseholdId { get; set; }
    public Guid UserId { get; set; }
    public HouseholdRole Role { get; set; }
    public DateTime JoinedAt { get; set; }
}