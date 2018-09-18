
using GraphExpression.Serialization;
using System;
using System.Linq;
using Xunit;

namespace GraphExpression.Tests
{
    public class SerializationDynamicItemTest
    {
        [Fact]
        public void Normal()
        {
            var expression = Utils.CreateEmptyExpression();
            var serialization = Utils.GetSerialization(expression);
            var dynamicItem = new DynamicItemEntity(expression, "PropInt", 100);
            var result = dynamicItem.ToString();
            Assert.Equal("@PropInt: 100", result);
        }

        [Fact]
        public void PropertySymbol()
        {
            var expression = Utils.CreateEmptyExpression();
            var serialization = Utils.GetSerialization(expression);
            serialization.ItemsSerialize.OfType<DynamicItemSerialize>().First().Symbol = "*";
            var dynamicItem = new DynamicItemEntity(expression, "PropInt", 100);
            var result = dynamicItem.ToString();
            Assert.Equal("*PropInt: 100", result);
        }

        [Fact]
        public void ShowTypeFull()
        {
            var expression = Utils.CreateEmptyExpression();
            var serialization = Utils.GetSerialization(expression);
            serialization.ShowType = ShowTypeOptions.FullTypeName;
            var dynamicItem = new DynamicItemEntity(expression, "PropInt", 100);
            var result = dynamicItem.ToString();
            Assert.Equal("@System.Int32.PropInt: 100", result);
        }

        [Fact]
        public void ShowTypeNone()
        {
            var expression = Utils.CreateEmptyExpression();
            var serialization = Utils.GetSerialization(expression);
            serialization.ShowType = ShowTypeOptions.None;
            var dynamicItem = new DynamicItemEntity(expression, "PropInt", 100);
            var result = dynamicItem.ToString();
            Assert.Equal("@PropInt: 100", result);
        }

        [Fact]
        public void ShowTypeOnlyName()
        {
            var expression = Utils.CreateEmptyExpression();
            var serialization = Utils.GetSerialization(expression);
            serialization.ShowType = ShowTypeOptions.TypeName;
            var dynamicItem = new DynamicItemEntity(expression, "PropInt", 100);
            var result = dynamicItem.ToString();
            Assert.Equal("@Int32.PropInt: 100", result);
        }

        [Fact]
        public void ValueNull()
        {
            var expression = Utils.CreateEmptyExpression();
            var serialization = Utils.GetSerialization(expression);
            var dynamicItem = new DynamicItemEntity(expression, "PropString", null);
            var result = dynamicItem.ToString();
            Assert.Equal("@PropString: null", result);
        }

        [Fact]
        public void ValueNull_WithoutTypeAndSymbolProperty()
        {
            var expression = Utils.CreateEmptyExpression();
            var serialization = Utils.GetSerialization(expression);
            var dynamicItem = new DynamicItemEntity(expression, "PropString", null);
            serialization.ItemsSerialize.OfType<DynamicItemSerialize>().First().Symbol = null;

            var result = dynamicItem.ToString();
            Assert.Equal("PropString: null", result);
        }
    }
}
