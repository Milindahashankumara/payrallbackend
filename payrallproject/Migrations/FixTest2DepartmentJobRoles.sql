-- =============================================
-- Fix Job Role for Employee 010 (testnew-1)
-- =============================================

-- Step 1: Check if test-2 department exists and get its ID
SELECT Id, DepartmentName, EmployeeCategoriesId 
FROM Departments 
WHERE DepartmentName = 'test-2';

-- Step 2: Create job roles for test-2 department
-- (Adjust the role names as needed for your business)
INSERT INTO JobRoles (RoleName, DepartmentId, EmployeeCategoriesId, IsActive)
VALUES 
    ('Test Manager', 
     (SELECT Id FROM Departments WHERE DepartmentName = 'test-2'), 
     (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 
     1),
    ('Test Supervisor', 
     (SELECT Id FROM Departments WHERE DepartmentName = 'test-2'), 
     (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 
     1),
    ('Test Officer', 
     (SELECT Id FROM Departments WHERE DepartmentName = 'test-2'), 
     (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Staff'), 
     1),
    ('Test Assistant', 
     (SELECT Id FROM Departments WHERE DepartmentName = 'test-2'), 
     (SELECT Id FROM EmployeeCategories WHERE CategoryName = 'Casual'), 
     1);

-- Step 3: Verify job roles were created
SELECT jr.Id, jr.RoleName, d.DepartmentName
FROM JobRoles jr
INNER JOIN Departments d ON jr.DepartmentId = d.Id
WHERE d.DepartmentName = 'test-2';

-- Step 4: Update Employee 010 with a job role (choose appropriate role)
-- Update with 'Test Manager' role
UPDATE Employe 
SET JobRoleId = (
    SELECT TOP 1 jr.Id 
    FROM JobRoles jr
    INNER JOIN Departments d ON jr.DepartmentId = d.Id
    WHERE d.DepartmentName = 'test-2' 
    AND jr.RoleName = 'Test Manager'
)
WHERE EmployeeNumber = '010';

-- Step 5: Verify the employee now has a job role
SELECT 
    e.Id,
    e.FullName,
    e.EmployeeNumber,
    e.DepartmentID,
    d.DepartmentName,
    e.JobRoleId,
    jr.RoleName AS JobRole
FROM Employe e
LEFT JOIN Departments d ON e.DepartmentID = d.Id
LEFT JOIN JobRoles jr ON e.JobRoleId = jr.Id
WHERE e.EmployeeNumber = '010';

-- Step 6: Update any other employees in test-2 department without job roles
UPDATE Employe 
SET JobRoleId = (
    SELECT TOP 1 jr.Id 
    FROM JobRoles jr
    WHERE jr.DepartmentId = DepartmentID
    ORDER BY jr.Id
)
WHERE DepartmentID = (SELECT Id FROM Departments WHERE DepartmentName = 'test-2')
AND JobRoleId IS NULL
AND IsActive = 1;

-- Step 7: Verify all employees in test-2 now have job roles
SELECT 
    e.Id,
    e.FullName,
    e.EmployeeNumber,
    d.DepartmentName,
    jr.RoleName AS JobRole
FROM Employe e
LEFT JOIN Departments d ON e.DepartmentID = d.Id
LEFT JOIN JobRoles jr ON e.JobRoleId = jr.Id
WHERE d.DepartmentName = 'test-2'
AND e.IsActive = 1;
