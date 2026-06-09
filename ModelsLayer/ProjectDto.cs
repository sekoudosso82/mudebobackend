using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class ProjectDto
    {
        // [Required]
        [Key]
        public int? ProjectId { get; set; }
        [Required]
        [MinLength(3)]
        public string? ProjectTitle { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ProjectStatus { get; set; }
        public DateTime? ProjectDate { get; set; }
        public IFormFile? ProjectPhotoUrl { get; set; }
    }
}
