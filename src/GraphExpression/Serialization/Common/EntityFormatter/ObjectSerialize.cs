﻿using System;

namespace GraphExpression.Serialization
{
    public class ObjectSerialize : IEntitySerialize
    {
        public bool CanSerialize(ComplexEntityExpressionSerializer serializer, EntityItem<object> item)
        {
            return true;
        }

        public (Type Type, string ContainerName) GetSerializeInfo(ComplexEntityExpressionSerializer serializer, EntityItem<object> item)
        {
            return (
                item.Entity?.GetType(),
                null
            );
        }
    }
}
