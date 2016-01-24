using BaoVietCore.Interfaces;

namespace BaoVietCore.Models
{
    public class Category
    {
        public IPaper Owner { get; set; }
        public string Title {get;set;}
        public string Source { get; set; }

        public Category(string title, string source)
        {
            Title = title;
            Source = source;
        }
    }
}