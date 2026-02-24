-- =============================================
-- Job Roles Feature - Database Migration Script
-- =============================================

-- Step 1: Create JobRoles Table
CREATE TABLE JobRoles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(255) NOT NULL,
    DepartmentId INT NULL,
    EmployeeCategoriesId INT NULL,
    IsActive BIT DEFAULT 1,
    CONSTRAINT FK_JobRoles_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
    CONSTRAINT FK_JobRoles_EmployeeCategories FOREIGN KEY (EmployeeCategoriesId) REFERENCES EmployeeCategories(Id)
);

-- Step 2: Add JobRoleId column to Employe table
ALTER TABLE Employe
ADD JobRoleId INT NULL;

-- Step 3: Add foreign key constraint
ALTER TABLE Employe
ADD CONSTRAINT FK_Employe_JobRoles FOREIGN KEY (JobRoleId) REFERENCES JobRoles(Id);

-- Step 4: Insert sample data for Operations Department
INSERT INTO JobRoles (RoleName, DepartmentId, EmployeeCategoriesId, IsActive)
VALUES 
    ('Station Manager', (SELECT Id FROM Departments WHERE DepartmentName = 'Operations'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('Shift Supervisor', (SELECT Id FROM Departments WHERE DepartmentName = 'Operations'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('Cashier', (SELECT Id FROM Departments WHERE DepartmentName = 'Operations'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('Pumper', (SELECT Id FROM Departments WHERE DepartmentName = 'Operations'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Casual'), 1),
    ('Trainee Pumper', (SELECT Id FROM Departments WHERE DepartmentName = 'Operations'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Casual'), 1);

-- Step 5: Insert sample data for Security Department
INSERT INTO JobRoles (RoleName, DepartmentId, EmployeeCategoriesId, IsActive)
VALUES 
    ('Chief Security Officer', (SELECT Id FROM Departments WHERE DepartmentName = 'Security'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('Security Guard', (SELECT Id FROM Departments WHERE DepartmentName = 'Security'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Casual'), 1),
    ('Security Trainee', (SELECT Id FROM Departments WHERE DepartmentName = 'Security'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Casual'), 1);

-- Step 6: Insert sample data for Finance & Accounts Department
INSERT INTO JobRoles (RoleName, DepartmentId, EmployeeCategoriesId, IsActive)
VALUES 
    ('Finance Manager', (SELECT Id FROM Departments WHERE DepartmentName = 'Finance & Accounts'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('Accountant', (SELECT Id FROM Departments WHERE DepartmentName = 'Finance & Accounts'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('Payroll Clerk', (SELECT Id FROM Departments WHERE DepartmentName = 'Finance & Accounts'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('Bookkeeper', (SELECT Id FROM Departments WHERE DepartmentName = 'Finance & Accounts'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1);

-- Step 7: Insert sample data for HR Department
INSERT INTO JobRoles (RoleName, DepartmentId, EmployeeCategoriesId, IsActive)
VALUES 
    ('HR Manager', (SELECT Id FROM Departments WHERE DepartmentName = 'HR'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('HR Executive', (SELECT Id FROM Departments WHERE DepartmentName = 'HR'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1);

-- Step 8: Insert sample data for IT Department
INSERT INTO JobRoles (RoleName, DepartmentId, EmployeeCategoriesId, IsActive)
VALUES 
    ('IT Manager', (SELECT Id FROM Departments WHERE DepartmentName = 'IT Department'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('Systems Administrator', (SELECT Id FROM Departments WHERE DepartmentName = 'IT Department'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('IT Support Technician', (SELECT Id FROM Departments WHERE DepartmentName = 'IT Department'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1);

-- Step 9: Insert sample data for Maintenance & Facilities Department
INSERT INTO JobRoles (RoleName, DepartmentId, EmployeeCategoriesId, IsActive)
VALUES 
    ('Maintenance Supervisor', (SELECT Id FROM Departments WHERE DepartmentName = 'Maintenance & Facilities'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1),
    ('Maintenance Technician', (SELECT Id FROM Departments WHERE DepartmentName = 'Maintenance & Facilities'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Casual'), 1),
    ('Cleaner / Janitor', (SELECT Id FROM Departments WHERE DepartmentName = 'Maintenance & Facilities'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Casual'), 1);

-- Verification Queries
SELECT * FROM JobRoles ORDER BY DepartmentId, RoleName;
SELECT COUNT(*) AS TotalJobRoles FROM JobRoles WHERE IsActive = 1;
