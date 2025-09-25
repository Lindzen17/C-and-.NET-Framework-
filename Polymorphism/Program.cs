

using System;

namespace InterfaceExample
{
    // Define an interface called IQuittable
    // Interfaces specify methods that must be implemented by any class that inherits from them
    public interface IQuittable
    {
        void Quit(); // Any class that implements IQuittable must define this method
    }

    // Create an Employee class that implements the IQuittable interface
    public class Employee : IQuittable
    {
        // Properties of Employee class
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Constructor to initialize employee details
        public Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        // Implementation of the Quit method from the IQuittable interface
        public void Quit()
        {
            Console.WriteLine($"{FirstName} {LastName} has quit the job.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create an Employee object
            Employee emp = new Employee("Jesse", "Johnson");

            // Use polymorphism: assign the Employee object to a variable of type IQuittable
            IQuittable quittableEmp = emp;

            // Call the Quit method using the IQuittable interface reference
            quittableEmp.Quit();

            // Prevent console from closing immediately
            Console.ReadLine();
        }
    }
}

