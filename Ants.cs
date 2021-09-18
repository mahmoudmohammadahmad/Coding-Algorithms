using System;
using System.Collections;
using System.Globalization;

namespace Coding_Algorithms
{
    class Ants
    {
        public static void Main(string[] args)
        {
            string s = "fb1da30d1b7230";
            BitArray bitArray = ConvertHexToBitArray(s);
            int leftAntPos = 0;
            int rightAntPos = bitArray.Length - 1;
            int leftAntUpSpeed = 2;
            int leftAntDownSpeed = 1;
            int rightAntUpSpeed = 1;
            double rightAntDownSpeed = 0.5;
            double rightAntTimer = 0;
            double leftAntTimer = 0;
            int encounterTimes = 0;
            double diff = 0;
            double SecondTime = 0;
            bool case1 = false;
            bool case2 = false;

            while (encounterTimes < 2)
            {
                if (bitArray[leftAntPos] == false && encounterTimes  == 0)
                {
                    leftAntTimer += leftAntDownSpeed;//left ant makes  step Down

                    while ((rightAntTimer < leftAntTimer) && (rightAntPos > leftAntPos))
                    {
                        if (bitArray[rightAntPos] == false)
                        {
                            rightAntTimer += rightAntUpSpeed;// right ant makes step Up
                            diff = rightAntTimer - leftAntTimer;
                        }

                        else if (bitArray[rightAntPos] == true)
                        {
                            rightAntTimer += rightAntDownSpeed; // right ant makes step Down
                            diff = rightAntTimer - leftAntTimer;
                        }




                        rightAntPos--;
                       

                    }
                

                    if (rightAntPos <= leftAntPos)
                    {
                        encounterTimes++;
                        SecondTime += rightAntTimer;
                        Console.WriteLine("first encounter time = {0}", SecondTime);
                        rightAntTimer = leftAntTimer = 0;
                        rightAntPos += 1;


                    }



                }
                else if (bitArray[leftAntPos] == false && encounterTimes  == 1)
                {
                    leftAntTimer += leftAntUpSpeed;//left ant makes  step Down

                    while ((rightAntPos < bitArray.Length) && (rightAntTimer < leftAntTimer))
                    {
                        if (bitArray[rightAntPos] == false)
                        {
                            rightAntTimer += rightAntDownSpeed;// right ant makes step Up
                            diff = rightAntTimer - leftAntTimer;
                        }

                        else if (bitArray[rightAntPos] == true)
                        {

                            rightAntTimer += rightAntUpSpeed; // right ant makes step Down
                            diff = rightAntTimer - leftAntTimer;
                        }

                        rightAntPos++;
                       

                    }
                  


                    if (rightAntPos == bitArray.Length  && leftAntPos == 0)
                    {
                        encounterTimes += 1;
                        SecondTime += rightAntTimer;
                        break;
                    }

                    else if (leftAntPos > 0 && rightAntPos == bitArray.Length )

                    {
                        rightAntPos = 0;
                        case1 = true;
                    }

                    else if (leftAntPos == 0 && rightAntPos < bitArray.Length)

                    {
                        leftAntPos = bitArray.Length - 1;
                        case2 = true;
                    }
                    else if (leftAntPos == rightAntTimer)
                    {
                        SecondTime += rightAntTimer;
                        break;
                    }
                    else if ( rightAntPos >= leftAntPos &&(case1==true || case2==true))
                    {
                        SecondTime += rightAntTimer;
                        break;
                    }
                   



                }
                else if (bitArray[leftAntPos] == true && encounterTimes  == 0)
                {
                    leftAntTimer += leftAntUpSpeed;//left ant makes  step Down

                    while ((rightAntTimer < leftAntTimer) && (rightAntPos > leftAntPos))
                    {
                        if (bitArray[rightAntPos] == false)
                        {
                            rightAntTimer += rightAntUpSpeed;// right ant makes step Up
                            diff = rightAntTimer - leftAntTimer;
                        }
                        else if (bitArray[rightAntPos] == true)
                        {
                            rightAntTimer += rightAntDownSpeed; // right ant makes step Down
                            diff = rightAntTimer - leftAntTimer;
                        }



                        rightAntPos--;
                      

                    }
                

                    if (rightAntPos <= leftAntPos)
                    {
                        encounterTimes++;
                        SecondTime += rightAntTimer;
                        Console.WriteLine("first encounter time = {0}", SecondTime);
                        rightAntTimer = leftAntTimer = 0;
                        rightAntPos += 1;


                    }



                }
                else if (bitArray[leftAntPos] == true && encounterTimes == 1)
                {
                    leftAntTimer += leftAntDownSpeed;//left ant makes  step Down

                    while ((rightAntPos < bitArray.Length) && (rightAntTimer < leftAntTimer))
                    {
                        if (bitArray[rightAntPos] == false)
                        {
                            rightAntTimer += rightAntUpSpeed;// right ant makes step Up
                            diff = rightAntTimer - leftAntTimer;
                        }
                        else if (bitArray[rightAntPos] == true)
                        {
                            rightAntTimer += rightAntDownSpeed; // right ant makes step Down
                            diff = rightAntTimer - leftAntTimer;
                        }



                        rightAntPos++;

                       
                    }

                  

                    if ((rightAntPos == bitArray.Length) && (leftAntPos == 0))
                    {
                        encounterTimes++;
                        SecondTime += rightAntTimer;
                        break;

                    }
                    else if (leftAntPos > 0 && rightAntPos == bitArray.Length )

                    {
                        rightAntPos = 0;
                        case1 = true;
                    }
                    else if (leftAntPos == 0 && rightAntPos < bitArray.Length)

                    {
                        leftAntPos = bitArray.Length - 1;
                        case2 = true;
                    }
                    else if (rightAntPos >= leftAntPos && (case1 == true || case2 == true))
                    {
                        SecondTime += rightAntTimer;
                        break;
                    }


                }

                if (encounterTimes == 0)
                    leftAntPos += 1;
                else if (encounterTimes  == 1)
                    leftAntPos -= 1;
            }

            Console.WriteLine("Second encountertime = {0}", SecondTime);

        }

        public static BitArray ConvertHexToBitArray(string hexData)
        {
            if (hexData == null)
            {
                return null;
            }
            BitArray ba = new BitArray(4 * hexData.Length);
            for (int i = 0; i < hexData.Length; i++)
            {
                byte b = byte.Parse(hexData[i].ToString(), NumberStyles.HexNumber);

                for (int j = 0; j < 4; j++)
                {
                    ba.Set(i * 4 + j, (b & (1 << (3 - j))) != 0);
                }

            }
            return ba;
        }

    }
}
