# =============================================
# Fix IT Department Category - PowerShell Script
# =============================================
# This script applies the SQL fix to assign employee categories to departments

Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "    Fix IT Department - Assign Employee Category" -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host ""

# Get connection string from appsettings.json
$appSettingsPath = "D:\Intern\Pay Roll\payrallbackend\payrallproject\appsettings.json"

if (Test-Path $appSettingsPath) {
    $appSettings = Get-Content $appSettingsPath | ConvertFrom-Json
    $connectionString = $appSettings.ConnectionStrings.dbstring
    
    Write-Host "✓ Found connection string" -ForegroundColor Green
    Write-Host ""
} else {
    Write-Host "❌ Could not find appsettings.json" -ForegroundColor Red
    exit 1
}

# Path to SQL script
$sqlScriptPath = "D:\Intern\Pay Roll\payrallbackend\payrallproject\Migrations\FixITDepartmentCategory.sql"

if (-not (Test-Path $sqlScriptPath)) {
    Write-Host "❌ Could not find SQL script at: $sqlScriptPath" -ForegroundColor Red
    exit 1
}

Write-Host "Executing SQL fix script..." -ForegroundColor Yellow
Write-Host ""

# Execute the SQL script
try {
    # Using SqlCmd (if installed)
    if (Get-Command sqlcmd -ErrorAction SilentlyContinue) {
        # Parse connection string to get server and database
        if ($connectionString -match "Server=([^;]+);.*Database=([^;]+)") {
            $server = $matches[1]
            $database = $matches[2]
            
            Write-Host "Server: $server" -ForegroundColor Gray
            Write-Host "Database: $database" -ForegroundColor Gray
            Write-Host ""
            
            sqlcmd -S $server -d $database -i $sqlScriptPath -E
            
            Write-Host ""
            Write-Host "✓ SQL script executed successfully!" -ForegroundColor Green
        } else {
            Write-Host "❌ Could not parse connection string" -ForegroundColor Red
        }
    } else {
        Write-Host ""
        Write-Host "⚠️  SqlCmd not found. Please run the SQL script manually:" -ForegroundColor Yellow
        Write-Host ""
        Write-Host "1. Open SQL Server Management Studio (SSMS)" -ForegroundColor Cyan
        Write-Host "2. Connect to your database" -ForegroundColor Cyan
        Write-Host "3. Open this file: $sqlScriptPath" -ForegroundColor Cyan
        Write-Host "4. Execute the script (F5)" -ForegroundColor Cyan
        Write-Host ""
        Write-Host "OR copy-paste this SQL query:" -ForegroundColor Yellow
        Write-Host ""
        Get-Content $sqlScriptPath | Write-Host -ForegroundColor White
    }
} catch {
    Write-Host ""
    Write-Host "❌ Error executing SQL script: $_" -ForegroundColor Red
    Write-Host ""
    Write-Host "Please run the SQL script manually in SSMS:" -ForegroundColor Yellow
    Write-Host $sqlScriptPath -ForegroundColor Cyan
}

Write-Host ""
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "After running this fix, restart your backend server" -ForegroundColor Yellow
Write-Host "==================================================" -ForegroundColor Cyan
