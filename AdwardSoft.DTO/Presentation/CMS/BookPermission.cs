using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Presentation.CMS
{
    public class BookPermission
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public long UserId { get; set; }
    }
}
