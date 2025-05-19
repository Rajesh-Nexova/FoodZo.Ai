1. Multi-tenant Support

Organizations table for supporting multiple tenants
Subscription plans and user limits

2. Core User Management

Users table with authentication fields
User profiles for extended information
Account status management
Password security features

3. Role-Based Access Control

Roles with hierarchical levels
Permissions with modular organization
Many-to-many relationships between users/roles and roles/permissions
Direct user permissions for fine-grained control

4. Security Features

Session management with device tracking
Failed login attempt tracking
Account locking mechanism
Two-factor authentication support
Soft delete functionality

5. Advanced Features

Views for easy permission checking
Stored procedures for common operations
Performance indexes
Sample queries for common tasks

Main Tables:

organizations - Multi-tenant support
users - Core user information
user_profiles - Extended user data
roles - Role definitions
permissions - Permission definitions
user_roles - User-role assignments
role_permissions - Role-permission assignments
user_permissions - Direct user permissions
user_sessions - Session management

Pre-configured Data:
The script includes default roles (Super Admin, Admin, Manager, Editor, User, Guest) and system permissions for common operations like user management, role management, and profile management.
Useful Procedures:

CheckUserPermission() - Check if a user has a specific permission
GetUserPermissions() - Get all permissions for a user
AssignRoleToUser() - Assign a role to a user
CreateUserWithRole() - Create a new user with a default role