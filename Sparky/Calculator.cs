using System;
namespace Sparky
{
    public class Calculator
    {
        public IList<int> NumberRange { get; set; }
        public Calculator()
        {
            NumberRange= new List<int>();
        }

        public int AddNumbers(int number1, int number2)
        {
            return number1 + number2;
        }

        public double AddDoubleNumbers(double number1, double number2)
        {
            return number1 + number2;
        }

        public bool IsOdd(int number)
        {
            return (number % 2 != 0);
        }

        public IEnumerable<int> GetOddNumbers(int min, int max)
        {
            NumberRange.Clear();

            for (int i = min; i <= max; i++)
            {
                if (i % 2 == 1)
                {
                    NumberRange.Add(i);
                }
            }
            return NumberRange;
        }
    }
}