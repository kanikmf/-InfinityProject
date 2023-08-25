namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Models.Dtos.Project
{
    public class NewProjectDto
    {
     
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
