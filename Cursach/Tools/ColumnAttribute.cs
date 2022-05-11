using System;

namespace Cursach.Tools
{
    internal class ColumnAttribute : Attribute
    {
        public string Column { get; }
        public ColumnAttribute(string column)
        {
            Column = column;
        }
    }
}