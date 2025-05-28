-- ============================================================================
-- Advanced User Management and Role-Based Access Control (RBAC) Database
-- Compatible with MySQL, PostgreSQL, and SQL Server
--- Made Modifications for SQL Server
-- ============================================================================

-- Drop existing tables if they exist (for fresh installation)
-- Uncomment the following lines if you want to recreate tables
/*
DROP TABLE IF EXISTS user_sessions;
DROP TABLE IF EXISTS user_permissions;
DROP TABLE IF EXISTS role_permissions;
DROP TABLE IF EXISTS user_roles;
DROP TABLE IF EXISTS permissions;
DROP TABLE IF EXISTS roles;
DROP TABLE IF EXISTS user_profiles;
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS organizations;
*/
CREATE Database FoodZOAI 
GO
-- ============================================================================
-- 1. ORGANIZATIONS TABLE (Multi-tenant support)
-- ============================================================================
CREATE TABLE organizations (
    id INT identity(1,1),
    name VARCHAR(255) NOT NULL,
    slug VARCHAR(100)   NOT NULL,
    description TEXT,
    website VARCHAR(255),
    logo_url VARCHAR(500),
    subscription_plan VARCHAR(50), --DEFAULT 'free',--ENUM('free', 'basic', 'premium', 'enterprise') 
    max_users INT DEFAULT 10,
    status Varchar(50),--ENUM('active', 'suspended', 'inactive') DEFAULT 'active',
    created_at DateTime DEFAULT GETDATE(),
    updated_at DateTime DEFAULT GETDATE(),
    deleted_at DateTime NULL,
	CONSTRAINT PK_Organizations_ID PRIMARY KEY CLUSTERED (id)
)
GO

-- ============================================================================
-- 2. USERS TABLE (Core user information)
-- ============================================================================
CREATE TABLE users (
    id INT  identity(1,1),
    organization_id INT,
    username VARCHAR(50)   NOT NULL,
    email VARCHAR(255)   NOT NULL,
    email_verified_at DateTime NULL,
    password_hash VARCHAR(255) NOT NULL,
    salt VARCHAR(255),
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    phone VARCHAR(20),
    avatar_url VARCHAR(500), -- ENUM('active', 'pending', 'suspended', 'deactivated')
    status VARCHAR(50) ,--  DEFAULT 'pending',
    last_login_at DateTime NULL,
    password_changed_at DateTime DEFAULT GETDATE(),
    failed_login_attempts INT DEFAULT 0,
    locked_until DateTime NULL,
    two_factor_enabled bit DEFAULT 0,
    two_factor_secret VARCHAR(255),
    created_by INT NULL,
    created_at DateTime DEFAULT GETDATE(),
    updated_at DateTime DEFAULT GETDATE(),
    deleted_at DateTime NULL,

   CONSTRAINT PK_users_ID PRIMARY KEY CLUSTERED (id),
    FOREIGN KEY (organization_id) REFERENCES organizations(id) ,
    FOREIGN KEY (created_by) REFERENCES users(id),
    INDEX idx_users_email (email),
    INDEX idx_users_username (username),
    INDEX idx_users_organization (organization_id),
    INDEX idx_users_status (status)
);

-- ============================================================================
-- 3. USER PROFILES TABLE (Extended user information)
-- ============================================================================
CREATE TABLE user_profiles (
    id INT identity(1,1),
    user_id INT   NOT NULL,
    bio TEXT,
    date_of_birth DATE,
    gender varchar(5),--ENUM('male', 'female', 'other', 'prefer_not_to_say'),
    address TEXT,
    city VARCHAR(100),
    state_province VARCHAR(100),
    postal_code VARCHAR(20),
    country VARCHAR(100),
    timezone VARCHAR(50) DEFAULT 'UTC',
    language VARCHAR(10) DEFAULT 'en',
    notification_preferences nvarchar(max),
    privacy_settings nvarchar(max),
    custom_fields nvarchar(max),
    created_at DateTime DEFAULT GETDATE(),
    updated_at DateTime DEFAULT GETDATE() ,
    
	CONSTRAINT PK_User_Profiles_ID PRIMARY KEY CLUSTERED (id),
    FOREIGN KEY (user_id) REFERENCES users(id) 
);

