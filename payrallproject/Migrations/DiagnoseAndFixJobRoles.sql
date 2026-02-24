-- =============================================
-- Diagnose Job Role Issues
-- =============================================

-- Check all active employees and their job roles
SELECT 
    e.Id,
    e.FullName,
    e.EmployeeNumber,
    e.DepartmentID,
    d.DepartmentName,
    e.JobRoleId,
    jr.RoleName AS JobRole,
    CASE 
        WHEN e.JobRoleId IS NULL THEN 'MISSING JOB ROLE'
        ELSE 'HAS JOB ROLE'
    END AS Status
FROM Employe e
LEFT JOIN Departments d ON e.DepartmentID = d.Id
LEFT JOIN JobRoles jr ON e.JobRoleId = jr.Id
WHERE e.IsActive = 1
ORDER BY e.Id DESC;

-- Count employees by job role status
SELECT 
    CASE 
        WHEN JobRoleId IS NULL THEN 'Without Job Role'
        ELSE 'With Job Role'
    END AS Status,
    COUNT(*) AS EmployeeCount
FROM Employe
WHERE IsActive = 1
GROUP BY CASE WHEN JobRoleId IS NULL THEN 'Without Job Role' ELSE 'With Job Role' END;

-- Show all available job roles by department
SELECT 
    d.DepartmentName,
    jr.Id AS JobRoleId,
    jr.RoleName,
    ec.CategoryName
FROM JobRoles jr
INNER JOIN Departments d ON jr.DepartmentId = d.Id
LEFT JOIN EmployeeCategories ec ON jr.EmployeeCategoriesId = ec.Id
WHERE jr.IsActive = 1
ORDER BY d.DepartmentName, jr.RoleName;

-- Fix all employees without job roles (assign first available role for their department)
-- UNCOMMENT TO RUN:
/*
UPDATE e
SET e.JobRoleId = (
    SELECT TOP 1 jr.Id
    FROM JobRoles jr
    WHERE jr.DepartmentId = e.DepartmentID
    AND jr.IsActive = 1
    ORDER BY jr.Id
)
FROM Employe e
WHERE e.JobRoleId IS NULL
AND e.DepartmentID IS NOT NULL
AND e.IsActive = 1;
*/

-- Verify after fix
-- SELECT 
--     e.Id,
--     e.FullName,
--     e.EmployeeNumber,
--     d.DepartmentName,
--     jr.RoleName AS JobRole
-- FROM Employe e
-- LEFT JOIN Departments d ON e.DepartmentID = d.Id
-- LEFT JOIN JobRoles jr ON e.JobRoleId = jr.Id
-- WHERE e.IsActive = 1
-- ORDER BY e.Id DESC;
