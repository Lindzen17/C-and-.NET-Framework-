using System;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstStudentApp
{
    public class Student
    {
        public int StudentId { get; set; }   // PK by convention

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
