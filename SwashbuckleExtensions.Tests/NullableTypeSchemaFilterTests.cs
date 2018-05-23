using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleExtensions.Tests
{
    [TestClass]
    public class NullableTypeSchemaFilterTests
    {
        private const string XTag = "x-nullable";

        private Schema _schema;
        private ISchemaFilter _filter;

        [TestInitialize]
        public void TestInit()
        {
            _schema = new Schema();
            _filter = new NullableTypeSchemaFilter();
        }

        [TestMethod]
        public void GuidType_XNullableFalse()
        {
            Type type = typeof(Guid);
            SchemaFilterContext context = CreateContext(type);
            
            _filter.Apply(_schema, context);

            Assert.IsNotNull(_schema.Extensions);
            Assert.IsTrue(_schema.Extensions.Keys.Contains(XTag));
            Assert.AreEqual(false, _schema.Extensions[XTag]);
        }

        [TestMethod]
        public void NullableGuidType_NoXNullable()
        {
            Type type = typeof(Guid?);
            SchemaFilterContext context = CreateContext(type);

            _filter.Apply(_schema, context);

            Assert.IsNotNull(_schema.Extensions);
            Assert.IsFalse(_schema.Extensions.Keys.Contains(XTag));
        }

        [TestMethod]
        public void DictionaryWithIntValue_XNullableFalse()
        {
            Type type = typeof(Dictionary<string, int>);
            SchemaFilterContext context = CreateContext(type);

            _filter.Apply(_schema, context);

            Assert.IsNotNull(_schema.Extensions);
            Assert.IsTrue(_schema.Extensions.Keys.Contains(XTag));
            Assert.AreEqual(false, _schema.Extensions[XTag]);
        }

        [TestMethod]
        public void DictionaryWithNullableIntValue_NoXNullable()
        {
            Type type = typeof(Dictionary<string, int?>);
            SchemaFilterContext context = CreateContext(type);

            _filter.Apply(_schema, context);

            Assert.IsNotNull(_schema.Extensions);
            Assert.IsFalse(_schema.Extensions.Keys.Contains(XTag));
        }

        private SchemaFilterContext CreateContext(Type type)
        {
            return new SchemaFilterContext(type,
                new JsonPrimitiveContract(type),
                new SchemaRegistry(new JsonSerializerSettings()));
        }
    }
}
