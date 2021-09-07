using System;
using System.Collections;
// input : two integer
// output : length of the longest sequence when applying 3n+1 principle to all integers between two input  numbers
namespace Coding_Algorithms
{
    class ThreeNPlusOne
    {
        static void Main(string[] args)
        {
            // Reading two integers from the command line
            Console.WriteLine("Enter first Integer:");
            int firstInteger = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter second Integer:");
            int secondInteger = Convert.ToInt32(Console.ReadLine());

            // a temporal value for storing 3n+1 operation value
            int temp = 0;

            // a counter for tracking sequence length
            int counter = 0;

            // hashtable for mapping each number to is sequence length
            Hashtable sequenceLength = new Hashtable();

            // iterating through all the numbers between and including firstInteger and secondInteger
            for (int i = firstInteger; i<=secondInteger; i++)
            {   
                // temp contains the number which represents n in (3n+1)
                temp = i;
                //the first value of counter is 1 implying that the number itself is counted in sequence length
                counter = 1;

                // calculating the length of 3n+1 sequence for each number individually
                while (temp != 1)
                {
                    // if number is even , half the number
                    if (temp % 2 == 0)
                        temp = temp / 2;
                    else
                        temp = 3 * temp + 1;

                    counter++;

                }

                // key is the number,value is sequence length
                sequenceLength.Add(i, counter);

            }

            // determining the longest sequence
            //temporal variable for storing max sequence length
            int max = 0;

            //algorithm for determining max
            for (int i = firstInteger; i <= secondInteger; i++)
            {
                if ((int)sequenceLength[i] > max)
                {
                    max = (int)sequenceLength[i];
                }

            }

            Console.WriteLine("first integer  lastinteger  sequenceLength :");
            Console.Write(firstInteger + "  " + secondInteger + "  " + max);





        }
    }
}
