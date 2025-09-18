namespace payrallproject.Models.Dtos
{
    public class SalaryReportDto
    {
        public int EmployeeId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int WorkingDays { get; set; }
        public decimal Incentives { get; set; }
        public decimal Bonus { get; set; }
        public decimal SalaryAdvances { get; set; }
        public decimal Loans { get; set; }
        public decimal OtherDeductions { get; set; }
        public int LeaveDays { get; set; }
        public int HalfDays { get; set; }
        public int NoPayDays { get; set; }
        public decimal Ot1Hours { get; set; }
        public decimal Ot2Hours { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
