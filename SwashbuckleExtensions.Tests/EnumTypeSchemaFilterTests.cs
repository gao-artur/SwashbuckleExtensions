using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwashbuckleExtensions.Tests
{
    [TestClass]
    public class EnumTypeSchemaFilterTests
    {
        private enum TestEnum
        {
            Val1 = 1,
            Val2 = 3
        }

        [TestInitialize]
        public void TestInit()
        {
            _schema = new Schema();
            _filter = new EnumTypeSchemaFilter();
        }

        private const string XTag = "x-ms-enum";

        private Schema _schema;
        private ISchemaFilter _filter;

        [TestMethod]
        public void Enum_ContainsTag()
        {
            Type type = typeof(TestEnum);
            SchemaFilterContext context = CreateContext(type);

            _filter.Apply(_schema, context);

            Assert.IsNotNull(_schema.Extensions);
            Assert.IsTrue(_schema.Extensions.Keys.Contains(XTag));
        }

        [TestMethod]
        public void NullableEnum_ContainsTag()
        {
            Type type = typeof(TestEnum?);
            SchemaFilterContext context = CreateContext(type);

            _filter.Apply(_schema, context);

            Assert.IsNotNull(_schema.Extensions);
            Assert.IsTrue(_schema.Extensions.Keys.Contains(XTag));
        }

        [TestMethod]
        public void Guid_NoTag()
        {
            Type type = typeof(Guid);
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