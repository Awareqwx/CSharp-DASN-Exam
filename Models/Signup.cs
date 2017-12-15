using System;
using System.ComponentModel.DataAnnotations;

namespace DojoActivities.Models
{
    public class Signup
    {
        [Key]
        public int SignupId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}