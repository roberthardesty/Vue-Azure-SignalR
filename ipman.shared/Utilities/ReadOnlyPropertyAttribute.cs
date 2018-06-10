using System;

namespace ipman.shared.Utilities
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ReadOnlyPropertyAttribute : Attribute
    {

    }
}