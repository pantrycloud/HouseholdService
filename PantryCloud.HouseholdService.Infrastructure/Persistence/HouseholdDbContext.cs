using Microsoft.EntityFrameworkCore;
using PantryCloud.HouseholdService.Core.Configurations;
using PantryCloud.HouseholdService.Core.Entities;

namespace PantryCloud.HouseholdService.Infrastructure.Persistence;

public class HouseholdDbContext(DbContextOptions<HouseholdDbContext> options) : DbContext(options)
{
    public DbSet<Household> Households => Set<Household>();
    public DbSet<HouseholdMember> Members => Set<HouseholdMember>();
    public DbSet<HouseholdInvitation> Invitations => Set<HouseholdInvitation>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(HouseholdConfiguration).Assembly);

        base.OnModelCreating(builder);
    }
}