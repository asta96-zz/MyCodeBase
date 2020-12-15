using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution
{

    // Complete the sockMerchant function below.  10 20 20 10 10 30 50 10 20
    static int sockMerchant(int n, int[] ar)
    {
        int totalPaircounter=0, counter;
        var br = ar.GroupBy(v=>v).ToArray();
        for(int i= 0;i<br.Length;i++)
        {
            counter = 0;
            for (int j = 0; j < n; j++)
            {
                if (br[i].Key == ar[j] )
                {
                    counter++;
                }
                
            }
            if(counter >1)
            {
                totalPaircounter += (counter % 2==0) ?counter/2:(counter -1)/2;
            }
            
        }
        return totalPaircounter;
    }

    static void Main(string[] args)
    {
      //  TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine());

        int[] ar = Array.ConvertAll(Console.ReadLine().Split(' '), arTemp => Convert.ToInt32(arTemp))
        ;
        int result = sockMerchant(n, ar);
        Console.WriteLine(result);
        //textWriter.WriteLine(result);

        //textWriter.Flush();
        //textWriter.Close();
    }
}
