using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class Activities
    {
        [Required]
        [Key]
        public string? ActivityId { get; set; }
        public string? ActivityTitle { get; set; }
        public string? ActivityDescription { get; set; }
        public DateOnly? ActivityDate { get; set; }
        public string? ActivityStatus { get; set; }
        public string? ActivityPhoto { get; set; }
        public Activities() { }

        public Activities(string activityId,string activityTitle, 
            string activityDescription, DateOnly activityDate, 
            string activityStatus, string activityPhoto)
        {
            ActivityId = activityId;
            ActivityTitle = activityTitle;
            ActivityDescription = activityDescription;
            ActivityDate = activityDate;
            ActivityStatus = activityStatus;
            ActivityPhoto = activityPhoto;

        }


    }
}
