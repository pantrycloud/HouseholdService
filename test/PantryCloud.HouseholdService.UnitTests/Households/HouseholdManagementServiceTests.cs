using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PantryCloud.HouseholdService.Application;
using PantryCloud.HouseholdService.Application.Dtos;
using PantryCloud.HouseholdService.Core.Entities;
using PantryCloud.HouseholdService.Core.Enums;
using PantryCloud.HouseholdService.Core.Errors;
using PantryCloud.HouseholdService.Infrastructure.Services;
using Shouldly;

namespace PantryCloud.HouseholdService.UnitTests.Households;

public class HouseholdManagementServiceTests
{
    private readonly ILogger<HouseholdManagementService> _logger = TestHelper.MockLogger<HouseholdManagementService>();

  [Fact]
    public async Task GetCurrentHousehold_ShouldReturnHousehold_WhenUserIsMember()
    {
        await using var db = TestHelper.CreateInMemoryContext(nameof(GetCurrentHousehold_ShouldReturnHousehold_WhenUserIsMember));
        db.Members.Add(new HouseholdMember
        {
            UserId = Constants.UserId,
            HouseholdId = Constants.HouseholdId,
            Role = HouseholdRole.Member,
            JoinedAt = DateTime.UtcNow
        });
        db.Households.Add(new Core.Entities.Household
        {
            Id = Constants.HouseholdId,
            Name = Constants.HouseholdName
        });
        await db.SaveChangesAsync();

        var service = new HouseholdManagementService(
            db,
            TestHelper.CreateMockUserContext(Constants.UserId, Constants.UserEmail),
            _logger
        );

        var result = await service.GetCurrentHousehold(CancellationToken.None);

        result.IsError.ShouldBeFalse();
        result.Value.Id.ShouldBe(Constants.HouseholdId);
        result.Value.Name.ShouldBe(Constants.HouseholdName);
    }

    [Fact]
    public async Task GetCurrentHousehold_ShouldReturnError_WhenUserNotInHousehold()
    {
        await using var db = TestHelper.CreateInMemoryContext(nameof(GetCurrentHousehold_ShouldReturnError_WhenUserNotInHousehold));

        var service = new HouseholdManagementService(
            db,
            TestHelper.CreateMockUserContext(Constants.UserId, Constants.UserEmail),
            _logger
        );

        var result = await service.GetCurrentHousehold(CancellationToken.None);

        result.IsError.ShouldBeTrue();
        result.Errors.ShouldContain(e => e.Code == "Household.NotFound");
    }

    [Fact]
    public async Task CreateHousehold_ShouldSucceed_WhenUserNotInAnyHousehold()
    {
        await using var db = TestHelper.CreateInMemoryContext(nameof(CreateHousehold_ShouldSucceed_WhenUserNotInAnyHousehold));

        var service = new HouseholdManagementService(
            db,
            TestHelper.CreateMockUserContext(Constants.UserId, Constants.UserEmail),
            _logger
        );

        var request = new CreateHouseholdRequestDto("My Household");

        var result = await service.CreateHousehold(request, CancellationToken.None);

        result.IsError.ShouldBeFalse();
        result.Value.Name.ShouldBe("My Household");

        var member = await db.Members.FirstOrDefaultAsync(m => m.UserId == Constants.UserId);
        member.ShouldNotBeNull();
        member.Role.ShouldBe(HouseholdRole.Owner);
    }

