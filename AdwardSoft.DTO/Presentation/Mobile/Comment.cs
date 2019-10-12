using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Presentation.Mobile
{
    public class Comment
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string ParentId { get; set; }
    }
}
