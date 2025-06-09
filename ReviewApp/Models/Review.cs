namespace ReviewApp.Models
{
    public class Review
    {
        public string AuthorName { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; } // Например, от 1 до 5
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
