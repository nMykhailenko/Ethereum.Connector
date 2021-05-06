using System;
using System.Collections.Generic;
using System.Linq;
using Nethereum.ABI.Model;

namespace Ethereum.Connector.Application.Common.Extensions
{
    public static class ContractParameterExtension
    {
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