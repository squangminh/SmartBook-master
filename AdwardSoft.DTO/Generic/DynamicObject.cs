using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.DTO.Generic
{
    public class DynamicObject<T>
    {
        public T Response { get; set; }
        public string Messages { get; set; }

    }
}
