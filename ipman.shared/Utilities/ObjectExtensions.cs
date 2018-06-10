using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ipman.shared.Utilities
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts the string to TValue or, failing that, returns the specified default value
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue As<TValue>(this object value, TValue defaultValue = default(TValue))
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(TValue));

                if (converter.CanConvertFrom(typeof(string)))
                {
                    return (TValue) converter.ConvertFrom(value);
                }

                converter = TypeDescriptor.GetConverter(typeof(string));
                if (converter.CanConvertTo(typeof(TValue)))
                {
                    return (TValue) converter.ConvertTo(value, typeof(TValue));
                }
            }
            catch
            {
                return defaultValue;
            }
            return defaultValue;
        }

        /// <summary>
        /// Extension for 'Object' that copies the properties to a destination object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="shouldCopy">filter functon - property, source, and target values</param>
        public static void CopyProperties(this object source, object destination, Func<PropertyInfo, object, object, bool> shouldCopy = null )
        {
            // http://stackoverflow.com/questions/930433/apply-properties-values-from-one-object-to-another-of-the-same-type-automaticall
            
            // TODO: SG 9/3/2016 - Should profile CopyProperties() in the future and replace with hardcoded copies if its overused.
            // why?  Reflection for copying -> very often misused. 
            
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            // Iterate the Properties of the source instance and  
            // populate them from their desination counterparts  
            PropertyInfo[] srcProps = typeSrc.GetProperties();

            foreach (PropertyInfo srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }
                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }
                // Passed all tests. 

                var srcVal = srcProp.GetValue(source, null);
                var targetVal = targetProperty.GetValue(destination, null);

                if (shouldCopy != null)
                {
                    if (!shouldCopy(targetProperty, srcVal, targetVal))
                        continue; 
                }

                targetProperty.SetValue(destination, srcVal, null);
            }
        }

        /// <summary>
        /// returns true if the item is in the list of items provided
        /// </summary>
        /// <param name="value"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool In<T>(this T value, params T[] list)
        {
            return list.Contains(value);
        }


        /// <summary>
        /// Extension for 'Object' that verifies that all properties are the same. 
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="shouldCompare">filter functon - property, source, and target values</param>
        public static bool PropertyEquals(this object source, object destination, 
            Func<PropertyInfo, object, object, bool> shouldCompare = null)
        {
            // http://stackoverflow.com/questions/930433/apply-properties-values-from-one-object-to-another-of-the-same-type-automaticall

            // TODO: SG 9/3/2016 - Should profile CopyProperties() in the future and replace with hardcoded copies if its overused.
            // why?  Reflection for copying -> very often misused. 

            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            // Iterate the Properties of the source instance and  
            // populate them from their desination counterparts  
            PropertyInfo[] srcProps = typeSrc.GetProperties();

            foreach (PropertyInfo srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }
                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }
                // Passed all tests. 

                var srcVal = srcProp.GetValue(source, null);
                var targetVal = targetProperty.GetValue(destination, null);

                if (shouldCompare != null)
                {
                    if (!shouldCompare(targetProperty, srcVal, targetVal))
                        continue;
                }

                if (srcVal == null)
                {
                    if (targetVal == null) continue;
                    return false;
                }
                if (srcVal.Equals(targetVal)) continue;  // still equal! 
                return false; 
            }
            return true; 
        }

        /// <summary>
        /// gets the first instance of an attribute of the given type from this object's type, if it exists
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
        }

        public static void SetPropertyValue(this object obj, string propertyName, object val)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            var objType = obj.GetType();
            var propInfo = GetPropertyInfo(objType, propertyName);
            if (propInfo == null)
            {
                throw new ArgumentOutOfRangeException(nameof(propertyName), $"Couldn't find property {propertyName} in type {objType.FullName}");
            }
            propInfo.SetValue(obj, val, null);
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            var objType = obj.GetType();
            var propInfo = GetPropertyInfo(objType, propertyName);
            if (propInfo == null)
            {
                throw new ArgumentOutOfRangeException(nameof(propertyName), $"Couldn't find property {propertyName} in type {objType.FullName}");
            }
            return propInfo.GetValue(obj, null);
        }

        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propInfo;
            do
            {
                propInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            } while (propInfo == null && type != null);
            return propInfo;
        }

        public static void SetFieldValue(this object obj, string fieldName, object val)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            var objType = obj.GetType();
            var propInfo = GetFieldInfo(objType, fieldName);
            if (propInfo == null)
            {
                throw new ArgumentOutOfRangeException(nameof(fieldName), $"Couldn't find property {fieldName} in type {objType.FullName}");
            }
            propInfo.SetValue(obj, val);
        }

        private static FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            FieldInfo fieldInfo;
            do
            {
                fieldInfo = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField);
                type = type.BaseType;
            } while (fieldInfo == null && type != null);
            return fieldInfo;
        }
    }
}
