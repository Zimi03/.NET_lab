using System.ComponentModel.DataAnnotations;

namespace RatingApp.Components
{
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
}
