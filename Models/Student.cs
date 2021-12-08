using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models
{
    public class Student
    {
        [Key]
        public Guid StudentID { get; set; }
        [Required]
        public string FullName { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
