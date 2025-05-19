-- ============================================================================
-- Advanced User Management and Role-Based Access Control (RBAC) Database
-- Compatible with MySQL, PostgreSQL, and SQL Server
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

-- ============================================================================
-- 1. ORGANIZATIONS TABLE (Multi-tenant support)
-- ============================================================================
CREATE TABLE organizations (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    slug VARCHAR(100) UNIQUE NOT NULL,
    description TEXT,
    website VARCHAR(255),
    logo_url VARCHAR(500),
    subscription_plan ENUM('free', 'basic', 'premium', 'enterprise') DEFAULT 'free',
    max_users INT DEFAULT 10,
    status ENUM('active', 'suspended', 'inactive') DEFAULT 'active',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP NULL
);

-- ============================================================================
-- 2. USERS TABLE (Core user information)
-- ============================================================================
CREATE TABLE users (
    id INT PRIMARY KEY AUTO_INCREMENT,
    organization_id INT,
    username VARCHAR(50) UNIQUE NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    email_verified_at TIMESTAMP NULL,
    password_hash VARCHAR(255) NOT NULL,
    salt VARCHAR(255),
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    phone VARCHAR(20),
    avatar_url VARCHAR(500),
    status ENUM('active', 'pending', 'suspended', 'deactivated') DEFAULT 'pending',
    last_login_at TIMESTAMP NULL,
    password_changed_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    failed_login_attempts INT DEFAULT 0,
    locked_until TIMESTAMP NULL,
    two_factor_enabled BOOLEAN DEFAULT FALSE,
    two_factor_secret VARCHAR(255),
    created_by INT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP NULL,
    
    FOREIGN KEY (organization_id) REFERENCES organizations(id) ON DELETE SET NULL,
    FOREIGN KEY (created_by) REFERENCES users(id) ON DELETE SET NULL,
    INDEX idx_users_email (email),
    INDEX idx_users_username (username),
    INDEX idx_users_organization (organization_id),
    INDEX idx_users_status (status)
);

-- ============================================================================
-- 3. USER PROFILES TABLE (Extended user information)
-- ============================================================================
CREATE TABLE user_profiles (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT UNIQUE NOT NULL,
    bio TEXT,
    date_of_birth DATE,
    gender ENUM('male', 'female', 'other', 'prefer_not_to_say'),
    address TEXT,
    city VARCHAR(100),
    state_province VARCHAR(100),
    postal_code VARCHAR(20),
    country VARCHAR(100),
    timezone VARCHAR(50) DEFAULT 'UTC',
    language VARCHAR(10) DEFAULT 'en',
    notification_preferences JSON,
    privacy_settings JSON,
    custom_fields JSON,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

-- ============================================================================
-- 4. ROLES TABLE (Role definitions)
-- ============================================================================
CREATE TABLE roles (
    id INT PRIMARY KEY AUTO_INCREMENT,
    organization_id INT,
    name VARCHAR(100) NOT NULL,
    slug VARCHAR(100) NOT NULL,
    description TEXT,
    level INT DEFAULT 0, -- Higher level = more privileges
    is_system_role BOOLEAN DEFAULT FALSE, -- Cannot be deleted
    color VARCHAR(7), -- Hex color code for UI
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    FOREIGN KEY (organization_id) REFERENCES organizations(id) ON DELETE CASCADE,
    UNIQUE KEY unique_role_org (organization_id, slug),
    INDEX idx_roles_organization (organization_id),
    INDEX idx_roles_level (level)
);

-- ============================================================================
-- 5. PERMISSIONS TABLE (Permission definitions)
-- ============================================================================
CREATE TABLE permissions (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) UNIQUE NOT NULL,
    slug VARCHAR(100) UNIQUE NOT NULL,
    description TEXT,
    module VARCHAR(50), -- e.g., 'users', 'products', 'orders'
    action VARCHAR(50), -- e.g., 'create', 'read', 'update', 'delete'
    resource VARCHAR(50), -- e.g., 'user', 'product', 'order'
    is_system_permission BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    INDEX idx_permissions_module (module),
    INDEX idx_permissions_action (action),
    INDEX idx_permissions_resource (resource)
);

