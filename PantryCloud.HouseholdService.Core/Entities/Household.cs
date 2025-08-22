namespace PantryCloud.HouseholdService.Core.Entities;

public class Household
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    public ICollection<HouseholdMember> Members { get; set; } = new List<HouseholdMember>();
    public ICollection<HouseholdInvitation> Invitations { get; set; } = new List<HouseholdInvitation>();
}