﻿using System;
using System.Collections.Generic;
using System.Reflection;


namespace GraphExpression
{
    public interface IEntityReader
    {
        bool CanRead(ComplexExpressionFactory builder, object entity);
        IEnumerable<ComplexEntity> GetChildren(ComplexExpressionFactory builder, Expression<object> expression, object entity);
    }
}