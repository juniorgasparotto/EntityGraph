﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionGraph.Reflection
{
    public class EnumerableReaderArray : IEnumerableReader
    {
        public bool CanRead(object value, Type type)
        {
            return value is Array;
        }

        public IEnumerable<MethodValue> GetValues(object value, Type type)
        {
            var array = value as Array;
            var keysValues = ReflectionUtils.ArrayToDictionary(array);

            foreach (var keyValue in keysValues)
            {
                var parameter = new MethodValueParam("indices", null, keyValue.Key);
                yield return new MethodValue(keyValue.Value, parameter);
            }
        }
    }
}