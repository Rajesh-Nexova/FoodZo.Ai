using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Models;

public partial class FoodZoaiContext : DbContext
{
    public FoodZoaiContext()
    {
    }

    public FoodZoaiContext(DbContextOptions<FoodZoaiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appsetting> Appsettings { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<EmailSetting> EmailSettings { get; set; }
    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<ReminderUser> ReminderUsers { get; set; }

    public virtual DbSet<UserNotification> UserNotifications { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }
   

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    public DbSet<DailyReminder> DailyReminders { get; set; } = null!;

    public DbSet<WeeklyReminder> WeeklyReminders { get; set; }

    public DbSet<MonthlyReminder> MonthlyReminders { get; set; }





    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SANT\\SQLEXPRESS;Database=FoodZOAI;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appsetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_appsetting_ID");

            entity.ToTable("appsetting");

            entity.HasIndex(e => e.Key, "idx_appsetting_key");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedByUser)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.DeletedByUser)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Key)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("key");
            entity.Property(e => e.ModifiedByUser)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<EmailSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmailSettings_Id");

            entity.Property(e => e.CreatedByUser)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.DeletedByUser)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Host)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.IsEnableSsl).HasColumnName("IsEnableSSL");
            entity.Property(e => e.ModifiedByUser)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmailTemplate_Id");

            entity.ToTable("EmailTemplate");

            entity.Property(e => e.Body).IsUnicode(false);
            entity.Property(e => e.CreatedByUser)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.DeletedByUser)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedByUser)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Organizations_ID");

            entity.ToTable("organizations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("logo_url");
            entity.Property(e => e.Banner_Image)
               .HasMaxLength(500)
               .IsUnicode(false)
               .HasColumnName("banner_image");
            entity.Property(e => e.MaxUsers)
                .HasDefaultValue(10)
                .HasColumnName("max_users");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SubscriptionPlan)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("subscription_plan");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("website");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PermissionsD");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.Action, "idx_permissions_action");

            entity.HasIndex(e => e.Module, "idx_permissions_module");

            entity.HasIndex(e => e.Resource, "idx_permissions_resource");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("action");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsSystemPermission)
                .HasDefaultValue(false)
                .HasColumnName("is_system_permission");
            entity.Property(e => e.Module)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("module");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Resource)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("resource");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Roles_ID");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Level, "idx_roles_level");

            entity.HasIndex(e => e.OrganizationId, "idx_roles_organization");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsSystemRole)
                .HasDefaultValue(false)
                .HasColumnName("is_system_role");
            entity.Property(e => e.Level)
                .HasDefaultValue(0)
                .HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Organization).WithMany(p => p.Roles)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK__roles__organizat__75A278F5");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Role_Permissions_ID");

            entity.ToTable("role_permissions");

            entity.HasIndex(e => e.PermissionId, "idx_role_permissions_permission");

            entity.HasIndex(e => e.RoleId, "idx_role_permissions_role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Granted)
                .HasDefaultValue(true)
                .HasColumnName("granted");
            entity.Property(e => e.GrantedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("granted_at");
            entity.Property(e => e.GrantedBy).HasColumnName("granted_by");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.GrantedByNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.GrantedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__role_perm__grant__07C12930");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK__role_perm__permi__06CD04F7");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__role_perm__role___05D8E0BE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users_ID");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "idx_users_email");

            entity.HasIndex(e => e.LastLoginAt, "idx_users_last_login");

            entity.HasIndex(e => e.OrganizationId, "idx_users_organization");

            entity.HasIndex(e => e.Status, "idx_users_status");

            entity.HasIndex(e => e.Username, "idx_users_username");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("avatar_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("email_verified_at");
            entity.Property(e => e.FailedLoginAttempts)
                .HasDefaultValue(0)
                .HasColumnName("failed_login_attempts");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastLoginAt)
                .HasColumnType("datetime")
                .HasColumnName("last_login_at");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.LockedUntil)
                .HasColumnType("datetime")
                .HasColumnName("locked_until");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.PasswordChangedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("password_changed_at");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Salt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("salt");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TwoFactorEnabled)
                .HasDefaultValue(false)
                .HasColumnName("two_factor_enabled");
            entity.Property(e => e.TwoFactorSecret)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("two_factor_secret");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__users__created_b__68487DD7");

            entity.HasOne(d => d.Organization).WithMany(p => p.Users)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK__users__organizat__6754599E");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User_Permissions_ID");

            entity.ToTable("user_permissions");

            entity.HasIndex(e => e.ExpiresAt, "idx_user_permissions_expires");

            entity.HasIndex(e => e.PermissionId, "idx_user_permissions_permission");

            entity.HasIndex(e => e.UserId, "idx_user_permissions_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.Granted)
                .HasDefaultValue(true)
                .HasColumnName("granted");
            entity.Property(e => e.GrantedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("granted_at");
            entity.Property(e => e.GrantedBy).HasColumnName("granted_by");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.GrantedByNavigation).WithMany(p => p.UserPermissionGrantedByNavigations)
                .HasForeignKey(d => d.GrantedBy)
                .HasConstraintName("FK__user_perm__grant__0E6E26BF");

            entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_perm__permi__0D7A0286");

            entity.HasOne(d => d.User).WithMany(p => p.UserPermissionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_perm__user___0C85DE4D");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User_Profiles_ID");

            entity.ToTable("user_profiles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Bio)
                .HasColumnType("text")
                .HasColumnName("bio");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomFields).HasColumnName("custom_fields");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Gender)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Language)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("en")
                .HasColumnName("language");
            entity.Property(e => e.NotificationPreferences).HasColumnName("notification_preferences");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.PrivacySettings).HasColumnName("privacy_settings");
            entity.Property(e => e.StateProvince)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("state_province");
            entity.Property(e => e.Timezone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("UTC")
                .HasColumnName("timezone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_prof__user___6EF57B66");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User_Roles_ID");

            entity.ToTable("user_roles");

            entity.HasIndex(e => e.ExpiresAt, "idx_user_roles_expires");

            entity.HasIndex(e => e.RoleId, "idx_user_roles_role");

            entity.HasIndex(e => e.UserId, "idx_user_roles_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("assigned_at");
            entity.Property(e => e.AssignedBy).HasColumnName("assigned_by");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.UserRoleAssignedByNavigations)
                .HasForeignKey(d => d.AssignedBy)
                .HasConstraintName("FK__user_role__assig__01142BA1");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_role__role___00200768");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_role__user___7F2BE32F");
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_user_sessions_ID");

            entity.ToTable("user_sessions");

            entity.HasIndex(e => e.IsActive, "idx_sessions_active");

            entity.HasIndex(e => e.ExpiresAt, "idx_sessions_expires");

            entity.HasIndex(e => e.SessionToken, "idx_sessions_token");

            entity.HasIndex(e => e.UserId, "idx_sessions_user");

            entity.HasIndex(e => e.LastActivityAt, "idx_user_sessions_last_activity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeviceInfo)
                .IsUnicode(false)
                .HasColumnName("device_info");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ip_address");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastActivityAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_activity_at");
            entity.Property(e => e.Location)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("refresh_token");
            entity.Property(e => e.SessionToken)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("session_token");
            entity.Property(e => e.UserAgent)
                .HasColumnType("text")
                .HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_sess__user___14270015");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
