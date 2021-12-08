using System;

namespace StudentApp.Models
{
    public class AssignedCourse
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}
