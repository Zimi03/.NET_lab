# Blazor Lab
#1. Przerobienie podstawowych stron, wygenerowanych przez blazor
Pierwszym zadaniem w laboratorium było wygenerowanie projektu w technologii blazor, bez autoryzacji użytkownika oraz przerobienie komponentu: Weather.
W ramach komponentu należało: 
1. Wydłużyć prognozę do 10 dni (zmiana w funkcji Range generującej tablicę prognoz)
2. Stowrzyć zmienną _warmDays_, która będzie przechowywała liczbę ciepłych dni (powyżej 15\&deg;C)
```CSharp
warmDaysCount = VisibleForecasts.Count(f => f.TemperatureC > 15);
```
3.Wyświetlić liczbę ciepłych dni na ekranie w sekcji HTML
``` HTML
<p>Number of warm days: @warmDaysCount</p>
```
4. Dodać przycisk, który pozwoli w liście odfiltrować ciepłe dni
``` HTML
<p><button class="btn btn-primary me-2" @onclick="() => WarmDaysFilter()">Filtruj ciepłe dni</button></p>
```
```CSharp
private void WarmDaysFilter()
    {
        VisibleForecasts = OriginalForecasts?.Where(f => f.TemperatureC > 15).ToArray(); 
    }
```
5. Dodać filtrowanie listy po polu summary
``` HTML
<div ="mb-3">
        <label for="filterInput" class="form-label">Filtr po opisie:</label>
        <input id="filterInput" class="form-control"@oninput=@Input/>
</div>
```
``` CSharp
 private void Input(ChangeEventArgs arg)
    {
        string filter = arg.Value?.ToString() ?? string.Empty;
        VisibleForecasts = VisibleForecasts?.Where(f => f.Summary != null && f.Summary.Contains(filter)).ToArray();
    }
```

#2. Prosta aplikacja bazodanowa - ocenianie książek
Drugim etapem było stworzenie prostej aplikacji bazodanowej, tym razem z autoryzacją użytkownika, która pozwalała użytkownikom na ocenianie książek. 
W pierwszym etapie stworzono aplikację z użyciem VS w technologii blazor z autoryzacją użytkownika. 
Następnie utworzono bazę danych z jedną tabelą, przechowującą informacje o książkach:
```CSharp
public class Book
    {
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:dd-MM-yyyy", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
        public float Rate { get; set; }
        public string? ShortReview { get; set; }
        public string? PhotoUri {get; set;}
    }
```
Następnie utworzono Element szkieletowy korzystając z Razor Compontens using Entity Framework (CRUD). 
Do automatycznie wygenerowanych komponentów wprowadzono poniższe zmiany:
1. W widoku _detalis_ dodano możliwość dodania oceny - nowa ocena była średnią nowododanej oceny i obecnej oceny
``` HTML
<EditForm Model="@this" OnValidSubmit="AddRating">
            <div class="mb-3">
                <label for="newRating" class="form-label">New Rating (1-10)</label>
                <InputNumber @bind-Value="newRating" class="form-control" min="1" max="10" />
            </div>
            <button class="btn btn-primary" type="submit">Add Rating</button>
        </EditForm>
```
```CSharp
private async Task AddRating()
    {
        if (book is null) return;

        using var context = DbFactory.CreateDbContext();
        var currentBook = await context.Book.FirstOrDefaultAsync(b => b.Id == Id);

        if (currentBook is null) return;

        // Prosta średnia: nowa = (stara + nowa) / 2
        currentBook.Rate = (currentBook.Rate + newRating) / 2;

        await context.SaveChangesAsync();
        await LoadBookAsync(); // reload updated data
    }
```
2. W widoku index dodano możliwość sortowania po różnych kolumnach - sprowadziło się to do dodania atrubutu Sortable="true" do każdego PropertyColumn w QuickGrid oraz przekazania na front listy książek w postaci IQueryable<Book>.
3. Usunięcia z widoku _index_ Opisu oraz linku do zdjęcia
4. Dostęp do stron: _Edit_, _Delete_ został ograniczony tylko dla autoryzowanych użytkowników
5. Do _menu_ została dodana opcja przejścia do widoku Index
6. W widoku _Details_ wyświetlane jest zdjęcie książki przechowywane w bazie jako url
7. Dodano opcje autoryzacji przez konto google