-- ============================================================================
-- 4. ROLES TABLE (Role definitions)
-- ============================================================================
CREATE TABLE roles (
    id INT identity(1,1),
    organization_id INT,
    name VARCHAR(100) NOT NULL,
    slug VARCHAR(100)   NOT NULL,
    description TEXT,
    level INT DEFAULT 0, -- Higher level = more privileges
    is_system_role BIT DEFAULT 0, -- Cannot be deleted
    color VARCHAR(7), -- Hex color code for UI
    created_at DateTime DEFAULT GETDATE(),
    updated_at DateTime DEFAULT GETDATE(),
	
	
    CONSTRAINT PK_Roles_ID PRIMARY KEY CLUSTERED (id),
    FOREIGN KEY (organization_id) REFERENCES organizations(id),
    
    INDEX idx_roles_organization (organization_id),
    INDEX idx_roles_level (level)
);

-- ============================================================================
-- 5. PERMISSIONS TABLE (Permission definitions)
-- ============================================================================
CREATE TABLE permissions (
    id INT  identity(1,1),
    name VARCHAR(100)   NOT NULL,
    slug VARCHAR(100)   NOT NULL,
    description TEXT,
    module VARCHAR(50), -- e.g., 'users', 'products', 'orders'
    action VARCHAR(50), -- e.g., 'create', 'read', 'update', 'delete'
    resource VARCHAR(50), -- e.g., 'user', 'product', 'order'
    is_system_permission bit DEFAULT 0,
    created_at DateTime DEFAULT GETDATE(),
    updated_at DateTime DEFAULT GETDATE(),

    CONSTRAINT PK_PermissionsD PRIMARY KEY CLUSTERED (id),
    INDEX idx_permissions_module (module),
    INDEX idx_permissions_action (action),
    INDEX idx_permissions_resource (resource)
);

