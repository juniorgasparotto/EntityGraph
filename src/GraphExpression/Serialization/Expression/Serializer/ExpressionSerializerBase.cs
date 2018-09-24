﻿using GraphExpression.Utils;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace GraphExpression.Serialization
{
    public abstract class ExpressionSerializerBase<T> : ISerialize<T>
    {
        private const string DEFAULT_QUOTE = @"""";

        private IValueFormatter valueFormatter;
        private readonly Expression<T> expression;

        public bool EncloseParenthesisInRoot { get; set; }
        public bool RemoveQuoteWhenValidIdentified{ get; set; }

        public IValueFormatter ValueFormatter
        {
            get
            {
                if (valueFormatter == null)
                    valueFormatter = new DefaultValueFormatter();
                return valueFormatter;
            }
            set
            {
                valueFormatter = value;
            }
        }

        public ExpressionSerializerBase(Expression<T> expression)
        {
            this.expression = expression;
        }

        public virtual string Serialize()
        {
            var output = "";
            expression.IterationAll
                (
                    itemBeginGroupExpression =>
                    {
                        if (itemBeginGroupExpression.IsRoot)
                        {
                            if (EncloseParenthesisInRoot)
                                output = "(";

                            output += GetEnclosedValue(SerializeItem(itemBeginGroupExpression));
                        }
                        else
                        {
                            output += " + ";
                            if (itemBeginGroupExpression.IsFirstInParent)
                                output += "(";

                            output += GetEnclosedValue(SerializeItem(itemBeginGroupExpression));
                        }
                    },
                    itemEndGroupExpression =>
                    {
                        if (!itemEndGroupExpression.IsRoot || (itemEndGroupExpression.IsRoot && EncloseParenthesisInRoot))
                            output += ")";
                    }
                );

            return output;
        }

        public abstract string SerializeItem(EntityItem<T> item);

        private string GetEnclosedValue(string value)
        {
            var encloseStart = DEFAULT_QUOTE;
            var encloseEnd = DEFAULT_QUOTE;

            if (RemoveQuoteWhenValidIdentified 
                && ReflectionUtils.IsValidIdentifier(value)
                && !ReflectionUtils.IsCSharpKeyword(value))
            {
                encloseStart = null;
                encloseEnd = null;
            }

            return $"{encloseStart}{value}{encloseEnd}";
        }
    }
}
