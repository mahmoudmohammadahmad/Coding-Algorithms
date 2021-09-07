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

            //we want to know how much time our algorithm needs
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            //starting the timer
            stopwatch.Start();

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
                    if (sequenceLength.Contains(temp))
                    {
                        // we dont want to take into account number (1) two times , therefore we sbtract one
                        counter += (int)sequenceLength[temp] - 1;
                        temp = 1;

                    }
                    // if number is even , half the number
                     else if (temp % 2 == 0 )
                    {
                        temp = temp / 2;

                        // (++) takes into account the last number in the sequence, which is always (1).
                        counter++;
                    }

                     // if number is odd, apply 3n+1
                    else if (temp % 2 != 0  )
                    {
                        temp = 3 * temp + 1;
                        counter++;
                    }

                   



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
                // in case we want to see the mapping between keys and values.
               //  Console.WriteLine(i + ":" + sequenceLength[i]);

                if ((int)sequenceLength[i] > max)
                {
                    max = (int)sequenceLength[i];
                    
                }

            }

            Console.WriteLine("first integer  lastinteger  sequenceLength :");
            Console.Write(firstInteger + "  " + secondInteger + "  " + max);

            //record finish time
            stopwatch.Stop();

            Console.WriteLine(" Time taken : {0}", stopwatch.Elapsed);





        }
    }
}
