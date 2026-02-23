using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class Project
    {
        [Required]
        [Key]
        public int ProjectId{ get; set; }
        public string? ProjectTitle { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ProjectStatus { get; set; }
        public DateOnly ProjectDate { get; set; }
        public string? ProjectPhoto { get; set; }

        public Project(string projectTitle, string projectDescription, 
            string projectStatus, DateOnly projectDate, string projectPhoto) 
        {
            ProjectTitle = projectTitle;
            ProjectDescription = projectDescription;
            ProjectStatus = projectStatus;
            ProjectDate = projectDate;
            ProjectPhoto = projectPhoto;
        }

    }
}
