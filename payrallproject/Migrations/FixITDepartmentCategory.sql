-- =============================================
-- Fix IT Department - Assign Employee Category
-- =============================================
-- This script fixes departments that don't have an employee category assigned

-- First, let's check which departments are missing categories
SELECT 
    Id, 
    DepartmentName, 
    EmployeeCategoriesId,
    CASE 
        WHEN EmployeeCategoriesId IS NULL THEN '❌ MISSING CATEGORY'
        ELSE '✓ Has Category'
    END AS Status
FROM Departments 
WHERE IsActive = 1
ORDER BY DepartmentName;

-- Get the Staff category ID
DECLARE @StaffCategoryId INT = (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff');

-- Update IT department to have Staff category
UPDATE Departments 
SET EmployeeCategoriesId = @StaffCategoryId
WHERE DepartmentName = 'IT' 
  AND (EmployeeCategoriesId IS NULL OR EmployeeCategoriesId = 0)
  AND IsActive = 1;

-- Update any other departments without categories (set them to Staff by default)
UPDATE Departments 
SET EmployeeCategoriesId = @StaffCategoryId
WHERE EmployeeCategoriesId IS NULL 
  AND IsActive = 1;

-- Verify the fix
SELECT 
    d.Id, 
    d.DepartmentName, 
    d.EmployeeCategoriesId,
    ec.CategoryName AS AssignedCategory,
    CASE 
        WHEN d.EmployeeCategoriesId IS NULL THEN '❌ STILL MISSING'
        ELSE '✓ FIXED'
    END AS Status
FROM Departments d
LEFT JOIN EmployeeCategories ec ON d.EmployeeCategoriesId = ec.Id
WHERE d.IsActive = 1
ORDER BY d.DepartmentName;

-- Show which employees are in departments that were fixed
SELECT 
    e.Id AS EmployeeId,
    e.FullName,
    e.DepartmentID,
    d.DepartmentName,
    d.EmployeeCategoriesId,
    ec.CategoryName AS DepartmentCategory
FROM Employe e
INNER JOIN Departments d ON e.DepartmentID = d.Id
LEFT JOIN EmployeeCategories ec ON d.EmployeeCategoriesId = ec.Id
WHERE e.IsActive = 1
ORDER BY d.DepartmentName, e.FullName;
