using System;

namespace payrallproject.Models.Dtos
{
    public class HolidayDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; }
        public string HolidayType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateHolidayDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; }
        public string HolidayType { get; set; }
    }

    public class UpdateHolidayDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; }
        public string HolidayType { get; set; }
    }
}