-- ============================================================================
-- 6. USER_ROLES TABLE (Many-to-many: Users to Roles)
-- ============================================================================
CREATE TABLE user_roles (
    id INT identity(1,1),
    user_id INT NOT NULL,
    role_id INT NOT NULL  ,
    assigned_by INT,
    assigned_at DateTime DEFAULT GETDATE(),
    expires_at DateTime NULL,
    is_active bit DEFAULT 1,
    
	CONSTRAINT PK_User_Roles_ID PRIMARY KEY CLUSTERED (id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (role_id) REFERENCES roles(id),
    FOREIGN KEY (assigned_by) REFERENCES users(id) ,
    INDEX idx_user_roles_user (user_id),
    INDEX idx_user_roles_role (role_id)
);

-- ============================================================================
-- 7. ROLE_PERMISSIONS TABLE (Many-to-many: Roles to Permissions)
-- ============================================================================
CREATE TABLE role_permissions (
    id INT identity(1,1),
    role_id INT NOT NULL  ,
    permission_id INT NOT NULL  ,
    granted bit DEFAULT 1, -- Allow for explicit deny
    granted_by INT,
    granted_at DateTime DEFAULT GETDATE(),
    
	CONSTRAINT PK_Role_Permissions_ID PRIMARY KEY CLUSTERED (id),
    FOREIGN KEY (role_id) REFERENCES roles(id) ON DELETE CASCADE,
    FOREIGN KEY (permission_id) REFERENCES permissions(id) ON DELETE CASCADE,
    FOREIGN KEY (granted_by) REFERENCES users(id) ON DELETE SET NULL,
    INDEX idx_role_permissions_role (role_id),
    INDEX idx_role_permissions_permission (permission_id)
);

-- ============================================================================
-- 8. USER_PERMISSIONS TABLE (Direct user permissions)
-- ============================================================================
CREATE TABLE user_permissions (
    id INT identity(1,1),
    user_id INT NOT NULL  ,
    permission_id INT NOT NULL  ,
    granted BIT DEFAULT 1, -- Allow for explicit deny
    granted_by INT,
    granted_at DateTime DEFAULT GETDATE(),
    expires_at DateTime NULL,
	CONSTRAINT PK_User_Permissions_ID PRIMARY KEY CLUSTERED (id),
    FOREIGN KEY (user_id) REFERENCES users(id) ,
    FOREIGN KEY (permission_id) REFERENCES permissions(id) ,
    FOREIGN KEY (granted_by) REFERENCES users(id),  
    INDEX idx_user_permissions_user (user_id),
    INDEX idx_user_permissions_permission (permission_id)
);


-- ====appsetting

CREATE TABLE appsetting (
    id INT identity(1,1),
    [Name] varchar(50) NOT NULL  ,
    [key] varchar(50) NOT NULL  ,
    [value] varchar(50) NOT NULL  ,
created_at DateTime DEFAULT GETDATE(),
    updated_at DateTime DEFAULT GETDATE(),
    deleted_at DateTime NULL
    
	CONSTRAINT PK_appsetting_ID PRIMARY KEY CLUSTERED (id),
    INDEX idx_appsetting_key ([key])
);


-- ========================================

-- =====================================
-- EmailSettings

CREATE TABLE EmailSettings (
    Id INT IDENTITY(1,1),

    Host VARCHAR(32) NOT NULL,
	Port INT NOT NULL DEFAULT 25,
    UserName VARCHAR(32) NOT NULL,
    [Password] VARCHAR(128) NOT NULL,

    IsEnableSSL BIT NOT NULL,
    IsDefault BIT NOT NULL,
    IsActive BIT NOT NULL,

    CreatedByUser VARCHAR(32) NULL,
    ModifiedByUser VARCHAR(32) NULL,
    DeletedByUser VARCHAR(32) NULL,

    CONSTRAINT PK_EmailSettings_Id PRIMARY KEY CLUSTERED (Id)
);


INSERT INTO EmailSettings (
    Host, UserName, Password,
    IsEnableSSL, IsDefault, IsActive,
    CreatedByUser, ModifiedByUser, DeletedByUser
)
VALUES 
('smtp.gmail.com', 'lavanyajanu1507@gmail.com', 'pass1234', 1, 1, 1, 'admin', NULL, NULL),

('smtp.gmail.com', 'gokul@gmail.com', '12345', 1, 0, 1, 'admin', NULL, NULL);

--EmailTemplate

-- ==========================================

-- =====================================
-- EmailTemplate

CREATE TABLE EmailTemplate (
    Id INT IDENTITY(1,1),

    [Name] VARCHAR(32) NOT NULL,
    [Subject] VARCHAR(128) NOT NULL,   
    Body VARCHAR(MAX) NOT NULL,     

    IsActive BIT NOT NULL,

    CreatedByUser VARCHAR(32) NULL,
    ModifiedByUser VARCHAR(32) NULL,
    DeletedByUser VARCHAR(32) NULL,

    CONSTRAINT PK_EmailTemplate_Id PRIMARY KEY CLUSTERED (Id)
);


INSERT INTO EmailTemplate (
    Name, Subject, Body, IsActive,
    CreatedByUser, ModifiedByUser, DeletedByUser
)
VALUES 
-- Welcome Email
('WelcomeEmail', 'Welcome to Our Service!', 
 'Hello {{UserName}}, thank you for joining us!', 
 3, 'admin', NULL, NULL);

-- =====================================


-- ============================================================================
-- 9. USER_SESSIONS TABLE (Session management)
-- ============================================================================
CREATE TABLE user_sessions (
    id INT identity(1,1),
    user_id INT NOT NULL,
    session_token VARCHAR(255)   NOT NULL,
    refresh_token VARCHAR(255)  ,
    ip_address VARCHAR(45),
    user_agent TEXT,
    device_info varchar(Max), -- JSON
    location varchar(Max), -- JSON,
    is_active BIT DEFAULT 1,
    expires_at DateTime NOT NULL,
    last_activity_at DateTime DEFAULT GETDATE(),
    created_at DateTime DEFAULT GETDATE(),
    CONSTRAINT PK_user_sessions_ID PRIMARY KEY CLUSTERED (id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    INDEX idx_sessions_user (user_id),
    INDEX idx_sessions_token (session_token),
    INDEX idx_sessions_active (is_active),
    INDEX idx_sessions_expires (expires_at)
);

-- ============================================================================
-- 10. INITIAL DATA INSERTION
-- ============================================================================

-- Insert default organization
INSERT INTO organizations (name, slug, description, subscription_plan, max_users, status) 
VALUES ('Default Organization', 'default', 'Default organization for the application', 'enterprise', 1000, 'active');

-- Insert system roles
INSERT INTO roles (organization_id, name, slug, description, level, is_system_role, color) VALUES
(1, 'Super Administrator', 'super_admin', 'Full system access', 100, 1, '#FF0000'),
(1, 'Administrator', 'admin', 'Administrative access', 90, 1, '#FF6600'),
(1, 'Manager', 'manager', 'Management level access', 70, 1, '#0066FF'),
(1, 'Editor', 'editor', 'Content editing access', 50, 1, '#009900'),
(1, 'User', 'user', 'Basic user access', 10, 1, '#666666'),
(1, 'Guest', 'guest', 'Read-only access', 0, 1, '#CCCCCC');

-- Insert system permissions
INSERT INTO permissions (name, slug, description, module, action, resource, is_system_permission) VALUES
-- User management permissions
('Create Users', 'create_users', 'Can create new users', 'users', 'create', 'user', 1),
('Read Users', 'read_users', 'Can view user information', 'users', 'read', 'user', 1),
('Update Users', 'update_users', 'Can modify user information', 'users', 'update', 'user', 1),
('Delete Users', 'delete_users', 'Can delete users', 'users', 'delete', 'user', 1),
('Manage User Roles', 'manage_user_roles', 'Can assign/remove user roles', 'users', 'manage', 'roles', 1),

-- Role management permissions
('Create Roles', 'create_roles', 'Can create new roles', 'roles', 'create', 'role', 1),
('Read Roles', 'read_roles', 'Can view role information', 'roles', 'read', 'role', 1),
('Update Roles', 'update_roles', 'Can modify role information', 'roles', 'update', 'role', 1),
('Delete Roles', 'delete_roles', 'Can delete roles', 'roles', 'delete', 'role', 1),
('Manage Role Permissions', 'manage_role_permissions', 'Can assign/remove role permissions', 'roles', 'manage', 'permissions', 1),

-- Permission management permissions
('Create Permissions', 'create_permissions', 'Can create new permissions', 'permissions', 'create', 'permission', 1),
('Read Permissions', 'read_permissions', 'Can view permission information', 'permissions', 'read', 'permission', 1),
('Update Permissions', 'update_permissions', 'Can modify permission information', 'permissions', 'update', 'permission', 1),
('Delete Permissions', 'delete_permissions', 'Can delete permissions', 'permissions', 'delete', 'permission', 1),

-- Organization management permissions
('Create Organizations', 'create_organizations', 'Can create new organizations', 'organizations', 'create', 'organization', 1),
('Read Organizations', 'read_organizations', 'Can view organization information', 'organizations', 'read', 'organization', 1),
('Update Organizations', 'update_organizations', 'Can modify organization information', 'organizations', 'update', 'organization', 1),
('Delete Organizations', 'delete_organizations', 'Can delete organizations', 'organizations', 'delete', 'organization', 1),

-- Session management permissions
('Manage Sessions', 'manage_sessions', 'Can manage user sessions', 'sessions', 'manage', 'session', 1),
('View System Logs', 'view_system_logs', 'Can view system activity logs', 'system', 'read', 'logs', 1),

-- Profile management permissions
('Update Own Profile', 'update_own_profile', 'Can update own profile', 'profile', 'update', 'own', 1),
('View Own Profile', 'view_own_profile', 'Can view own profile', 'profile', 'read', 'own', 1);

-- Assign permissions to super admin role
INSERT INTO role_permissions (role_id, permission_id, granted_by)
SELECT 1, id, NULL FROM permissions WHERE is_system_permission = 1;

-- Assign specific permissions to admin role
INSERT INTO role_permissions (role_id, permission_id, granted_by)
SELECT 2, id, NULL FROM permissions 
WHERE slug IN ('create_users', 'read_users', 'update_users', 'manage_user_roles', 
               'read_roles', 'read_permissions', 'read_organizations', 'manage_sessions', 
               'update_own_profile', 'view_own_profile');

-- Assign permissions to manager role
INSERT INTO role_permissions (role_id, permission_id, granted_by)
SELECT 3, id, NULL FROM permissions 
WHERE slug IN ('read_users', 'update_users', 'read_roles', 'read_permissions', 
               'update_own_profile', 'view_own_profile');

-- Assign permissions to editor role
INSERT INTO role_permissions (role_id, permission_id, granted_by)
SELECT 4, id, NULL FROM permissions 
WHERE slug IN ('read_users', 'update_own_profile', 'view_own_profile');

-- Assign permissions to user role
INSERT INTO role_permissions (role_id, permission_id, granted_by)
SELECT 5, id, NULL FROM permissions 
WHERE slug IN ('update_own_profile', 'view_own_profile');

-- Assign permissions to guest role
INSERT INTO role_permissions (role_id, permission_id, granted_by)
SELECT 6, id, NULL FROM permissions 
WHERE slug IN ('view_own_profile');

---- ============================================================================
---- 11. USEFUL VIEWS
---- ============================================================================

---- View: User roles and permissions
--CREATE VIEW user_effective_permissions AS
--SELECT DISTINCT
--    u.id as user_id,
--    u.username,
--    u.email,
--    p.id as permission_id,
--    p.slug as permission_slug,
--    p.name as permission_name,
--    'role' as source_type,
--    r.name as source_name
--FROM users u
--JOIN user_roles ur ON u.id = ur.user_id AND ur.is_active = 1
--JOIN role_permissions rp ON ur.role_id = rp.role_id AND rp.granted = 1
--JOIN permissions p ON rp.permission_id = p.id
--JOIN roles r ON ur.role_id = r.id
--WHERE u.deleted_at IS NULL
--UNION
--SELECT DISTINCT
--    u.id as user_id,
--    u.username,
--    u.email,
--    p.id as permission_id,
--    p.slug as permission_slug,
--    p.name as permission_name,
--    'direct' as source_type,
--    'Direct Assignment' as source_name
--FROM users u
--JOIN user_permissions up ON u.id = up.user_id AND up.granted = 1
--JOIN permissions p ON up.permission_id = p.id
--WHERE u.deleted_at IS NULL;

---- View: User summary
--CREATE VIEW user_summary AS
--SELECT 
--    u.id,
--    u.username,
--    u.email,
--    u.first_name,
--    u.last_name,
--    u.status,
--    u.last_login_at,
--    o.name as organization_name,
--    GROUP_CONCAT(r.name ORDER BY r.level DESC) as roles,
--    COUNT(DISTINCT ur.role_id) as role_count,
--    u.created_at
--FROM users u
--LEFT JOIN organizations o ON u.organization_id = o.id
--LEFT JOIN user_roles ur ON u.id = ur.user_id AND ur.is_active = 1
--LEFT JOIN roles r ON ur.role_id = r.id
--WHERE u.deleted_at IS NULL
--GROUP BY u.id, u.username, u.email, u.first_name, u.last_name, u.status, u.last_login_at, o.name, u.created_at;

-- ============================================================================
-- 12. USEFUL STORED PROCEDURES
-- ============================================================================



---- Procedure: Check if user has permission
--CREATE PROCEDURE CheckUserPermission(
--    IN p_user_id INT,
--    IN p_permission_slug VARCHAR(100),
--    OUT p_has_permission BOOLEAN
--)
--BEGIN
--    DECLARE permission_count INT DEFAULT 0;
    
--    SELECT COUNT(*) INTO permission_count
--    FROM user_effective_permissions
--    WHERE user_id = p_user_id AND permission_slug = p_permission_slug;
    
--    SET p_has_permission = (permission_count > 0);
--END //

---- Procedure: Get user permissions
--CREATE PROCEDURE GetUserPermissions(
--    IN p_user_id INT
--)
--BEGIN
--    SELECT DISTINCT
--        permission_slug,
--        permission_name,
--        source_type,
--        source_name
--    FROM user_effective_permissions
--    WHERE user_id = p_user_id
--    ORDER BY permission_name;
--END //

---- Procedure: Assign role to user
--CREATE PROCEDURE AssignRoleToUser(
--    IN p_user_id INT,
--    IN p_role_id INT,
--    IN p_assigned_by INT,
--    IN p_expires_at DateTime
--)
--BEGIN
--    DECLARE EXIT HANDLER FOR SQLEXCEPTION
--    BEGIN
--        ROLLBACK;
--        RESIGNAL;
--    END;
    
--    START TRANSACTION;
    
--    INSERT INTO user_roles (user_id, role_id, assigned_by, expires_at)
--    VALUES (p_user_id, p_role_id, p_assigned_by, p_expires_at)
--    ON DUPLICATE KEY UPDATE
--        assigned_by = p_assigned_by,
--        assigned_at = GETDATE(),
--        expires_at = p_expires_at,
--        is_active = 1;
    
--    COMMIT;
--END //

---- Procedure: Create user with default role
--CREATE PROCEDURE CreateUserWithRole(
--    IN p_organization_id INT,
--    IN p_username VARCHAR(50),
--    IN p_email VARCHAR(255),
--    IN p_password_hash VARCHAR(255),
--    IN p_first_name VARCHAR(100),
--    IN p_last_name VARCHAR(100),
--    IN p_role_slug VARCHAR(100),
--    IN p_created_by INT,
--    OUT p_user_id INT
--)
--BEGIN
--    DECLARE role_id INT;
--    DECLARE EXIT HANDLER FOR SQLEXCEPTION
--    BEGIN
--        ROLLBACK;
--        RESIGNAL;
--    END;
    
--    START TRANSACTION;
    
--    -- Insert user
--    INSERT INTO users (organization_id, username, email, password_hash, first_name, last_name, created_by)
--    VALUES (p_organization_id, p_username, p_email, p_password_hash, p_first_name, p_last_name, p_created_by);
    
--    SET p_user_id = LAST_INSERT_ID();
    
--    -- Create user profile
--    INSERT INTO user_profiles (user_id) VALUES (p_user_id);
    
--    -- Assign default role
--    SELECT id INTO role_id FROM roles WHERE slug = p_role_slug AND organization_id = p_organization_id LIMIT 1;
    
--    IF role_id IS NOT NULL THEN
--        INSERT INTO user_roles (user_id, role_id, assigned_by)
--        VALUES (p_user_id, role_id, p_created_by);
--    END IF;
    
--    COMMIT;
--END //



-- ============================================================================
-- 13. INDEXES FOR PERFORMANCE
-- ============================================================================

-- Additional indexes for better performance
CREATE INDEX idx_user_sessions_last_activity ON user_sessions(last_activity_at);
CREATE INDEX idx_users_last_login ON users(last_login_at);
CREATE INDEX idx_user_roles_expires ON user_roles(expires_at);
CREATE INDEX idx_user_permissions_expires ON user_permissions(expires_at);
