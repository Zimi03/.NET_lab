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

## 1c. Wyniki









