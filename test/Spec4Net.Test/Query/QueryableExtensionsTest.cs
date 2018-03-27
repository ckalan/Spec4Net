using System.Collections.Generic;
using System.Linq;
using Spec4Net.Query;
using Xunit;

namespace Spec4Net.Test.Query
{
    public class QueryableExtensionsTest
    {
        [Fact]
        public void OrderByProperties()
        {
            var people = new List<Person>()
            {
                new Person() {Id = 1, Age = 20, Name = "tom", Wage = 400},
                new Person() {Id = 2, Age = 20, Name = "sam", Wage = 100},
                new Person() {Id = 3, Age = 20, Name = "tom", Wage = 100},
                new Person() {Id = 4, Age = 30, Name = "tom", Wage = 300},
            };

            var ordered = people.AsQueryable().OrderBy(("Age", ""), ("Name", "Desc"), ("Wage", "")).Select(t => t.Id)
                .ToArray();

            Assert.Equal(new[] {3, 1, 2, 4}, ordered);
        }

        private class Person
        {
            public int Id { get; set; }

            public long Age { get; set; }

            public string Name { get; set; }

            public decimal Wage { get; set; }
        }
    }
}
