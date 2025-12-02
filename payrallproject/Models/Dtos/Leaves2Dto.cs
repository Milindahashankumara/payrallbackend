namespace payrallproject.Models.Dtos
{
    public class Leaves2Dto
    {
        public int? Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsHalfDay { get; set; }
        public bool? IsFirstHalfDay { get; set; }
        public decimal NumberOfDays { get; set; }
        public string LeaveType { get; set; } = "Casual";
        public string? Reason { get; set; }
        public string Status { get; set; } = "Pending";
        public int Year { get; set; }
    }

    public class CreateLeaveDto
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsHalfDay { get; set; }
        public bool? IsFirstHalfDay { get; set; }
        public string LeaveType { get; set; } = "Casual";
        public string? Reason { get; set; }
    }

    public class CreateNoPayDto
    {
        public int EmployeeId { get; set; }
        public DateTime NoPayDate { get; set; }
        public string? Reason { get; set; }
    }

    public class LeaveBalanceDto
    {
        public string LeaveType { get; set; } = string.Empty;
        public decimal EntitledDays { get; set; }
        public decimal UsedDays { get; set; }
        public decimal BalanceDays { get; set; }
    }

    public class EmployeeLeaveSummaryDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public int Year { get; set; }
        public List<LeaveBalanceDto> LeaveBalances { get; set; } = new();
        public decimal TotalHalfDays { get; set; }
        public List<Leaves2Dto> Leaves { get; set; } = new();
        public List<NoPayEntryDto> NoPayDays { get; set; } = new();
    }

    public class NoPayEntryDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime NoPayDate { get; set; }
        public string? Reason { get; set; }
    }

    public class DateRangeLeaveSummaryDto
    {
        public int EmployeeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal LeaveDays { get; set; }
        public decimal HalfDays { get; set; }
        public int NoPayDays { get; set; }
    }
}
