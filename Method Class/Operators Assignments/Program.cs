using System;

namespace EmployeeComparisonApp
{
    // Create an Employee class
    public class Employee
    {
        // Employee properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Overload the "==" operator
        public static bool operator ==(Employee emp1, Employee emp2)
        {
            // If both are null, return true
            if (ReferenceEquals(emp1, emp2))
                return true;

            // If either is null, return false
            if (emp1 is null || emp2 is null)
                return false;

            // Compare by Id property
            return emp1.Id == emp2.Id;
        }

        // Overload the "!=" operator (must be done in pairs)
        public static bool operator !=(Employee emp1, Employee emp2)
        {
            return !(emp1 == emp2);
        }

        // Override Equals (recommended when overloading ==)
        public override bool Equals(object obj)
        {
            if (obj is Employee emp)
            {
                return this.Id == emp.Id;
            }
            return false;
        }

        // Override GetHashCode (must be consistent with Equals)
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the first Employee object
            Employee emp1 = new Employee { Id = 1, FirstName = "John", LastName = "Doe" };

            // Instantiate the second Employee object
            Employee emp2 = new Employee { Id = 1, FirstName = "Jane", LastName = "Smith" };

            // Compare the two Employee objects using the overloaded == operator
            if (emp1 == emp2)
            {
                Console.WriteLine("emp1 and emp2 are equal (they have the same Id).");
            }
            else
            {
                Console.WriteLine("emp1 and emp2 are NOT equal (their Ids are different).");
            }

            // Also show the result using the != operator
            Console.WriteLine("emp1 != emp2? " + (emp1 != emp2));

            // Prevent console window from closing immediately
            Console.ReadLine();
        }
    }
}

