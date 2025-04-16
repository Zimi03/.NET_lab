# Operacje wielowątkowe
Repo dotyczy zagadnienia wielowątkowości. Pierwsza część pokazuje problem mnożenia 2 macierzy kwadratowych i tego jak wykonanie tej operacji na wątkach przyspiesza wykonanie tego zadania. Mnożenie wykonano z użyciem biblioteki _Parallel_ oraz _Thread_, a nastętpnie porównano wyniki.
W drugiej części projektu wykonana została prosta aplikacja konsolowa w technologii Windows Forms, która pozwalała użytkownikowi podać obraz z dysku, a następnie dokonwywała 4 filtracji obrazu: Odcienie szarości, Progowanie, Negatyw, Odbicie lustrzane. Za każdą z operacji odpowiedzialny był osobny wątek.
# 1. Mnożenie macierzy na wątkach
Na początku została stworzona klasa, która pozwalała na mnożenie macierzy. Pola klasy: 
``` C#
class MatrixMultiplying
    {
        public int[,] matrix1 { get; set; }
        public int[,] matrix2 { get; set; }
        public int[,] resultMatrix { get; set; }
        public int n { get; set; }
        public int max_threads { get; set; }
        public long elapsedMilliseconds { get; private set; }
    }
```
n to rozmiar macierzy, max_threads to maksymalna liczba wątków, na której może być wykonana operacja, elapsedMilliseconds jest to zmienna, która przechowuje informacje o czasie w jakim została wykonana operacja mnożenia - potrzebne do benchmarku.
Konstruktor, który tworzy instancje problemu, z pseudolosowymi liczbami z użyciem biblioteki Random.
``` C#
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
```
Dzięki użyciu konstruktora bez podania seed'a, wiemy że liczby generowane przy tworzeniu instancji będą różne - co również jest potrzebne do benchmarku. 
Klasa również ma metody, które pozwalają na wyświetlanie każdej z 3 macierzy - potrzebne do sprawdzenia poprawności mnożenia.
## 1a. Mnożenie z wykorzystaniem biblioteki Parallel
``` C#
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
```
Mnożenie z wykorzystaniem biblioteki _Parallel_ wykonano z użyciem pętli _For_ dostępnej w bibliotece _Parallel_, która automatycznie przydziela zadania wątkom. 
## 1b. Mnożenie macierzy z użyciem biblioteki Thread
``` C#
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
```
Do wykonania mnożenia z użyciem biblioteki _Thread_ zastosowane zostało podejście, w którym każdy wątek dostawał odpowiednią liczbę wierszy, za wymnożenie których był odpwiedzialny. Liczba ta była zależna od ilości wątków, które wykonywały operacje oraz od wielkości macierzy. Jeżeli rozmiar macierzy nie był podzielny przez ilość wątków, ostatni wątek dostawał wszystkie wiersze pozostałe z dzielenia całkowitego.
Do mnożenia macierzy z wykorzystaniem biblioteki _Thread_ użyta została funkcja pomocnicza innerCalculateResult, która:
1. Była wywoływana w momencie tworzenia wątku
2. Pozwalała przydzielić konkretnym wątkom, konkretne wiersze macierzy, za wymnożenie których odpowiedzialny był konkretny wątek.

## 1c. Badania i wyniki
Badania zostały przeprowadzone w następujący sposób: 
wybrane zostały 3 rozmiary macierzy: 100, 200, 400, testy przeprowadzono na 3 ilościach wątków: 1, 2, 4. Dla każdego rozmiaru i ilości wątków wykonane zostało 5 powtórzeń dla różnych liczb w macierzach, wzięte zostały wyniki uśrednione. 
Parallel		
Rozmiar	Ilość wątków	średni czas[ms]
100	1	11,4
100	2	3,4
100	4	2,6
200	1	56,2
200	2	29,2
200	4	16,8
400	1	454,4
400	2	234
400	4	124![image](https://github.com/user-attachments/assets/da532fac-2582-4f3d-bdd3-ddd3dc2cbbe6)

Thread		
Rozmiar	Ilość wątków	średni czas[ms]
100	1	12,6
100	2	3,8
100	4	2,4
200	1	58,8
200	2	29,6
200	4	18,6
400	1	462,2
400	2	236,2
400	4	124![image](https://github.com/user-attachments/assets/7712e2ef-a7af-445d-badb-3ad18963b11a)










