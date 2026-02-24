-- Check if newly created employees have JobRoleId assigned
SELECT 
    Id,
    FullName,
    EmployeeNumber,
    DepartmentID,
    JobRoleId,
    IsActive,
    CASE 
        WHEN JobRoleId IS NULL THEN 'NO JOB ROLE'
        ELSE 'HAS JOB ROLE'
    END AS JobRoleStatus
FROM Employe
WHERE IsActive = 1
ORDER BY Id DESC;

-- Check job role details for employees
SELECT 
    e.Id AS EmployeeId,
    e.FullName,
    e.EmployeeNumber,
    e.JobRoleId,
    jr.RoleName AS JobRole,
    d.DepartmentName
FROM Employe e
LEFT JOIN JobRoles jr ON e.JobRoleId = jr.Id
LEFT JOIN Departments d ON e.DepartmentID = d.Id
WHERE e.IsActive = 1
ORDER BY e.Id DESC;

-- Count employees with and without job roles
SELECT 
    CASE 
        WHEN JobRoleId IS NULL THEN 'Without Job Role'
        ELSE 'With Job Role'
    END AS Status,
    COUNT(*) AS Count
FROM Employe
WHERE IsActive = 1
GROUP BY CASE WHEN JobRoleId IS NULL THEN 'Without Job Role' ELSE 'With Job Role' END;
