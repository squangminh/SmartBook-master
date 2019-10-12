using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.ValueObjects.Generic
{
    public class DataService<T>
    {
        public T Response { get; set; }
        public string Messages { get; set; }
        public bool Success { get; set; }
    }
}
