using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DojoActivities.Models
{
    public class Activity
    {
        public enum DurType {Days, Hours, Minutes}
        [Key]
        public int ActivityId { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string Title { get; set; }
        public DateTime TimeAndDate { get; set; }
        public int Duration { get; set; }
        public DurType DurationType { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        [InverseProperty("CreatedActivities")]
        public User Creator { get; set; }
        public List<Signup> Signedup { get; set; }

        public Activity()
        {
            Signedup = new List<Signup>();
        }
    }
}