using System;
using System.Collections.Generic;
using System.Linq;
using Nethereum.ABI.Model;

namespace Ethereum.Connector.Application.Common.Extensions
{
    /// <summary>
    /// Contract parameter extensions
    /// </summary>
    public static class ContractParameterExtensions
    {
        /// <summary>
        /// Get arguments.
        /// </summary>
        /// <param name="parameters">List of parameters.</param>
        /// <param name="obj">Object</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Array of object arguments.</returns>
        public static object[] GetArguments<T>(this IEnumerable<Parameter> parameters, T obj)
        {
            var enumerable = parameters as Parameter[] ?? parameters.ToArray();
            if (obj == null || !enumerable.Any())
                return new object[] { };

            var result = new object[enumerable.Count()];
            var properties = obj.GetPropertiesInfo();
            var orderedParameters = enumerable.OrderBy(x => x.Order);

            var i = 0;
            foreach (var parameter in orderedParameters)
            {
                var property = properties
                    .FirstOrDefault(x => string.Equals(
                        x.Name, 
                        parameter.Name, 
                        StringComparison.InvariantCultureIgnoreCase));
                
                if (property == null) continue;

                var value = property.GetValue(obj, null);
                result[i++] = value.ToString();
            }

            return result;
        }
    }
}