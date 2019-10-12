using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Presentation.Mobile
{
    public class Chapter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreate { get; set; }
        public int View { get; set; }
    }

    public class ChapterDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreate { get; set; }
        public int View { get; set; }
        public string Content { get; set; }
        public string Creater { get; set; }
    }
}
