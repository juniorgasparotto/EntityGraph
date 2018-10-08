﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace GraphExpression
{
    public class PropertyReader : IMemberReader
    {
        public IEnumerable<ComplexEntity> GetMembers(ComplexExpressionFactory builder, Expression<object> expression, object entity)
        {
            // get all propertis: 
            // 1) ignore indexed (this[...]) with GetIndexParameters > 0
            // 2) ignore properties with only setters
            var properties = builder.GetProperties(entity);
            foreach (var p in properties)
                if (!p.GetIndexParameters().Any() && p.GetGetMethod() != null)
                    yield return new PropertyEntity(expression, entity, p);
        }
    }
}