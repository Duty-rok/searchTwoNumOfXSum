//  Copiryght 2021, Aleksander Shashkin
//  This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <https://www.gnu.org/licenses/>.
using System;
using static System.Console;

namespace searchTwoNumOfXSum
{
    enum RecordIn//enumeration which help in mergeSort method
    {
        none,
        recordFromLeft,
        recordFromRight
    }
    class Program
    {
        static Random rand = new Random();
        static int Main(string[] args)
        {
            int size,
                compareNum;
            InputData(out size, out compareNum);
            int[] array = CreateArray(size);
            if (!search2Numbers(array, compareNum))
                WriteLine($"I don't find those two numbers, amount of which is equal {compareNum}, sorry");
            Write("Print Enter for Exit...");
            ReadKey(true);
            return 0;
        }
        static bool search2Numbers(int[] array, int compareNum)
        {
            array = mergeSort(array);//sort array
            int leftPoint = 0,
                rightPoint = array.Length - 1;
            while (leftPoint != rightPoint)//go on array from begin and end, checking a pare of numbers 
            {
                int sumOf2Num = array[leftPoint] + array[rightPoint]; //amount of two numbers from array
                if (sumOf2Num > compareNum)
                    rightPoint--;
                else if (sumOf2Num < compareNum)
                    leftPoint++;
                else
                {
                    WriteLine($"{array[rightPoint]} + {array[leftPoint]} = {compareNum}");
                    return true;
                }
            }
            return false;
        }
        static int[] CreateArray(int size)//created of random array
        {
            int[] result = new int[size];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = rand.Next(-25, 25);
                Write($"{result[i]}, ");
            }
            WriteLine();
            return result;
        }
        static void InputData(out int size, out int summOfNums)//input of size of array and number of comparing
        {
            size = 0;
            summOfNums = 0;
            bool correct0fdata = false;
            WriteLine("Hello, my name is tsar, i want to play with you.");
            while (!correct0fdata)
            {
                Write("Say me a size of massive (integer > 1) = ");
                if (!(int.TryParse(ReadLine(), out size)) | !(size > 1))
                {
                    WriteLine("You number doesn't correspond my parametrs, retry");
                    correct0fdata = false;
                }
                else
                    correct0fdata = true;
            }
            correct0fdata = false;
            while (!correct0fdata)
            {
                Write("Say me a number of compare whith summ of two numbers from massive (anything) = ");
                if (!(int.TryParse(ReadLine(), out summOfNums)) | !(summOfNums > 1))
                {
                    WriteLine("You number is very big, retry");
                    correct0fdata = false;
                }
                else
                    correct0fdata = true;
            }
        }
        static int[] mergeSort(int[] mass)
        {
            int[] result = new int[mass.Length]; // array for return as result of method
            int length = mass.Length;
            if (length <= 1)
                return mass; //conditional of recursive end, one elemet array has already sort
            int[] left = new int[length / 2], //left subarray with length of alf of array
                right = new int[length - left.Length];//the residue of array put in right
            Array.Copy(mass, 0, left, 0, left.Length);
            Array.Copy(mass, left.Length, right, 0, right.Length); //copy of massive in c#
            left = mergeSort(left); //merge sort of left (recursive)
            right = mergeSort(right);//merge dort of right (recursive)
            (int, int) buffer = (0, 0); //item1 - number from left array, item2 from right
            int leftIn = 0,
                rightIn = 0,
                resIn = 0; //indexses of arrays for merging
            RecordIn rec = RecordIn.none;//merging from two sorted subarrays to one array
            while (!(leftIn == left.Length | rightIn == right.Length))
            {
                buffer.Item1 = left[leftIn];
                buffer.Item2 = right[rightIn];
                if (buffer.Item1 > buffer.Item2)
                {
                    rec = RecordIn.recordFromRight;
                    result[resIn++] = buffer.Item2;
                    rightIn++;
                }
                else
                {
                    rec = RecordIn.recordFromLeft;
                    result[resIn++] = buffer.Item1;
                    leftIn++;
                }
            }
            if (rec == RecordIn.recordFromLeft)
                for (; rightIn < right.Length; rightIn++)
                    result[resIn++] = right[rightIn];
            else
                for (; leftIn < left.Length; leftIn++)
                    result[resIn++] = left[leftIn];
            return result;
        }
    }
}
