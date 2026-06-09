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
        //[StringLength(20, MinimumLength = 3,
        //ErrorMessage = "project title must be between 3 and 20 characters.")]
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
