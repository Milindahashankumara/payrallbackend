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
            var employee = await _dbContext.Employe
                .Include(e => e.EmployeeCategories)
                .FirstOrDefaultAsync(e => e.Id == dto.EmployeeId);

            if (employee == null || employee.EmployeeCategories == null)
                throw new Exception("Employee or category not found.");

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
                OT1Hours = dto.OT1Hours,
                OT2Hours = dto.OT2Hours,
                IsDaySalaryBased = isDaySalaryBased
            };

            if (isDaySalaryBased)
            {
                report.Wages = (employee.DaySalary ?? 0) * dto.WorkingDays;
                report.KPIAllowance = ((employee.KPIrate ?? 0) * dto.WorkingDays) / 30;
                report.GrossSalary = report.Wages + report.KPIAllowance + dto.Incentives;
                report.TotalDeductions = dto.SalaryAdvances + dto.Loans + dto.OtherDeductions;
                report.NetSalary = report.GrossSalary + dto.Bonus - report.TotalDeductions;
            }
            else
            {
                var basic = (employee.BasicSalary ?? 0) + (employee.BRA1 ?? 0) + (employee.BRA2 ?? 0);
                report.Wages = basic;
                report.KPIAllowance = employee.KPIamount ?? 0;
                report.OT1Payment = basic / 240 * GetOTRate(1) * dto.OT1Hours;
                report.OT2Payment = basic / 240 * GetOTRate(2) * dto.OT2Hours;
                report.TotalOTPayment = report.OT1Payment + report.OT2Payment;
                report.NoPayDays = dto.NoPayDays;
                report.EPFLiableSalary = basic - (dto.NoPayDays * basic / 30);
                report.GrossSalary = report.EPFLiableSalary + report.KPIAllowance + dto.Incentives + report.TotalOTPayment;
                report.EPF1 = report.EPFLiableSalary * 0.08m;
                report.EPF2 = report.EPFLiableSalary * 0.12m;
                report.ETF = report.EPFLiableSalary * 0.03m;
                report.EmployeeContribution = report.EPF2 + report.ETF;
                report.TotalDeductions = report.EPF1 + dto.SalaryAdvances + dto.Loans + dto.OtherDeductions;
                report.NetSalary = report.GrossSalary + dto.Bonus - report.TotalDeductions;
            }

            _dbContext.SalaryReports.Add(report);
            await _dbContext.SaveChangesAsync();
            return report;
        }

        private int GetOTRate(int otType)
        {
            // You can fetch OT rate from DB if needed, here is a stub:
            return otType == 1 ? 1 : 2;
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
            report.OT1Hours = dto.OT1Hours;
            report.OT2Hours = dto.OT2Hours;

            // Recalculate salary fields as needed (reuse your calculation logic here)
            // Example:
            // var employee = await _dbContext.Employe.Include(e => e.EmployeeCategories).FirstOrDefaultAsync(e => e.Id == dto.EmployeeId);
            // ... (salary calculation logic)

            await _dbContext.SaveChangesAsync();
            return report;
        }
    }
}
