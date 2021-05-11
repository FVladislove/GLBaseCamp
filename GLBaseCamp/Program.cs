using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace GLBaseCamp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int[]> classes = new Dictionary<string, int[]>
            {
                {"1-A", new[] {10, 14, 4}},
                {"1-B", new[] {8, 12, 15}},
                {"1-C", new[] {12, 7, 8}}
            };
            Console.WriteLine("Task 1 (a)");
            Console.WriteLine(string.Join(' ',
                CalculateStudentsByGrades(classes)));

            Console.WriteLine("Task 1 (b)");
            foreach (var _class in CalculatePercentageByGrades(classes))
            {
                Console.WriteLine(_class.Key + ":\t" + string.Join(' ', _class.Value));
            }

            Console.WriteLine("Task 1 (c)");
            Console.WriteLine(string.Join(' ', CalculatePercentageByGradesOnParallel(classes)));

            Console.WriteLine("Task 2");
            Console.WriteLine("Number 2 is even? -> " + IsEven(2) + "\n"
                              + "Number 15 is even? -> " + IsEven(15));

            Console.WriteLine("Task 3");
            Console.WriteLine("Middle number from 13, 14, 12 is -> " + GetMiddleNum(13, 14, 12));

            Console.WriteLine("Task 4");
            int[] numbers = {1, 1, 14, 54, 43, 22, 23, 22, 666, 777, 829, 3, 2, 1, 16, 16, 16};
            Console.WriteLine("Numbers ->\t[" + string.Join(", ", numbers) + "]\n"
                              + "UniqNumbers ->\t[" + string.Join(", ", GetDistinctArray(numbers)) + "]");

            Console.WriteLine("Task 5");
            int[,] matrix =
            {
                {1, 2, 3},
                {6, 5, 7},
                {5, 0, 8},
                {9, 4, 3}
            };
            PrintMatrix(matrix);
            Console.WriteLine("Transponated");
            PrintMatrix(TransposeMatrix(matrix));

            Console.WriteLine("Task 8");
            string str = "я-не-хочу-робити-дз";
            Console.WriteLine(str);
            Console.WriteLine("first -> " + FindFirstBetween(str, '-'));

            Console.WriteLine("Task 9");
            str = "Lorem ipsum dolor sit amet";
            FindWord(str, "ipsum");

            Console.WriteLine("Additional");
            Console.WriteLine(string.Join(' ', FindDuplicates(numbers)));
        }

        // Task 1
        static int[] CalculateStudentsByGrades(Dictionary<string, int[]> classes)
        {
            // example of class => [10, 14, 4], where
            // 10 - number of excellent students
            // 14 - number of good students
            // 4 - number of C grade students
            int[] totalCount = {0, 0, 0};
            foreach (string key in classes.Keys)
            {
                for (int i = 0; i < classes[key].Length; i++)
                {
                    totalCount[i] += classes[key][i];
                }
            }

            return totalCount;
        }

        static float[] CalculatePercentageOfRange(int[] arr, float denominator)
        {
            return arr.Select(num => (float) Math.Round(num / denominator * 100, 1)).ToArray();
        }

        static Dictionary<string, float[]> CalculatePercentageByGrades(Dictionary<string, int[]> classes)
        {
            var classesPercentage = new Dictionary<string, float[]>();
            foreach (string key in classes.Keys)
            {
                float studentsCount = classes[key].Sum();
                classesPercentage.Add(key, CalculatePercentageOfRange(classes[key], studentsCount));
            }

            return classesPercentage;
        }

        static float[] CalculatePercentageByGradesOnParallel(Dictionary<string, int[]> parallel)
        {
            int[] studentsCalculated = CalculateStudentsByGrades(parallel);
            float totalCount = studentsCalculated.Sum();
            float[] parallelPercentage = {0, 0, 0};
            for (int i = 0; i < studentsCalculated.Length; i++)
            {
                parallelPercentage[i] = (float) Math.Round(studentsCalculated[i] / totalCount * 100, 1);
            }

            return parallelPercentage;
        }

        // Task 2
        static bool IsEven(int num)
        {
            return num % 2 == 0;
        }

        // Task 3
        static float GetMiddleNum(float a, float b, float c)
        {
            if (a > b && a < c || a > c && a < b)
                return a;
            if (b > a && b < c || b > c && b < a)
            {
                return b;
            }

            return c;
        }

        // Task 4
        static int[] GetDistinctArray(int[] arr)
        {
            var uniqNumbers = new List<int>();
            bool isInUniq; // ???
            foreach (int num in arr)
            {
                isInUniq = false;
                foreach (var uniqNumber in uniqNumbers)
                {
                    if (num == uniqNumber)
                    {
                        isInUniq = true;
                        break;
                    }
                }

                if (!isInUniq)
                {
                    uniqNumbers.Add(num);
                }
            }

            return uniqNumbers.ToArray();
        }

        // Task 5
        static int[,] TransposeMatrix(int[,] matrix)
        {
            int[,] transposed = new int[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    transposed[j, i] = matrix[i, j];
                }
            }

            return transposed;
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        // Task 6
        static double RoundNum(double num, int digits)
        {
            // kekw
            return Math.Round(num, digits);
        }

        // Task 7
        static double FindY1(double x)
        {
            return 100 * Math.Tan(x) * Math.Sqrt(x) / x;
        }

        static double FindY2(double x)
        {
            return 2 * Math.Sin(x) - 4;
        }

        // Task 8
        static string FindFirstBetween(string str, char delimiter)
        {
            string[] splitted = str.Split(delimiter);
            if (splitted.Length >= 2)
            {
                return splitted[1];
            }

            return null;
        }

        // Task 9
        static void FindWord(string str, string word)
        {
            int startIdx = str.IndexOf(word, StringComparison.Ordinal);
            Console.WriteLine(startIdx + "-" + (startIdx + word.Length - 1));
        }

        // additional
        static List<int> FindDuplicates(int[] numbers)
        {
            var numbersCounters = new Dictionary<int, int>();
            foreach (int number in numbers)
            {
                if (numbersCounters.ContainsKey(number))
                {
                    numbersCounters[number] += 1;
                }
                else
                {
                    numbersCounters.Add(number, 1);
                }
            }
            var duplicates = numbersCounters.Where(it => it.Value > 2);
            return duplicates.Select(duplicate => duplicate.Key).ToList();
        }
    }
}