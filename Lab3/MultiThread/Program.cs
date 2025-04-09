using System.IO;
namespace MultiThread
{
    internal class Program
    {
        static void Main(string[] args)
        {

            BenchmarkMatrixMultiplication("ParallelResults.txt");
        }
        static void BenchmarkMatrixMultiplication(string filePath)
        {
            int[] sizes = { 100, 200, 400 };
            int[] threadCounts = { 1, 2, 4 };
            int repetitions = 5;

            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Rozmiar, Ilość wątków, średni czas[ms]");
                foreach (int size in sizes)
                {
                    foreach (int threads in threadCounts)
                    {
                        long[] totalTime = new long[repetitions];

                        for (int i = 0; i < repetitions; i++)
                        {
                            var multiplier = new ParallelMatrixMultiplying(size, threads);
                            multiplier.MultiplyMatricesParallel();
                            totalTime[i] = multiplier.elapsedMilliseconds;
                        }

                        double averageTime = totalTime.Average();
                        Console.WriteLine($"Rozmiar: {size}x{size}, Wątki: {threads}, Średni czas: {averageTime} ms");
                        writer.WriteLine($"{size},{threads},{averageTime}");
                    }
                }
            }
        }

    }
}
