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

<p align="center">
  <img width="400" height="600" src="https://github.com/user-attachments/assets/d3b44d84-c2bf-43ac-98a5-bbe8ca837ff5" alt="Benchmark mnożenia macierzy">
  <br>
  <em>Wyniki benchmarku dla mnożenia macierzy z użyciem biblioteki Parallel</em>
</p>

<p align="center">
  <img width="400" height="600" src="https://github.com/user-attachments/assets/cacfa83b-2a11-412c-9959-4d9211ef619a">
   <br>
  <em>Wyniki benchmarku dla mnożenia macierzy z użyciem biblioteki Parallel</em>
</p>


## 1d. Wnioski
### Wielowątkowość
Proces mnożenia macierzy jest idealnym problemem do zrównoleglenia, poniewać każdy wątek działa na osobnym zasobie, dzięki czemu nie trzeba blokować zasobów. To przekłada się na liniowe przyspieszenie wykonywanych operacji. Czas skrócenia operacji jest proporcjonalny do ilości wątków między które podzielone są zadania.
### Biblioteka Thread a Parallel
Udało się uzyskać bardzo zbliżone wyniki pomiędzy bibliotekami. W większości przypadków operacja z użyciem biblioteki _Parallel_ była szybsza, jednak były takie przypadki kiedy to _Thread_ okazał się szybszy. W większości przypadków biblioteka _Parallel_ jest szybsza, jest tak ze względu na to że działamy wysokopoziomowo, a biblioteka sama w sobie ma optymalnie rozwiązane działania niskopoziomowe. 
Z kolei zbliżenie się wynikami z użyciem biblioteki _Thread_ świadczy o dobrym podejściu do niskopoziomowego zarządzania wątkami - dobrym podzieleniu zadań między wątki.
### Ilość zadeklarowanych wątków, a ilość wątków fizycznych
Testy zostały przeprowadzone na wirtualnej maszynie, która miała do dyspozycji 4 fizyczne wątki. Przy testach dla 8 wątków, widać, że lepiej radzi sobie biblioteka _Parallel_. Jest tak dlatego, że biblioteka _Thread_ marnuje czas na utworzenie i przydzielenie zadań wątkom, których fizycznie nie ma. Biblioteka _Parallel_ nawet jeżeli dostanie informację o wykorzystaniu większej ilości wątków niż jest fizycznie dostępne, wykorzysta liczbę maksymalnie dostępnych. Chociaż i z jej wykorzystaniem widać dłuższe czasy dla 8 wątków niż dla 4.
# 2. Wielowątkowe przetwarzanie obrazów - GUI
<p align="center">
<img width="1279" alt="image" src="https://github.com/user-attachments/assets/2b8c38c3-8295-4bf7-b3f5-592c9e3fd4b9" />
<br>
<em>Aplikacja okienkowa do wielowątkowego przetwarzania obrazu</em>
</p>
W tej części projektu stworzona została aplikacja do wielowątkowego przetwarzania obrazu. Jeden wątek był odpowiedzialny za jedną operację przetworzenia. W aplikacji zastosowane zostały 4 operajce przetwarzania:

1. Odcienie szarości
2. Progowanie
3. Negatyw
4. Odbicie lustrzane

Filtry w kodzie
``` C#
public static class Filters
    {
        public static Bitmap ToGrayScale(Bitmap source)
        {
            Bitmap result = new Bitmap(source);
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    Color pixel = result.GetPixel(x, y);
                    int gray = (int)(0.3 * pixel.R + 0.59 * pixel.G + 0.11 * pixel.B);
                    Color grayColor = Color.FromArgb(gray, gray, gray);
                    result.SetPixel(x, y, grayColor);
                }
            }
            return result;
        }

        public static Bitmap ToThreshold(Bitmap source, int threshold = 128)
        {
            Bitmap result = new Bitmap(source);
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    Color pixel = result.GetPixel(x, y);
                    int gray = (int)(0.3 * pixel.R + 0.59 * pixel.G + 0.11 * pixel.B);
                    Color color = gray < threshold ? Color.Black : Color.White;
                    result.SetPixel(x, y, color);
                }
            }
            return result;
        }

        public static Bitmap ToNegative(Bitmap source)
        {
            Bitmap result = new Bitmap(source);
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    Color pixel = result.GetPixel(x, y);
                    Color negColor = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    result.SetPixel(x, y, negColor);
                }
            }
            return result;
        }

        public static Bitmap ToMirror(Bitmap source)
        {
            Bitmap result = new Bitmap(source);
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width / 2; x++)
                {
                    Color left = result.GetPixel(x, y);
                    Color right = result.GetPixel(result.Width - x - 1, y);
                    result.SetPixel(x, y, right);
                    result.SetPixel(result.Width - x - 1, y, left);
                }
            }
            return result;
        }
    }
```
Przetwarzanie obrazu po naciśnięciu guzika
``` C#
private void button2_Click(object sender, EventArgs e)
        {
            if (img == null)
            {
                MessageBox.Show("Provide Image First");
                return;
            }

            var sourceClones = new Bitmap[] {
                (Bitmap)img.Clone(),
                (Bitmap)img.Clone(),
                (Bitmap)img.Clone(),
                (Bitmap) img.Clone()
            };

            Action[] filters = new Action[]
            {
                () => {GreyScale.Image = Filters.ToGrayScale(sourceClones[0]); },
                () => {Threshold.Image = Filters.ToThreshold(sourceClones[1]); },
                () => {Negative.Image = Filters.ToNegative(sourceClones[2]); },
                () => {Mirroring.Image = Filters.ToMirror(sourceClones[3]); }
            };


            Parallel.For(0, filters.Length, i =>
            {
                filters[i]();
            });
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;

        }
```




