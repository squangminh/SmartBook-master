using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Presentation.CMS
{
    public class Chapter
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime TimeCreate { get; set; }
        public long CreateUserId { get; set; }
        public long View { get; set; }
        public int BookId { get; set; }
    }
}
