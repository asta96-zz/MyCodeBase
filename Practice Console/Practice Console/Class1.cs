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

    // Complete the birthday function below.
    static int birthday(List<int> squares, int d, int m)
    {
        int ways = 0;
        for (int i = 0; i < squares.Count - (m - 1); i++)
            if (squares.Skip(i).Take(m).Sum() == d)

            {
                Console.WriteLine();
                ways++;

            }
        return ways;
    }

    /*
    static int migratoryBirds(List<int> arr)
    {


         var x= arr.GroupBy(a=>a).Select(b=> new { Bird=b.Key,Count=b.Count()}).OrderByDescending(c => c.Count).ThenBy(d => d.Bird).FirstOrDefault().Bird;
        int counter = 0;
        IDictionary<int, int> dic = new Dictionary<int, int>();
        for (int i = 1; i < 6; i++)
        {
            counter = arr.Count(x => x == i);
            dic.Add(i, counter);
        } 
        foreach (KeyValuePair<int, int> a in dic)
        {
            Console.WriteLine("a.key:" + a.Key + "a.Value:" + a.Value);
        }
        return dic.OrderBy(x => x.Key).Max().Value;
        var x = dic.OrderBy(x => x.Key).Aggregate((l, m) => l.Value > m.Value ? l : m).Key;
        var y = dic.OrderByDescending(x => x.Key).Aggregate((l, m) => l.Value > m.Value ? l : m).Key;
    }
    */
    static string dayOfProgrammer(int year)
    {
        string formatdate = "";
        DateTime date = DateTime.UtcNow;
        if (DateTime.TryParseExact(year.ToString(), "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            formatdate = date.AddDays(256).ToString();
        return formatdate;
    }

    static void Main(string[] args)
    {
       // TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int year = Convert.ToInt32(Console.ReadLine().Trim());

        string result = dayOfProgrammer(year);

        //textWriter.WriteLine(result);

        //textWriter.Flush();
        //textWriter.Close();
    }
}

