-- Fix Employee 010 (testnew-1) - Assign a Job Role
-- First, check what department they're in
SELECT 
    e.Id,
    e.FullName,
    e.EmployeeNumber,
    e.DepartmentID,
    d.DepartmentName,
    e.JobRoleId
FROM Employe e
LEFT JOIN Departments d ON e.DepartmentID = d.Id
WHERE e.EmployeeNumber = '010';

-- Show available job roles for their department (test-2)
SELECT 
    jr.Id AS JobRoleId,
    jr.RoleName,
    d.DepartmentName
FROM JobRoles jr
INNER JOIN Departments d ON jr.DepartmentId = d.Id
WHERE d.DepartmentName = 'test-2';

-- If no job roles exist for 'test-2' department, you need to create one first
-- Example: Create a job role for test-2 department
-- INSERT INTO JobRoles (RoleName, DepartmentId, EmployeeCategoriesId, IsActive)
-- VALUES ('Test Role', (SELECT Id FROM Departments WHERE DepartmentName = 'test-2'), (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 1);

-- After creating the job role, update the employee
-- UPDATE Employe 
-- SET JobRoleId = (SELECT TOP 1 Id FROM JobRoles WHERE DepartmentId = (SELECT Id FROM Departments WHERE DepartmentName = 'test-2'))
-- WHERE EmployeeNumber = '010';

-- Verify the update
-- SELECT 
--     e.Id,
--     e.FullName,
--     e.EmployeeNumber,
--     e.JobRoleId,
--     jr.RoleName AS JobRole
-- FROM Employe e
-- LEFT JOIN JobRoles jr ON e.JobRoleId = jr.Id
-- WHERE e.EmployeeNumber = '010';
