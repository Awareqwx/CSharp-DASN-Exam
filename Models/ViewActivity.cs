using System;
using System.ComponentModel.DataAnnotations;

namespace DojoActivities.Models
{
    public class ViewActivity
    {

        public enum DurType {Days, Hours, Minutes}

        [Required(ErrorMessage = "Field is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Time, ErrorMessage = "Please enter a time.")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a Date.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [Range(0, 1000, ErrorMessage = "Please enter a number between 1 and 1000.")]
        public int Duration { get; set; }
        public DurType DurationType { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}