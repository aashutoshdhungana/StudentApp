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
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
