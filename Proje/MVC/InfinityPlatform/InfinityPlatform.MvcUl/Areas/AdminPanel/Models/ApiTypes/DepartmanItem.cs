namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes
{
    public class DepartmanItem
    {

        public int DepartmanId { get; set; }
        public string? DepartmanName { get; set; }
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public int? EmployeeCount { get; set; }
        public string? ContactInformation { get; set; }
    }
}
