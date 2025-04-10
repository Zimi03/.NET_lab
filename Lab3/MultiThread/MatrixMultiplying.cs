using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread
{
    class MatrixMultiplying
    {
        public int[,] matrix1 { get; set; }
        public int[,] matrix2 { get; set; }
        public int[,] resultMatrix { get; set; }
        public int n { get; set; }
        public int max_threads { get; set; }
        public long elapsedMilliseconds { get; private set; }

        public MatrixMultiplying(int n, int max_threads)
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
            Console.WriteLine();
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
            Console.WriteLine();
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
        public void MultiplyMatricesThread()
        { 
            Stopwatch stopwatch = Stopwatch.StartNew();

            Thread[] threads = new Thread[max_threads];
            int rowsPerThread = n / max_threads;

            for (int i = 0;i<this.max_threads; i++)
            {
                int startRow = i * rowsPerThread;
                int endRow = (i == max_threads-1) ? n : startRow + rowsPerThread;

                threads[i] = new Thread(() => innerCalculateResult(startRow, endRow));
                threads[i].Start();
            }
            foreach(Thread t in threads)
            {
                t.Join();
            }
            stopwatch.Stop();
            elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        }
        private void innerCalculateResult(int startRow, int endRow)
        {
            for (int i = startRow; i < endRow; i++)
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
            }
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
            Console.WriteLine();
        }

    }
}
