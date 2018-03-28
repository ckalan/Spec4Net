using System;
using System.Linq.Expressions;
using LinqKit;
using Xunit;

namespace Specifications4Net.Test
{
    public class SpecificationExtensionsTest
    {
        private readonly Expression<Func<int, bool>> _sourceExp = t => t == 1;

        private readonly Expression<Func<int, bool>> _targetExp = t => t < 10;

        [Fact]
        public void And()
        {
            var logicalSpec = (LogicalSpecification<int>) new Specification<int>(_sourceExp).And(_targetExp);

            Assert.Equal(PredicateOperator.And, logicalSpec.Operator);
            Assert.Equal(logicalSpec.Left.Expression, _sourceExp);
            Assert.Equal(logicalSpec.Right.Expression, _targetExp);
        }

        [Fact]
        public void Or()
        {
            var logicalSpec = (LogicalSpecification<int>)new Specification<int>(_sourceExp).Or(_targetExp);

            Assert.Equal(PredicateOperator.Or, logicalSpec.Operator);
            Assert.Equal(logicalSpec.Left.Expression, _sourceExp);
            Assert.Equal(logicalSpec.Right.Expression, _targetExp);
        }

        [Fact]
        public void Not()
        {
            var notSpec = (NotSpecification<int>) new Specification<int>(_sourceExp).Not();
            Assert.True(notSpec.Predicate(2));
            Assert.False(notSpec.Predicate(1));
        }
    }
}
