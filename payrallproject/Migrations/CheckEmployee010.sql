-- Check the specific employee testnew-1 (Employee Number 010)
SELECT 
    e.Id,
    e.FullName,
    e.EmployeeNumber,
    e.DepartmentID,
    e.JobRoleId,
    d.DepartmentName,
    jr.RoleName AS JobRole,
    e.IsActive
FROM Employe e
LEFT JOIN Departments d ON e.DepartmentID = d.Id
LEFT JOIN JobRoles jr ON e.JobRoleId = jr.Id
WHERE e.EmployeeNumber = '010';

-- If JobRoleId is NULL, you need to update it
-- Example update (replace X with the correct JobRoleId):
-- UPDATE Employe SET JobRoleId = X WHERE EmployeeNumber = '010';

-- To see available job roles for the employee's department:
SELECT 
    jr.Id AS JobRoleId,
    jr.RoleName,
    d.DepartmentName
FROM JobRoles jr
INNER JOIN Departments d ON jr.DepartmentId = d.Id
WHERE d.Id = (SELECT DepartmentID FROM Employe WHERE EmployeeNumber = '010');
