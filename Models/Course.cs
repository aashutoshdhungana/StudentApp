using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
