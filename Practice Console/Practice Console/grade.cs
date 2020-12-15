//// C# Program to find LCM of n elements 
//using System;

//public class GFG
//{

//    public static long lcm_of_array_elements(int[] element_array)
//    {
//        long lcm_of_array_elements = 1;
//        int divisor = 2;

//        while (true)
//        {

//            int counter = 0;
//            bool divisible = false;
//            for (int i = 0; i < element_array.Length; i++)
//            {

//                // lcm_of_array_elements (n1, n2, ... 0) = 0. 
//                // For negative number we convert into 
//                // positive and calculate lcm_of_array_elements. 
//                if (element_array[i] == 0)
//                {
//                    return 0;
//                }
//                else if (element_array[i] < 0)
//                {
//                    element_array[i] = element_array[i] * (-1);
//                }
//                if (element_array[i] == 1)
//                {
//                    counter++;
//                }

//                // Divide element_array by devisor if complete 
//                // division i.e. without remainder then replace 
//                // number with quotient; used for find next factor 
//                if (element_array[i] % divisor == 0)
//                {
//                    divisible = true;
//                    element_array[i] = element_array[i] / divisor;
//                }
//            }

//            // If divisor able to completely divide any number 
//            // from array multiply with lcm_of_array_elements 
//            // and store into lcm_of_array_elements and continue 
//            // to same divisor for next factor finding. 
//            // else increment divisor 
//            if (divisible)
//            {
//                lcm_of_array_elements = lcm_of_array_elements * divisor;
//            }
//            else
//            {
//                divisor++;
//            }

//            // Check if all element_array is 1 indicate 
//            // we found all factors and terminate while loop. 
//            if (counter == element_array.Length)
//            {
//                return lcm_of_array_elements;
//            }
//        }
//    }

//    // Driver Code 
//    public static void Main()
//    {
//        int[] element_array = { 2, 7, 3, 9, 4 };
//        Console.Write(lcm_of_array_elements(element_array));
//    }
//}
//namespace Practice
//{
//    // This Code is contributed by nitin mittal 
//    using System.CodeDom.Compiler;
//    using System.Collections.Generic;
//    using System.Collections;
//    using System.ComponentModel;
//    using System.Diagnostics.CodeAnalysis;
//    using System.Globalization;
//    using System.IO;
//    using System.Linq;
//    using System.Reflection;
//    using System.Runtime.Serialization;
//    using System.Text.RegularExpressions;
//    using System.Text;
//    using System;

//    class Solution
//    {
//        public static long lcm_of_array_elements(int[] element_array)
//        {
//            long lcm_of_array_elements = 1;
//            int divisor = 2;

//            while (true)
//            {

//                int counter = 0;
//                bool divisible = false;
//                for (int i = 0; i < element_array.Length; i++)
//                {

//                    // lcm_of_array_elements (n1, n2, ... 0) = 0. 
//                    // For negative number we convert into 
//                    // positive and calculate lcm_of_array_elements. 
//                    if (element_array[i] == 0)
//                    {
//                        return 0;
//                    }
//                    else if (element_array[i] < 0)
//                    {
//                        element_array[i] = element_array[i] * (-1);
//                    }
//                    if (element_array[i] == 1)
//                    {
//                        counter++;
//                    }

//                    // Divide element_array by devisor if complete 
//                    // division i.e. without remainder then replace 
//                    // number with quotient; used for find next factor 
//                    if (element_array[i] % divisor == 0)
//                    {
//                        divisible = true;
//                        element_array[i] = element_array[i] / divisor;
//                    }
//                }

//                // If divisor able to completely divide any number 
//                // from array multiply with lcm_of_array_elements 
//                // and store into lcm_of_array_elements and continue 
//                // to same divisor for next factor finding. 
//                // else increment divisor 
//                if (divisible)
//                {
//                    lcm_of_array_elements = lcm_of_array_elements * divisor;
//                }
//                else
//                {
//                    divisor++;
//                }

//                // Check if all element_array is 1 indicate 
//                // we found all factors and terminate while loop. 
//                if (counter == element_array.Length)
//                {
//                    return lcm_of_array_elements;
//                }
//            }
//        }

//        // Complete the kangaroo function below.
//        static string kangaroo(int x1, int v1, int x2, int v2)
//        {
//            int[] elements = { x1, v1, x2, v2 };
//            string output = "NO";
//            if (v1 > v2)
//            {
//                int iter = (int)lcm_of_array_elements(elements);
//                for (int i = 1; i < iter + 1; i++)
//                {
//                    if (i * (x1 + v1 * i) == i * (x2 + v2 * i))
//                    {
//                        output = "YES";
//                        break;
//                    }
//                }
//            }
//            return output;
//        }





//        static void Main(string[] args)
//        {
//            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

//            string[] x1V1X2V2 = Console.ReadLine().Split(' ');

//            int x1 = Convert.ToInt32(x1V1X2V2[0]);

//            int v1 = Convert.ToInt32(x1V1X2V2[1]);

//            int x2 = Convert.ToInt32(x1V1X2V2[2]);

//            int v2 = Convert.ToInt32(x1V1X2V2[3]);

//            string result = kangaroo(x1, v1, x2, v2);

//            textWriter.WriteLine(result);

//            textWriter.Flush();
//            textWriter.Close();
//        }
//    }
//}

