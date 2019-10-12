using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Presentation.CMS
{
    public class Comment
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }
    }
}
