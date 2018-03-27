using System.Collections.Generic;
using System.Linq;
using LinqKit;
using Xunit;

namespace Spec4Net.Test
{
    
    public class SpecificationTest
    {
        [Theory]
        [InlineData((new [] {1,2}), true)]
        [InlineData(new int[0], false)]
        public void PredicateAndExpression_SuccessScenario(int[] data, bool expectedResult)
        {
            var spec = new Specification<IEnumerable<int>>(t => t.Any());
            Assert.Equal(expectedResult, spec.Predicate(data));
            Assert.Equal(expectedResult, spec.Expression.Invoke(data));
        }
    }
}
