using System;
using EntityGraphQL.Extensions;

namespace EntityGraphQL.Schema.FieldExtensions
{
    public static class UseConnectionPagingExtension
    {
        /// <summary>
        /// Update field to implement paging with the Connection<> classes and metadata.
        /// Only call on a field that returns an IEnumerable
        /// </summary>
        /// <param name="field"></param>
        /// <param name="defaultPageSize">If no values are passed for first or last arguments. This value will be applied to the first argument</param>
        /// <param name="maxPageSize">If either argument first or last is greater than this value an error is raised</param>
        /// <returns></returns>
        public static Field UseConnectionPaging(this Field field, int? defaultPageSize = null, int? maxPageSize = null)
        {
            if (!field.Resolve.Type.IsEnumerableOrArray())
                throw new ArgumentException($"UseConnectionPaging must only be called on a field that returns an IEnumerable");
            field.AddExtension(new ConnectionPagingExtension(defaultPageSize, maxPageSize));
            return field;
        }
    }
}