-- ============================================================================
-- 6. USER_ROLES TABLE (Many-to-many: Users to Roles)
-- ============================================================================
CREATE TABLE user_roles (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL,
    role_id INT NOT NULL,
    assigned_by INT,
    assigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    expires_at TIMESTAMP NULL,
    is_active BOOLEAN DEFAULT TRUE,
    
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY (role_id) REFERENCES roles(id) ON DELETE CASCADE,
    FOREIGN KEY (assigned_by) REFERENCES users(id) ON DELETE SET NULL,
    UNIQUE KEY unique_user_role (user_id, role_id),
    INDEX idx_user_roles_user (user_id),
    INDEX idx_user_roles_role (role_id)
);

-- ============================================================================
-- 7. ROLE_PERMISSIONS TABLE (Many-to-many: Roles to Permissions)
-- ============================================================================
CREATE TABLE role_permissions (
    id INT PRIMARY KEY AUTO_INCREMENT,
    role_id INT NOT NULL,
    permission_id INT NOT NULL,
    granted BOOLEAN DEFAULT TRUE, -- Allow for explicit deny
    granted_by INT,
    granted_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (role_id) REFERENCES roles(id) ON DELETE CASCADE,
    FOREIGN KEY (permission_id) REFERENCES permissions(id) ON DELETE CASCADE,
    FOREIGN KEY (granted_by) REFERENCES users(id) ON DELETE SET NULL,
    UNIQUE KEY unique_role_permission (role_id, permission_id),
    INDEX idx_role_permissions_role (role_id),
    INDEX idx_role_permissions_permission (permission_id)
);

-- ============================================================================
-- 8. USER_PERMISSIONS TABLE (Direct user permissions)
-- ============================================================================
CREATE TABLE user_permissions (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL,
    permission_id INT NOT NULL,
    granted BOOLEAN DEFAULT TRUE, -- Allow for explicit deny
    granted_by INT,
    granted_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    expires_at TIMESTAMP NULL,
    
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY (permission_id) REFERENCES permissions(id) ON DELETE CASCADE,
    FOREIGN KEY (granted_by) REFERENCES users(id) ON DELETE SET NULL,
    UNIQUE KEY unique_user_permission (user_id, permission_id),
    INDEX idx_user_permissions_user (user_id),
    INDEX idx_user_permissions_permission (permission_id)
);

