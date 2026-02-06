using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.SalaryReportService
{
    public class SalaryReportService : ISalaryReportService
    {
        private readonly AuthDbContext _dbContext;

        public SalaryReportService(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SalaryReport> GenerateAndStoreSalaryReportAsync(SalaryReportDto dto)
        {
            if (dto.FromDate > dto.ToDate)
            {
                throw new Exception("FromDate cannot be after ToDate");
            }

            var employee = await _dbContext.Employe
                .Include(e => e.EmployeeCategories)
                .FirstOrDefaultAsync(e => e.Id == dto.EmployeeId);

            if (employee == null)
                throw new Exception($"Employee with ID {dto.EmployeeId} not found.");

            if (employee.DepartmentID == null)
                throw new Exception($"Employee {employee.FullName} does not have a department assigned.");

            var department = await _dbContext.Departments
                .FirstOrDefaultAsync(e => e.Id == employee.DepartmentID);

            if (department == null)
                throw new Exception($"Department with ID {employee.DepartmentID} not found.");

            if (department.EmployeeCategoriesId == null)
                throw new Exception($"Department {department.DepartmentName} does not have an employee category assigned.");

            var categ = await _dbContext.EmployeeCategories
                .FirstOrDefaultAsync(e => e.Id == department.EmployeeCategoriesId);

            if (categ == null)
                throw new Exception($"Employee category with ID {department.EmployeeCategoriesId} not found.");

            var ot1rate = await _dbContext.OT
                .FirstOrDefaultAsync(e => e.Id == 2);
            var ot2rate = await _dbContext.OT
                .FirstOrDefaultAsync(e => e.Id == 1);

            if (ot1rate == null || ot2rate == null)
                throw new Exception("OT rates not configured properly in the system.");

            if (employee.EmployeeCategories == null)
                throw new Exception("Employee category not found.");

            var isDaySalaryBased = employee.EmployeeCategories.DaySalarybased ?? false;

            var report = new SalaryReport
            {
                EmployeeId = dto.EmployeeId,
                Year = dto.Year,
                Month = dto.Month,
                WorkingDays = dto.WorkingDays,
                Incentives = dto.Incentives,
                Bonus = dto.Bonus,
                SalaryAdvances = dto.SalaryAdvances,
                Loans = dto.Loans,
                OtherDeductions = dto.OtherDeductions,
                LeaveDays = dto.LeaveDays,
                HalfDays = dto.HalfDays,
                NoPayDays = dto.NoPayDays,
                Ot1Hours = dto.Ot1Hours,
                Ot2Hours = dto.Ot2Hours,
                IsDaySalaryBased = isDaySalaryBased,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                AttendanceAllowance = dto.AttendanceAllowance,
                TransportAllowance = dto.TransportAllowance,
                FoodAllowance = dto.FoodAllowance,
                MedicalAllowance = dto.MedicalAllowance,
                InternetAllowance = dto.InternetAllowance,
            };

            report.EmployeeName = employee.FullName;
            report.EmployeeNumber = employee.EmployeeNumber;
            report.DepartmentName = department.DepartmentName;
            report.CategaryName = categ.CategoryName;

            if (isDaySalaryBased)
            {
                report.DaySalary = employee.DaySalary;
                report.KpiRate = employee.KpiRate;
                report.Wages = (employee.DaySalary ?? 0) * dto.WorkingDays;

                decimal kr = employee.KpiRate ?? 0;
                decimal wd = dto.WorkingDays;
                //report.KpiAllowance = ((kr) * wd) / 30;
                report.KpiAllowance = Math.Round((kr * wd) / 30m, 2);

                report.GrossSalary = report.Wages + report.KpiAllowance + dto.Incentives + dto.Bonus + dto.AttendanceAllowance + dto.TransportAllowance + dto.FoodAllowance + dto.MedicalAllowance + dto.InternetAllowance;
                report.TotalDeductions = dto.SalaryAdvances + dto.Loans + dto.OtherDeductions;
                report.NetSalary = report.GrossSalary - report.TotalDeductions;
            }
            else
            {
                var basic = (employee.BasicSalary ?? 0) + (employee.Bra1 ?? 0) + (employee.Bra2 ?? 0);
                decimal bas = basic;

                report.BasicStationarySal = employee.BasicSalary;
                report.basicSala = basic;
                report.Wages = basic;
                report.Bra1 = employee.Bra1;
                report.Bra2 = employee.Bra2;
                report.KpiAllowance = employee.KpiAmount ?? 0;
                report.Ot1Payment = bas / 240 * ot1rate.Rate * dto.Ot1Hours;
                report.Ot2Payment = bas / 240 * ot2rate.Rate * dto.Ot2Hours;
                report.TotalOtPayment = report.Ot1Payment + report.Ot2Payment;
                report.NoPayDays = dto.NoPayDays;

                //report.NoPay = dto.NoPayDays * basic / 30;
                report.NoPay = Math.Round((decimal)(dto.NoPayDays * bas / 30), 2);

                report.EpfLiableSalary = basic - report.NoPay;
                report.GrossSalary = report.EpfLiableSalary + report.KpiAllowance + dto.Bonus + dto.Incentives + report.TotalOtPayment + dto.AttendanceAllowance + dto.TransportAllowance + dto.FoodAllowance + dto.MedicalAllowance + dto.InternetAllowance;
                report.Epf1 = report.EpfLiableSalary * 0.08m;
                report.Epf2 = report.EpfLiableSalary * 0.12m;
                report.Etf = report.EpfLiableSalary * 0.03m;
                report.EmployeeContribution = report.Epf2 + report.Etf;
                report.TotalDeductions = report.Epf1 + dto.SalaryAdvances + dto.Loans + dto.OtherDeductions;
                report.NetSalary = report.GrossSalary - report.TotalDeductions;
            }

            _dbContext.SalaryReports.Add(report);
            await _dbContext.SaveChangesAsync();
            return report;
        }

        public async Task<List<SalaryReport>> GetAllSalaryReportsAsync()
        {
            return await _dbContext.SalaryReports
                .Include(r => r.Employee)
                .ToListAsync();
        }
        public async Task<List<SalaryReport>> GetAllSalaryReportsByEmployeeIdAsync(int employeeId)
        {
            return await _dbContext.SalaryReports
                .Include(r => r.Employee)
                .Where(r => r.EmployeeId == employeeId)
                .ToListAsync();
        }
        public async Task<SalaryReport?> UpdateSalaryReportAsync(int id, SalaryReportDto dto)
        {
            var report = await _dbContext.SalaryReports
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null) return null;

            // Update input fields
            report.EmployeeId = dto.EmployeeId;
            report.Year = dto.Year;
            report.Month = dto.Month;
            report.WorkingDays = dto.WorkingDays;
            report.Incentives = dto.Incentives;
            report.Bonus = dto.Bonus;
            report.SalaryAdvances = dto.SalaryAdvances;
            report.Loans = dto.Loans;
            report.OtherDeductions = dto.OtherDeductions;
            report.LeaveDays = dto.LeaveDays;
            report.HalfDays = dto.HalfDays;
            report.NoPayDays = dto.NoPayDays;
            report.Ot1Hours = dto.Ot1Hours;
            report.Ot2Hours = dto.Ot2Hours;
            report.AttendanceAllowance = dto.AttendanceAllowance;
            report.TransportAllowance = dto.TransportAllowance;
            report.FoodAllowance = dto.FoodAllowance;
            report.MedicalAllowance = dto.MedicalAllowance;
            report.InternetAllowance = dto.InternetAllowance;

            // Recalculate salary fields as needed (reuse your calculation logic here)
            // Example:
            // var employee = await _dbContext.Employe.Include(e => e.EmployeeCategories).FirstOrDefaultAsync(e => e.Id == dto.EmployeeId);
            // ... (salary calculation logic)

            await _dbContext.SaveChangesAsync();
            return report;
        }
        public async Task<bool> DeleteSalaryReportAsync(int id)
        {
            var report = await _dbContext.SalaryReports
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
                return false;

            _dbContext.SalaryReports.Remove(report);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
