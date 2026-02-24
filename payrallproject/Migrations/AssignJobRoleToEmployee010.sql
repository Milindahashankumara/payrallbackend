-- =============================================
-- Assign Job Role to Employee 010 (testnew-1)
-- Employee is in IT Department
-- =============================================

-- Step 1: Check current status of employee 010
SELECT 
    e.Id,
    e.FullName,
    e.EmployeeNumber,
    e.DepartmentID,
    d.DepartmentName,
    e.JobRoleId,
    jr.RoleName AS CurrentJobRole
FROM Employe e
LEFT JOIN Departments d ON e.DepartmentID = d.Id
LEFT JOIN JobRoles jr ON e.JobRoleId = jr.Id
WHERE e.EmployeeNumber = '010';

-- Step 2: Show available job roles for IT Department
SELECT 
    jr.Id AS JobRoleId,
    jr.RoleName,
    d.DepartmentName,
    ec.CategoryName
FROM JobRoles jr
INNER JOIN Departments d ON jr.DepartmentId = d.Id
LEFT JOIN EmployeeCategories ec ON jr.EmployeeCategoriesId = ec.Id
WHERE d.DepartmentName = 'IT Department'
AND jr.IsActive = 1;

-- Step 3: Assign 'IT Manager' role to employee 010
UPDATE Employe 
SET JobRoleId = (
    SELECT jr.Id 
    FROM JobRoles jr
    INNER JOIN Departments d ON jr.DepartmentId = d.Id
    WHERE d.DepartmentName = 'IT Department' 
    AND jr.RoleName = 'IT Manager'
)
WHERE EmployeeNumber = '010';

-- Step 4: Verify the update
SELECT 
    e.Id,
    e.FullName,
    e.EmployeeNumber,
    d.DepartmentName,
    jr.RoleName AS JobRole,
    e.IsActive
FROM Employe e
LEFT JOIN Departments d ON e.DepartmentID = d.Id
LEFT JOIN JobRoles jr ON e.JobRoleId = jr.Id
WHERE e.EmployeeNumber = '010';

-- SUCCESS! Employee 010 now has IT Manager role assigned
