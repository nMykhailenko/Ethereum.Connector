using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ethereum.Connector.Application.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<PropertyInfo> GetPropertiesInfo(this object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties().ToList();

            return properties;
        }
    }
}