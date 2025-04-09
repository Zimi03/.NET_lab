using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread
{
    class ParallelMatrixMultiplying
    {
        public int[,] matrix1 { get; set; }
        public int[,] matrix2 { get; set; }
        public int[,] resultMatrix { get; set; }
        public int n { get; set; }
        public int max_threads { get; set; }
        public long elapsedMilliseconds { get; private set; }

        public ParallelMatrixMultiplying(int n, int max_threads)
        {          
            this.n = n;
            this.max_threads = max_threads;
            this.matrix1 = new int[n, n];
            this.matrix2 = new int[n, n];
            this.resultMatrix = new int[n, n];

            Random random = new Random();
            Random random2 = new Random();
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    this.matrix1[i,j] = random.Next(0,5);
                    this.matrix2[i,j] = random.Next(0,5);
                }
            }
        }

        public void ShowMatrix1()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(this.matrix1[i, j]+" ");
                    
                }
                Console.WriteLine();
            }
        }
        public void ShowMatrix2()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(this.matrix2[i, j] + " ");

                }
                Console.WriteLine();
            }
        }
        public void MultiplyMatricesParallel()
        { 
            ParallelOptions opt = new ParallelOptions() { MaxDegreeOfParallelism = max_threads };

            Stopwatch stopwatch = Stopwatch.StartNew();
            
            Parallel.For(0, n, opt, i =>
            {
                for (int j = 0; j < n; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < n; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    resultMatrix[i, j] = sum;
                }
            });

            stopwatch.Stop();
            elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        }
        public void ShowResultMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(resultMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(this.elapsedMilliseconds);
        }

    }
}
