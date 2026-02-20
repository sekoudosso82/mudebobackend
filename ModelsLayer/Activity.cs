using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class Activity
    {
        public string? ActivityId { get; set; }
        public string? ActivityTitle { get; set; }
        public string? ActivityDescription { get; set; }
        public DateOnly? ActivityDate { get; set; }
        public string? ActivityStatus { get; set; }
        public string? ActivityPhoto { get; set; }

        public Activity(string activityId,string activityTitle, 
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
