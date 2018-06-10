using System;
using System.Linq;
using System.Reflection;

namespace ipman.shared.Utilities
{

    public static class EnumExtensions
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example>string desc = myEnumVariable.GetAttribute<DescriptionAttribute>().Description;</example>
        public static T GetAttribute<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes<T>();
            return attributes.FirstOrDefault();
        }
    }
}
