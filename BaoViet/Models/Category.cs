namespace BaoViet.Models
{
    public class Category
    {
        public string Title {get;set;}
        public string Source { get; set; }

        public Category(string title, string source)
        {
            Title = title;
            Source = source;
        }
    }
}