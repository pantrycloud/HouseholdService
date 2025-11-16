using PantryCloud.HouseholdService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PantryCloud.HouseholdService.Core.Configurations;

public class HouseholdInvitationConfiguration : IEntityTypeConfiguration<HouseholdInvitation>
{
    public void Configure(EntityTypeBuilder<HouseholdInvitation> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Code)
            .IsRequired();
        
        builder.Property(i => i.ExpiresAt)
            .IsRequired();

        builder.HasIndex(i => i.Code)
            .IsUnique(); 
    }
}