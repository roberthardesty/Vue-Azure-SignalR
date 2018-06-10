using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ipman.shared.Utilities
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Used for producing a DataTable of InvoiceId for sending to SQL Server as a table parameter
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="outputColumnName"></param>
        /// <returns></returns>
        public static DataTable ToInvoiceIdDataTable<T>(this IEnumerable<T> enumerable, string outputColumnName = "Invoice _fact_PK")
        {
            var results = new DataTable();
            results.Columns.Add(outputColumnName, typeof(T));
            if (enumerable != null)
            {
                foreach (var row in enumerable)
                {
                    results.Rows.Add(row);
                }
            }
            return results;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable, string outputColumnName = "Id")
        {
            var results = new DataTable();
            results.Columns.Add(outputColumnName, typeof(T));
            if (enumerable != null)
            {
                foreach (var row in enumerable)
                {
                    results.Rows.Add(row);
                }
            }
            return results;
        }

        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            if (source != null)
            {
                // ReSharper disable once UnusedVariable
                foreach (object obj in source)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool HasMembers(this IEnumerable source)
        {
            return !source.IsNullOrEmpty();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source != null)
            {
                // ReSharper disable once UnusedVariable
                foreach (T obj in source)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool HasMembers<T>(this IEnumerable<T> source)
        {
            return !source.IsNullOrEmpty();
        }

        private static readonly Random Random = new Random();
        public static T PickRandomOrDefault<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable.ToList();
            if (list.Count == 0) return default(T);
            return list[Random.Next(list.Count)];
        }

        public static T PickRandom<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable.ToList();
            if (list.Count == 0) throw new NotSupportedException("No items in list to pick one from");
            return list[Random.Next(list.Count)];
        }

        public static async Task<IEnumerable<T>> WhereAsync<T>(this IEnumerable<T> items, Func<T, Task<bool>> predicate)
        {
            var itemTaskList = items.Select(item => new {Item = item, PredTask = predicate.Invoke(item)}).ToList();
            await Task.WhenAll(itemTaskList.Select(x => x.PredTask));
            return itemTaskList.Where(x => x.PredTask.Result).Select(x => x.Item);
        }
    }
}
