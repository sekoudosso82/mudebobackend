using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class Projects
    {
        //[Required]
        [Key]
        public int? ProjectId{ get; set; }
        [Required]
        public string? ProjectTitle { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ProjectStatus { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string? ProjectPhotoUrl { get; set; }
        public Projects() { }

        public Projects(int projectId, string projectTitle, string projectDescription, 
            string projectStatus, DateTime projectDate, string projectPhotoUrl) 
        {
            ProjectId = projectId;
            ProjectTitle = projectTitle;
            ProjectDescription = projectDescription;
            ProjectStatus = projectStatus;
            ProjectDate = projectDate;
            ProjectPhotoUrl = projectPhotoUrl;
        }

    }
}
