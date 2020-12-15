class Solution
{
    static void Main()
    {
        string a = "London";
        string b = "london";
        bool y=string.Equals(a, b, System.StringComparison.OrdinalIgnoreCase);
        System.Console.WriteLine(y);
    }

    ///*static void Main()
    //{

    //    List<string> dict = new List<string>();
    //    dict.Add("love");
    //    dict.Add("stairs");
    //    dict.Add("sitars");
    //    dict.Add("cool");
    //    dict.Add("looc");
    //    List<string> query = new List<string>();
    //    query.Add("pick");
    //    query.Add("stair");
    //    query.Add("abcd");
    //    List<int> output = new List<int>();

    //    foreach (string s in query)
    //    {
    //        int count = 0;
    //        char[] q = s.ToCharArray().OrderBy(y => y).ToArray();
    //        List<string> temp = dict.Any(x => x.Length == s.Length) ? dict.Where(x => x.Length == s.Length).ToList() : new List<string>();
    //        foreach (string x in temp)
    //        {
    //            //s to char array, x to char array. 
    //            char[] c = x.ToCharArray().OrderBy(y => y).ToArray();
    //            if (c.SequenceEqual(q))
    //            {
    //                count++;
    //            }

    //        }
    //        output.Add(count);
    //    }

    //    Console.Write(output);

    //}*/
    //static void Main()
    //{
    //    string s = "51Pa*0Lp*0e";
    //     s = "51Pa*0Lp*ee0eE";
    //    char[] c = s.ToCharArray();
    //    char[] temp = c.Where(x => (Char.IsNumber(x) & !x.Equals('0'))).ToArray();
    //    char[] outp = new char[s.Length];
    //    List<char> op = new List<char>();
    //    int j = 0;
    //    for (int i = 0; i < c.Length; i++)
    //    {
    //        if (Char.IsUpper(c[i]) && Char.IsLower(c[i + 1]) && c[i + 2].Equals('*'))
    //        {
    //            //swap them, a* after them, move to i+2
    //            outp[i] = c[i + 1];
    //            outp[i + 1] = c[i];
    //            continue;
    //        }
    //        else if (c[i].Equals('*'))
    //        {
    //            outp[i] = '-';
    //            continue;
    //        }
    //        else if (Char.IsNumber(c[i]) && c[i] == '0')
    //        {
    //            outp[i] = temp[temp.Length - 1 - j];
    //            j++;
    //            //replace it with 0 and place the orignial numer at start, move to i+1
    //        }
    //        else if(Char.IsNumber(c[i]))
    //        {
    //            outp[i] = '-';
    //        }
    //        else if (i < c.Length - 1)
    //        {
    //             if ((Char.IsLower(c[i]) && !c[i + 1].Equals('*')))
    //            {
    //                outp[i] = c[i];
    //                //continue;
    //            }
    //        }
    //        else if (i < c.Length - 2)
    //        {
    //             if(Char.IsUpper(c[i]) && !c[i + 2].Equals('*'))
    //            {
    //                outp[i] = '-';
    //                //continue;
    //            }
    //        }
    //        else
    //        {
    //            outp[i] = '-';
    //            continue;
    //        }

    //    }

    //    //Console.Write(outp);
    //    string os = new string(outp.Where(x=> !x.Equals('-')).ToArray());

    //    string xOp = os;
    //    //xOp.TrimStart().Replace('-','');
    //    Console.WriteLine(xOp);
    //}
}
