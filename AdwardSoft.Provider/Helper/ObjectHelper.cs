using System;
using System.Collections.Generic;
using System.Text;

namespace AdwardSoft.Provider.Helper
{
    public static class ObjectHelper
    {
        public static T CreateInstance<T>(this T obj) where T : class, new()
        {
            return (obj == null ?  new T() : obj);
        }
    }
}
