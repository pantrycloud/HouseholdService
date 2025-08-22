namespace PantryCloud.HouseholdService.Core.Entities;

public class HouseholdInvitation
{
    public Guid Id { get; set; }
    public Guid HouseholdId { get; set; }

    public string Code { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
}