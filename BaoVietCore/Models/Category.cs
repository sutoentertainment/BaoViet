using BaoVietCore.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BaoVietCore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public PaperBase Owner { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }

        public Category(string title, string source)
        {
            Title = title;
            Source = source;
        }
    }
}