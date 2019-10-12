using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Presentation.Mobile
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public string Author { get; set; }
        public List<Genre> Genres { get; set; }
        public string LastChapter { get; set; }
        public decimal BookRate { get; set; }
        public int CountRate { get; set; }
    }

    public class BookDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public List<Genre> Genres { get; set; }
        public decimal BookRate { get; set; }
        public int TotalChapter { get; set; }
        public DateTime TimeCreate { get; set; }
        public string Description { get; set; }
    }
}
