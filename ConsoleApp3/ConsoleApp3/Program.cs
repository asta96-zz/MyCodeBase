using System;
using System.Linq;
using System.Collections.Generic;
namespace BinaryTree
{
    class Program
    {
        private static int noIteration = 0;
        static void Main(string[] args)
        {
            PreOrderTraversal();
           // Rotate(new int[] {1, 2, 3, 4, 5, 6, 7}, 3);
           
            //Console.WriteLine("No of disks:");
            //int n=Convert.ToInt32(Console.ReadLine());
            //Honoi(n, 'A', 'B', 'C');
            //Console.WriteLine("No of iteration :"+noIteration);
            //Console.ReadKey();



            /* CRM_core obj = new CRM_core();
             obj.UseMultipleRequest();
             Reverse(-5210);
             try
             {
                 long x = long.Parse("20000000000000000000");


             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.ToString());
                 throw;
             }
                 System.Console.WriteLine("Binary Search tree\n");
                 BinaryTree binaryTree = new BinaryTree();
                 List<int> numbers = new List<int>() { 10, 3, 2, 5, 12, 11, 15 };

                 numbers.ForEach(x => binaryTree.Insert(x));
                 Console.WriteLine("Inorder traversal: left->root->right \n");
                 binaryTree.InorderTraversal();
                 binaryTree.Reverse();
                 Console.WriteLine("Mirrored tree: left->root->right \n");
                 binaryTree.InorderTraversal();
                 Console.WriteLine("PreOrderTraversal: root->left->right  \n");
                 binaryTree.PreOrderTraversal();
                 Console.WriteLine("PostOrderTraversal: root->right->left  \n");
                 binaryTree.PostOrderTraversal();*/
            }

        private static void PreOrderTraversal()
        {
            List<int> v1 = new List<int>();
            IList<int> v2 = new List<int>() { 1,2,3};
            IList<int> v3 = new List<int>() { 4,5,6};
            v1.AddRange(v2);
            v1.AddRange(v3);
            foreach(var i in v1)
            {
                Console.Write(i.ToString());
            }
        }

        public static void Rotate(int[] nums, int k)
        {
            //int prev;
            //int iterate = 0;
            ////1,2,3
            ////
            //while (iterate < k) 
            //{
            //     prev = nums[nums.Length-1];

            //    for (int i=0;i<nums.Length;i++)
            //    {
            //        int temp = nums[i];
            //        nums[i] = prev;
            //        prev = temp;

            //    }
            //    iterate++;
            //}
            int[] a = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                a[(i + k) % nums.Length] = nums[i];
            }
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = a[i];
            }

        }












        private static void Honoi(int n, char source, char temp, char dest)
        {
            if (n == 1)
            {
                noIteration++;
                Console.WriteLine("Move disk " + n+" from "+source+"-->"+dest);
                return;
            }
            Honoi(n - 1, source, dest, temp);
            noIteration++;
            Console.WriteLine("Move disk " + n + " from " + source + "--> " + dest);
            Honoi(n - 1, temp, source, dest);
        }

        public static int Reverse(int x)
        {
            int result = 0;
            bool negative = false;
            if (x < 0)
            {
                negative = true;
                x *= -1;
            }

            while (x != 0)
            {
                int mod = x % 10;
                x /= 10;
                if (result > (int.MaxValue - mod) / 10)
                {
                    return 0;
                }
                result = (result * 10) + mod;
            }

            return negative ? result * -1 : result;

        }

    }
}
