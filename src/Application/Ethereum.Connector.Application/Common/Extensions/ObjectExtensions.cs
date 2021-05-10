using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ethereum.Connector.Application.Common.Extensions
{
    /// <summary>
    /// Object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Get properties information.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <returns>List of property info.</returns>
        public static IEnumerable<PropertyInfo> GetPropertiesInfo(this object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties().ToList();

            return properties;
        }
    }
}