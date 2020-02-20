using System;
using System.Linq;

namespace string_checker
{
    class Program
    {
        public static double Percent(int diff, int len) //lev distance,len of string
        {
            double ans=1;
            ans = ( (double)len - (double)diff ) / (double)len;
            ans = Math.Round(ans, 5);
            return ans*100;
        }
        public static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
            {
                return m;
            }
            if (m == 0)
            {
                return n;
            }
            for (int i = 0; i <= n; d[i, 0] = i++){}
            for (int j = 0; j <= m; d[0, j] = j++){}
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }
        static void Main(string[] args)
        {
            //string[] arr = { "first" , "second" , "third" , "fourth" , "fifth" , "sixth" , "seventh" , "eighth" , "ninth" , "tenth"};
            string[] arr = {"Terms:", "Invoice Date", "Invoice Number", "Customer Number", "Ordered By", "Order Number", "Purchase Order Number",
                              "Ship To:", "Stock Number", "Description", "Product Category", "Ordered", "Shipped", "Unit Price", "Unit", "Extension",
                              "Ship Date", "Pkg Count", "Weight", "Sub Total", "Sales Tax", "Freight", "TOTAL", "Amount Due:", "Date Due:", "Mail To:",
                              "Sales Order Number", "American Hotel Register", "Invoice:", "Date:", "Total Amount:", "Catalog Number", "Payment Terms:",
                              "Due Date:", "Bill To", "Ship To", "Remit To", "Quantity", "Extended Price", "Subtotal", "Total Amount", "Tax"
                             };
            int[] result = new int[arr.Length];
            int min = 20; // minimum lev distance initialized
            int pos = 0;
            string input = "stok number";
            
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = Program.LevenshteinDistance(input, arr[i].ToLower());
                if (result[i] < min)
                {
                    min = result[i];
                    pos = i;
                }
            }
            Console.WriteLine("Input : " + input);
            Console.WriteLine("Closest Match : " + arr[pos]);
            //Console.WriteLine(Percent(min, arr[pos].Length) + "%");
            TopFive(arr,result);
        }
        public static void TopFive(string[] keyWords, int[] LevDistance)
        {
            int[] clone = new int[keyWords.Length];
            for (int i = 0; i < clone.Length; i++) //creating and initializing a clone array
            {
                clone[i] = i;
            }
            int temp,temp2;
            for (int j = 0; j <= LevDistance.Length - 2; j++) //sorting levdistance along with the clone to get indices of the least lev distances
            {
                for (int i = 0; i <= LevDistance.Length - 2; i++)
                {
                    if (LevDistance[i] > LevDistance[i + 1])
                    {
                        temp = LevDistance[i + 1];
                        LevDistance[i + 1] = LevDistance[i];
                        LevDistance[i] = temp;

                        temp2 = clone[i + 1];
                        clone[i + 1] = clone[i];
                        clone[i] = temp2;
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i+1 +" - "+ keyWords[clone[i]]+ " " + Percent(LevDistance[i], keyWords[clone[i]].Length) + " %");
            }
        }
    }
}
