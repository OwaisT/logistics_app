using LogisticsApp.Domain.Shared.Aggregates.User;
using LogisticsApp.Domain.Shared.Aggregates.User.ValueObjects;
using LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogisticsApp.Infrastructure.Persistence.Aggregates.Users.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
        ConfigureUserRolesTable(builder);
    }

    private static void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt);

        builder.Property(u => u.IsActive)
            .IsRequired();

    }

    private static void ConfigureUserRolesTable(EntityTypeBuilder<User> builder)
    {
        builder.Ignore(u => u.Roles);

        builder
            .HasMany<RoleEntity>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                j => j
                    .HasOne<RoleEntity>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRoles_Roles_RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRoles_Users_UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("UserRoles");
                    j.HasKey("UserId", "RoleId");
                });
    }

}