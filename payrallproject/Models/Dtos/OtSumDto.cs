namespace payrallproject.Models.Dtos
{
    public class OtSumDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TotalOt1Hours { get; set; }
        public int TotalOt2Hours { get; set;}
    }
}
