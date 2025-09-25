using System;

namespace MathOperationExample
{
    // Create a class called MathClass
    public class MathClass
    {
        // Create a void method that takes two integers as parameters
        public void DoMath(int number1, int number2)
        {
            // Perform a math operation on the first integer (e.g., multiply it by 2)
            int result = number1 * 2;

            // Display the result of the operation
            Console.WriteLine("The result of the operation on the first number is: " + result);

            // Display the second integer to the screen
            Console.WriteLine("The second number is: " + number2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the class (create an object of MathClass)
            MathClass mathObj = new MathClass();

            // Call the method, passing in two numbers (positional arguments)
            mathObj.DoMath(5, 10);

            // Call the method again, this time specifying the parameters by name
            mathObj.DoMath(number1: 7, number2: 20);

            // Prevent console from closing immediately
            Console.ReadLine();
        }
    }
}
