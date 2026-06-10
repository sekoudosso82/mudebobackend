using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class ActivitiesDto
    {
        // [Required]
        [Key]
        public int? ActivityId { get; set; }
        [Required]
        [MinLength(3)]
        public string? ActivityTitle { get; set; }
        public string? ActivityDescription { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string? ActivityStatus { get; set; }
        public IFormFile? ActivityPhotoUrl { get; set; }
    }
}
