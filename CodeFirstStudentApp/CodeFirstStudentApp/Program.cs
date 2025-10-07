using System;
using System.Linq;

namespace CodeFirstStudentApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting — creating DB and adding one student if not present.");

            using (var db = new SchoolContext())
            {
                // Trigger DB creation based on model
                db.Database.Initialize(force: false);

                // Create and add one student
                var student = new Student
                {
                    Name = "Alex Martinez",
                    EnrollmentDate = DateTime.UtcNow.Date
                };

                db.Students.Add(student);
                db.SaveChanges();

                Console.WriteLine("Saved student with ID: " + student.StudentId);

                // Verify by listing students
                var students = db.Students.ToList();
                Console.WriteLine($"Total students in DB: {students.Count}");
                foreach (var s in students)
                {
                    Console.WriteLine($"ID={s.StudentId}  Name={s.Name}  Enrolled={s.EnrollmentDate:d}");
                }
            }

            Console.WriteLine("Done. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
