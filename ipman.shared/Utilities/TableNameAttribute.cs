using System;

namespace ipman.shared.Utilities
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TableNameAttribute : Attribute
    {
        public TableNameAttribute(string tableName)
        {
            TableName = tableName;
        }

        public string TableName { get; private set; }
    }

}