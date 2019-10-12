using System;

namespace AdwardSoft.ValueObjects
{
    public class MultipleDataEntry
    {
        public Type Type { get; private set; }
        public DataRetriveTypeEnum DataRetriveType { get; private set; }
        public string PropertyName { get; private set; }

        public MultipleDataEntry(Type type, DataRetriveTypeEnum dataRetriveType, string propertyName)
        {
            Type = type;
            DataRetriveType = dataRetriveType;
            PropertyName = propertyName;
        }
    }

    public enum DataRetriveTypeEnum
    {
        FirstOrDefault,
        List
    }
}