-- ============================================================================
-- 9. USER_SESSIONS TABLE (Session management)
-- ============================================================================
CREATE TABLE user_sessions (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL,
    session_token VARCHAR(255) UNIQUE NOT NULL,
    refresh_token VARCHAR(255) UNIQUE,
    ip_address VARCHAR(45),
    user_agent TEXT,
    device_info JSON,
    location JSON,
    is_active BOOLEAN DEFAULT TRUE,
    expires_at TIMESTAMP NOT NULL,
    last_activity_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
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
(1, 'Super Administrator', 'super_admin', 'Full system access', 100, TRUE, '#FF0000'),
(1, 'Administrator', 'admin', 'Administrative access', 90, TRUE, '#FF6600'),
(1, 'Manager', 'manager', 'Management level access', 70, TRUE, '#0066FF'),
(1, 'Editor', 'editor', 'Content editing access', 50, TRUE, '#009900'),
(1, 'User', 'user', 'Basic user access', 10, TRUE, '#666666'),
(1, 'Guest', 'guest', 'Read-only access', 0, TRUE, '#CCCCCC');

-- Insert system permissions
INSERT INTO permissions (name, slug, description, module, action, resource, is_system_permission) VALUES
-- User management permissions
('Create Users', 'create_users', 'Can create new users', 'users', 'create', 'user', TRUE),
('Read Users', 'read_users', 'Can view user information', 'users', 'read', 'user', TRUE),
('Update Users', 'update_users', 'Can modify user information', 'users', 'update', 'user', TRUE),
('Delete Users', 'delete_users', 'Can delete users', 'users', 'delete', 'user', TRUE),
('Manage User Roles', 'manage_user_roles', 'Can assign/remove user roles', 'users', 'manage', 'roles', TRUE),

-- Role management permissions
('Create Roles', 'create_roles', 'Can create new roles', 'roles', 'create', 'role', TRUE),
('Read Roles', 'read_roles', 'Can view role information', 'roles', 'read', 'role', TRUE),
('Update Roles', 'update_roles', 'Can modify role information', 'roles', 'update', 'role', TRUE),
('Delete Roles', 'delete_roles', 'Can delete roles', 'roles', 'delete', 'role', TRUE),
('Manage Role Permissions', 'manage_role_permissions', 'Can assign/remove role permissions', 'roles', 'manage', 'permissions', TRUE),

-- Permission management permissions
('Create Permissions', 'create_permissions', 'Can create new permissions', 'permissions', 'create', 'permission', TRUE),
('Read Permissions', 'read_permissions', 'Can view permission information', 'permissions', 'read', 'permission', TRUE),
('Update Permissions', 'update_permissions', 'Can modify permission information', 'permissions', 'update', 'permission', TRUE),
('Delete Permissions', 'delete_permissions', 'Can delete permissions', 'permissions', 'delete', 'permission', TRUE),

-- Organization management permissions
('Create Organizations', 'create_organizations', 'Can create new organizations', 'organizations', 'create', 'organization', TRUE),
('Read Organizations', 'read_organizations', 'Can view organization information', 'organizations', 'read', 'organization', TRUE),
('Update Organizations', 'update_organizations', 'Can modify organization information', 'organizations', 'update', 'organization', TRUE),
('Delete Organizations', 'delete_organizations', 'Can delete organizations', 'organizations', 'delete', 'organization', TRUE),

-- Session management permissions
('Manage Sessions', 'manage_sessions', 'Can manage user sessions', 'sessions', 'manage', 'session', TRUE),
('View System Logs', 'view_system_logs', 'Can view system activity logs', 'system', 'read', 'logs', TRUE),

-- Profile management permissions
('Update Own Profile', 'update_own_profile', 'Can update own profile', 'profile', 'update', 'own', TRUE),
('View Own Profile', 'view_own_profile', 'Can view own profile', 'profile', 'read', 'own', TRUE);

-- Assign permissions to super admin role
INSERT INTO role_permissions (role_id, permission_id, granted_by)
SELECT 1, id, NULL FROM permissions WHERE is_system_permission = TRUE;

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

-- ============================================================================
-- 11. USEFUL VIEWS
-- ============================================================================

-- View: User roles and permissions
CREATE VIEW user_effective_permissions AS
SELECT DISTINCT
    u.id as user_id,
    u.username,
    u.email,
    p.id as permission_id,
    p.slug as permission_slug,
    p.name as permission_name,
    'role' as source_type,
    r.name as source_name
FROM users u
JOIN user_roles ur ON u.id = ur.user_id AND ur.is_active = TRUE
JOIN role_permissions rp ON ur.role_id = rp.role_id AND rp.granted = TRUE
JOIN permissions p ON rp.permission_id = p.id
JOIN roles r ON ur.role_id = r.id
WHERE u.deleted_at IS NULL
UNION
SELECT DISTINCT
    u.id as user_id,
    u.username,
    u.email,
    p.id as permission_id,
    p.slug as permission_slug,
    p.name as permission_name,
    'direct' as source_type,
    'Direct Assignment' as source_name
FROM users u
JOIN user_permissions up ON u.id = up.user_id AND up.granted = TRUE
JOIN permissions p ON up.permission_id = p.id
WHERE u.deleted_at IS NULL;

-- View: User summary
CREATE VIEW user_summary AS
SELECT 
    u.id,
    u.username,
    u.email,
    u.first_name,
    u.last_name,
    u.status,
    u.last_login_at,
    o.name as organization_name,
    GROUP_CONCAT(r.name ORDER BY r.level DESC) as roles,
    COUNT(DISTINCT ur.role_id) as role_count,
    u.created_at
FROM users u
LEFT JOIN organizations o ON u.organization_id = o.id
LEFT JOIN user_roles ur ON u.id = ur.user_id AND ur.is_active = TRUE
LEFT JOIN roles r ON ur.role_id = r.id
WHERE u.deleted_at IS NULL
GROUP BY u.id, u.username, u.email, u.first_name, u.last_name, u.status, u.last_login_at, o.name, u.created_at;

-- ============================================================================
-- 12. USEFUL STORED PROCEDURES
-- ============================================================================

DELIMITER //

-- Procedure: Check if user has permission
CREATE PROCEDURE CheckUserPermission(
    IN p_user_id INT,
    IN p_permission_slug VARCHAR(100),
    OUT p_has_permission BOOLEAN
)
BEGIN
    DECLARE permission_count INT DEFAULT 0;
    
    SELECT COUNT(*) INTO permission_count
    FROM user_effective_permissions
    WHERE user_id = p_user_id AND permission_slug = p_permission_slug;
    
    SET p_has_permission = (permission_count > 0);
END //

-- Procedure: Get user permissions
CREATE PROCEDURE GetUserPermissions(
    IN p_user_id INT
)
BEGIN
    SELECT DISTINCT
        permission_slug,
        permission_name,
        source_type,
        source_name
    FROM user_effective_permissions
    WHERE user_id = p_user_id
    ORDER BY permission_name;
END //

-- Procedure: Assign role to user
CREATE PROCEDURE AssignRoleToUser(
    IN p_user_id INT,
    IN p_role_id INT,
    IN p_assigned_by INT,
    IN p_expires_at TIMESTAMP
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;
    
    START TRANSACTION;
    
    INSERT INTO user_roles (user_id, role_id, assigned_by, expires_at)
    VALUES (p_user_id, p_role_id, p_assigned_by, p_expires_at)
    ON DUPLICATE KEY UPDATE
        assigned_by = p_assigned_by,
        assigned_at = CURRENT_TIMESTAMP,
        expires_at = p_expires_at,
        is_active = TRUE;
    
    COMMIT;
END //

-- Procedure: Create user with default role
CREATE PROCEDURE CreateUserWithRole(
    IN p_organization_id INT,
    IN p_username VARCHAR(50),
    IN p_email VARCHAR(255),
    IN p_password_hash VARCHAR(255),
    IN p_first_name VARCHAR(100),
    IN p_last_name VARCHAR(100),
    IN p_role_slug VARCHAR(100),
    IN p_created_by INT,
    OUT p_user_id INT
)
BEGIN
    DECLARE role_id INT;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;
    
    START TRANSACTION;
    
    -- Insert user
    INSERT INTO users (organization_id, username, email, password_hash, first_name, last_name, created_by)
    VALUES (p_organization_id, p_username, p_email, p_password_hash, p_first_name, p_last_name, p_created_by);
    
    SET p_user_id = LAST_INSERT_ID();
    
    -- Create user profile
    INSERT INTO user_profiles (user_id) VALUES (p_user_id);
    
    -- Assign default role
    SELECT id INTO role_id FROM roles WHERE slug = p_role_slug AND organization_id = p_organization_id LIMIT 1;
    
    IF role_id IS NOT NULL THEN
        INSERT INTO user_roles (user_id, role_id, assigned_by)
        VALUES (p_user_id, role_id, p_created_by);
    END IF;
    
    COMMIT;
END //

DELIMITER ;

-- ============================================================================
-- 13. INDEXES FOR PERFORMANCE
-- ============================================================================

-- Additional indexes for better performance
CREATE INDEX idx_user_sessions_last_activity ON user_sessions(last_activity_at);
CREATE INDEX idx_users_last_login ON users(last_login_at);
CREATE INDEX idx_user_roles_expires ON user_roles(expires_at);
CREATE INDEX idx_user_permissions_expires ON user_permissions(expires_at);

-- ============================================================================
-- 14. SAMPLE QUERIES
-- ============================================================================

-- Get all permissions for a specific user
-- SELECT * FROM user_effective_permissions WHERE user_id = 1;

-- Get all users with a specific role
-- SELECT u.*, r.name as role_name 
-- FROM users u 
-- JOIN user_roles ur ON u.id = ur.user_id 
-- JOIN roles r ON ur.role_id = r.id 
-- WHERE r.slug = 'admin' AND ur.is_active = TRUE;

-- Get role hierarchy
-- SELECT r1.name as role, r2.name as can_manage 
-- FROM roles r1 
-- CROSS JOIN roles r2 
-- WHERE r1.level >= r2.level 
-- ORDER BY r1.level DESC, r2.level DESC;

-- Clean up expired sessions
-- DELETE FROM user_sessions WHERE expires_at < CURRENT_TIMESTAMP;

-- Clean up expired user roles
-- UPDATE user_roles SET is_active = FALSE WHERE expires_at < CURRENT_TIMESTAMP AND expires_at IS NOT NULL;

-- ============================================================================
-- END OF SCRIPT
-- ============================================================================