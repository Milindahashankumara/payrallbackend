-- Verify Employe table structure
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Employe'
ORDER BY ORDINAL_POSITION;

-- Check if JobRoleId column exists
SELECT COUNT(*) as JobRoleIdExists
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Employe' AND COLUMN_NAME = 'JobRoleId';

-- Check recent employees
SELECT TOP 10 
    Id, 
    FullName, 
    EmployeeNumber, 
    IsActive,
    DepartmentID,
    JobRoleId,
    EmployeeCategoriesID
FROM Employe
ORDER BY Id DESC;

-- Count active vs inactive employees
SELECT 
    IsActive,
    COUNT(*) as Count
FROM Employe
GROUP BY IsActive;
