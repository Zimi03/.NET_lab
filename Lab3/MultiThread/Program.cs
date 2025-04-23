using System.IO;
namespace MultiThread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var methods = new Dictionary<string, Action<MatrixMultiplying>>
            {           
                { "MultiplyMatricesThread", m => m.MultiplyMatricesThread() },
                { "MultiplyMatricesParallel", m => m.MultiplyMatricesParallel() }
            };

            foreach (var method in methods)
            {
                BenchmarkMethod(method.Key, method.Value, method.Key + ".txt");
            }
        }

        
        static void BenchmarkMethod(string methodName, Action<MatrixMultiplying> multiplyMethod, string filePath)
        {
            int[] sizes = { 100, 200, 400, 1000};
            int[] threadCounts = { 1, 2, 4, 8 };
            int repetitions = 5;

            // Dopisz do pliku jeśli istnieje, albo utwórz nowy
            bool fileExists = File.Exists(filePath);
            using (var writer = new StreamWriter(filePath, append: true))
            {
                if (!fileExists)
                    writer.WriteLine("Metoda, Rozmiar, Ilość wątków, Średni czas [ms]");

                foreach (int size in sizes)
                {
                    foreach (int threads in threadCounts)
                    {
                        long[] times = new long[repetitions];

                        for (int i = 0; i < repetitions; i++)
                        {
                            var multiplier = new MatrixMultiplying(size, threads);
                            multiplyMethod(multiplier);
                            times[i] = multiplier.elapsedMilliseconds;
                        }

                        double avgTime = times.Average();
                        Console.WriteLine($"[{methodName}] Rozmiar: {size}x{size}, Wątki: {threads}, Średni czas: {avgTime} ms");
                        writer.WriteLine($"{methodName};{size};{threads};{avgTime}");
                    }
                }
            }

        }
    }
}
