namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Models.ApiTypes
{
    public class ProjectItem
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Budget { get; set; }
        public string Status { get; set; }
    }
}
