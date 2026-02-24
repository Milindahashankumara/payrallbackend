-- =============================================
-- Department Structure Setup (Run this FIRST if departments don't exist)
-- =============================================

-- Check if departments exist
SELECT * FROM Departments WHERE IsActive = 1;

-- If you need to create the recommended department structure, run this:

-- Note: Replace EmployeeCategoriesId values with actual IDs from your EmployeeCategories table
-- To find your category IDs, run: SELECT * FROM EmployeeCategories;

DECLARE @StaffCategoryId INT = (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff');
DECLARE @CasualCategoryId INT = (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Casual');

-- Insert Departments (if they don't exist)
IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentName = 'Operations')
BEGIN
    INSERT INTO Departments (DepartmentName, Description, EmployeeCategoriesId, IsActive)
    VALUES ('Operations', 'Core business operations including fuel pumping and customer service', @StaffCategoryId, 1);
END

IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentName = 'Security')
BEGIN
    INSERT INTO Departments (DepartmentName, Description, EmployeeCategoriesId, IsActive)
    VALUES ('Security', 'On-site security, asset protection, and safety management', @StaffCategoryId, 1);
END

IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentName = 'Finance & Accounts')
BEGIN
    INSERT INTO Departments (DepartmentName, Description, EmployeeCategoriesId, IsActive)
    VALUES ('Finance & Accounts', 'Financial management, accounting, and payroll processing', @StaffCategoryId, 1);
END

IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentName = 'HR')
BEGIN
    INSERT INTO Departments (DepartmentName, Description, EmployeeCategoriesId, IsActive)
    VALUES ('HR', 'Human resources, recruitment, and employee management', @StaffCategoryId, 1);
END

IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentName = 'IT Department')
BEGIN
    INSERT INTO Departments (DepartmentName, Description, EmployeeCategoriesId, IsActive)
    VALUES ('IT Department', 'Information technology, systems administration, and technical support', @StaffCategoryId, 1);
END

IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentName = 'Maintenance & Facilities')
BEGIN
    INSERT INTO Departments (DepartmentName, Description, EmployeeCategoriesId, IsActive)
    VALUES ('Maintenance & Facilities', 'Building maintenance, equipment repair, and facility management', @StaffCategoryId, 1);
END

-- Verify departments were created
SELECT * FROM Departments WHERE IsActive = 1 ORDER BY DepartmentName;

-- Show department IDs for reference
SELECT 
    Id,
    DepartmentName,
    EmployeeCategoriesId,
    (SELECT CategoryName FROM EmployeeCategories WHERE Id = Departments.EmployeeCategoriesId) AS CategoryName
FROM Departments 
WHERE IsActive = 1
ORDER BY DepartmentName;
