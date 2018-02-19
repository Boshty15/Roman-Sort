using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Vaja01
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = Int32.MinValue;
            int min = Int32.MaxValue;

            string datoteka = args[1];

            //branje datoteke
            string fileContent = File.ReadAllText(datoteka);
            string[] integerStrings = fileContent.Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
            int[] A = new int[integerStrings.Length];
            
            for (int i = 0; i < integerStrings.Length; i++)
            {
                A[i] = int.Parse(integerStrings[i]);
                if(A[i] > max)
                {
                    max = A[i];
                }
                if(A[i] < min)
                {
                    min = A[i];
                }
            }
            
            if (args.Length == 2)
            {
                if(Int32.Parse(args[0]) == 0)
                {
                    //counting sort
                    Console.WriteLine("Counting sort");

                    int[] B = countingSort(A, min, max);
                    zapisVDaoteko(B);
                }
                if (Int32.Parse(args[0]) == 1)
                {
                    //roman sort
                    Console.WriteLine("Roman sort");

                    int[] B = romanSort(A, max, min);
                    zapisVDaoteko(B);
                }
            }
        }
        private static void zapisVDaoteko(int[] B)
        {
            for (int i = 0; i < B.Length; i++)
            {
                using (FileStream fs = new FileStream("out.txt",FileMode.Append ,FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(B[i] + " ");
                }
            }
        }
        private static int[] romanSort(int[] A, int max,int min)
        {
            int[] C = new int[max-min +1];
            for(int i = 0; i < C.Length; i++)
            {
                C[i] = 0;
            }
            for(int i = 0; i < A.Length; i++)
            {
                C[A[i]-min] += 1;
            }
            int m = 0;
            int n = min;
            for(int i = 0; i < C.Length; i++)
            {
                if(C[i] != 0)
                {
                    for(int j = 0; j < C[i]; j++)
                    {
                        A[m] = n;
                        m++;
                    }
                }
                n++;
            }
            return A;
        }
        private static int[] countingSort(int[] A, int min, int max)
        {
            int[] C = new int[max - min + 1];
            int z = 0;

            for (int i = 0; i < C.Length; i++)
            {
                C[i] = 0;
            }
            for (int i = 0; i < A.Length; i++)
            {
                C[A[i] - min]++;
            }

            for (int i = min; i <= max; i++)
            {
                while (C[i - min]-- > 0)
                {
                    A[z] = i;
                    z++;
                }
            }
            return A;
        }
    }
   
}
