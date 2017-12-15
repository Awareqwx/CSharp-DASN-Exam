using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DojoActivities.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        [Key]
        public int UserId { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        [InverseProperty("Creator")]
        public List<Activity> CreatedActivities { get; set; }
        public List<Signup> SignedupFor { get; set; }

        public User()
        {
            CreatedActivities = new List<Activity>();
            SignedupFor = new List<Signup>();
        }
    }
}