    [Fact]
    public async Task CreateHousehold_ShouldReturnError_WhenUserAlreadyMember()
    {
        await using var db = TestHelper.CreateInMemoryContext(nameof(CreateHousehold_ShouldReturnError_WhenUserAlreadyMember));
        db.Members.Add(new HouseholdMember
        {
            UserId = Constants.UserId,
            HouseholdId = Constants.HouseholdId,
            Role = HouseholdRole.Member,
            JoinedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        var service = new HouseholdManagementService(
            db,
            TestHelper.CreateMockUserContext(Constants.UserId, Constants.UserEmail),
            _logger
        );

        var result = await service.CreateHousehold(new CreateHouseholdRequestDto("Another One"), CancellationToken.None);

        result.IsError.ShouldBeTrue();
        result.Errors.ShouldContain(HouseholdErrors.UserAlreadyInHousehold);
    }

    [Fact]
    public async Task LeaveHousehold_ShouldDeleteHousehold_WhenUserIsOnlyMember()
    {
        await using var db = TestHelper.CreateInMemoryContext(nameof(LeaveHousehold_ShouldDeleteHousehold_WhenUserIsOnlyMember));
        db.Households.Add(new Core.Entities.Household { Id = Constants.HouseholdId, Name = "Solo House" });
        db.Members.Add(new HouseholdMember
        {
            UserId = Constants.UserId,
            HouseholdId = Constants.HouseholdId,
            Role = HouseholdRole.Owner,
            JoinedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        var service = new HouseholdManagementService(
            db,
            TestHelper.CreateMockUserContext(Constants.UserId, Constants.UserEmail),
            _logger
        );

        var result = await service.LeaveHousehold(new LeaveHouseholdRequestDto(), CancellationToken.None);

        result.IsError.ShouldBeFalse();
        db.Members.Any().ShouldBeFalse();
        db.Households.Any().ShouldBeFalse();
    }

    [Fact]
    public async Task LeaveHousehold_ShouldReturnError_WhenOwnerAndNotAlone()
    {
        await using var db = TestHelper.CreateInMemoryContext(nameof(LeaveHousehold_ShouldReturnError_WhenOwnerAndNotAlone));
        db.Households.Add(new Core.Entities.Household { Id = Constants.HouseholdId, Name = "Team House" });
        db.Members.AddRange(
            new HouseholdMember
            {
                UserId = Constants.UserId,
                HouseholdId = Constants.HouseholdId,
                Role = HouseholdRole.Owner,
                JoinedAt = DateTime.UtcNow
            },
            new HouseholdMember
            {
                UserId = Guid.NewGuid(),
                HouseholdId = Constants.HouseholdId,
                Role = HouseholdRole.Member,
                JoinedAt = DateTime.UtcNow
            }
        );
        await db.SaveChangesAsync();

        var service = new HouseholdManagementService(
            db,
            TestHelper.CreateMockUserContext(Constants.UserId, Constants.UserEmail),
            _logger
        );

        var result = await service.LeaveHousehold(new LeaveHouseholdRequestDto(), CancellationToken.None);

        result.IsError.ShouldBeTrue();
        result.Errors.ShouldContain(HouseholdErrors.OwnerCannotLeave);
    }

    [Fact]
    public async Task LeaveHousehold_ShouldRemoveMember_WhenNonOwner()
    {
        await using var db = TestHelper.CreateInMemoryContext(nameof(LeaveHousehold_ShouldRemoveMember_WhenNonOwner));
        db.Households.Add(new Core.Entities.Household { Id = Constants.HouseholdId, Name = "Shared House" });
        db.Members.AddRange(
            new HouseholdMember
            {
                UserId = Constants.UserId,
                HouseholdId = Constants.HouseholdId,
                Role = HouseholdRole.Member,
                JoinedAt = DateTime.UtcNow
            },
            new HouseholdMember
            {
                UserId = Guid.NewGuid(),
                HouseholdId = Constants.HouseholdId,
                Role = HouseholdRole.Owner,
                JoinedAt = DateTime.UtcNow
            }
        );
        await db.SaveChangesAsync();

        var service = new HouseholdManagementService(
            db,
            TestHelper.CreateMockUserContext(Constants.UserId, Constants.UserEmail),
            _logger
        );

        var result = await service.LeaveHousehold(new LeaveHouseholdRequestDto(), CancellationToken.None);

        result.IsError.ShouldBeFalse();
        db.Members.Count().ShouldBe(1);
        db.Members.Single().Role.ShouldBe(HouseholdRole.Owner);
    }

    [Fact]
    public async Task LeaveHousehold_ShouldReturnError_WhenUserNotMember()
    {
        await using var db = TestHelper.CreateInMemoryContext(nameof(LeaveHousehold_ShouldReturnError_WhenUserNotMember));

        var service = new HouseholdManagementService(
            db,
            TestHelper.CreateMockUserContext(Constants.UserId, Constants.UserEmail),
            _logger
        );

        var result = await service.LeaveHousehold(new LeaveHouseholdRequestDto(), CancellationToken.None);

        result.IsError.ShouldBeTrue();
        result.Errors.ShouldContain(HouseholdErrors.UserNotInAnyHousehold);
    }
}