using System;
using Specifications4Net.Criterion;
using Xunit;

namespace Specifications4Net.Test.Filter
{
    public class SimpleCriterionTest
    {
        private readonly Person _person = new Person() {Name = "Tom", Age = 20};

        [Theory]
        [InlineData("Name", "eq", "Tom", true)]
        [InlineData("SSN", "eq", null, true)]
        [InlineData("Age", "eq", 20, true)]
        [InlineData("Name", "eq", null, false)]
        [InlineData("Age", "eq", 1000, false)]
        [InlineData("Age", "gt", 19, true)]
        [InlineData("Age", "gte", 20, true)]
        [InlineData("Age", "gte", 21, false)]
        [InlineData("Age", "lt", 21, true)]
        [InlineData("Age", "lte", 20, true)]
        [InlineData("Age", "lte", 19, false)]
        [InlineData("Name", "neq", null, true)]
        [InlineData("Name", "neq", "Tom", false)]
        public void ToSpecification_SuccessScenario(string property, string op, object value, bool expectedResult)
        {
            Assert.Equal(expectedResult,
                new SimpleCriterion(property, value, op).ToSpecification<Person>().Predicate(_person));
        }

        [Fact]
        public void ToSpecification_MissingProperty_ThrowsError()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new SimpleCriterion("Foo", new object(), "eq").ToSpecification<Person>());
        }

        private class Person
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public string SSN { get; set; }
        }
    }
}
