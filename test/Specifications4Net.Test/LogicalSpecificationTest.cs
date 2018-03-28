using LinqKit;
using Xunit;

namespace Specifications4Net.Test
{
    public class LogicalSpecificationTest
    {
        private ISpecification<int> _left = new Specification<int>(x => x > 10);

        private ISpecification<int> _right = new Specification<int>(x => x % 2 == 0);

        [Theory]
        [InlineData(1, false)]
        [InlineData(12, true)]
        [InlineData(13, false)]
        public void And(int data, bool expectedResult)
        {
            var spec = new LogicalSpecification<int>(_left, _right, PredicateOperator.And);
            Assert.Equal(expectedResult, spec.Predicate(data));
            Assert.Equal(expectedResult, spec.Expression.Invoke(data));
        }


        [Theory]
        [InlineData(1, false)]
        [InlineData(12, true)]
        [InlineData(13, true)]
        public void Or(int data, bool expectedResult)
        {
            var spec = new LogicalSpecification<int>(_left, _right, PredicateOperator.Or);
            Assert.Equal(expectedResult, spec.Predicate(data));
            Assert.Equal(expectedResult, spec.Expression.Invoke(data));
        }
    }
}
