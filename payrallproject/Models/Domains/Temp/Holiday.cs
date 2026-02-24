using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class Holiday
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;

    public bool IsRecurring { get; set; }

    public string HolidayType { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
