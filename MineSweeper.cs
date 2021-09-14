using System;

class MineSweeper
{
     static void Main2 (String[] args)
    {
        string temp = Console.ReadLine();
        string[] tokens = temp.Split(" ");
        int m, n;
        while (temp != "0 0")
        {
            m = Int32.Parse(tokens[0]);
            n = Int32.Parse(tokens[1]);

            Console.WriteLine("input numbers are: {0} and {1}", m, n);

            temp = Console.ReadLine();
            tokens = temp.Split(" ");

        }

    }
}