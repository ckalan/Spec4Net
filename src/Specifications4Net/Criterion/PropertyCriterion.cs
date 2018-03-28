namespace Specifications4Net.Criterion
{
    public class PropertyCriterion : ICriterion
    {
        public string Property { get; }

        public object Value { get;  }

        public ISpecification<T> ToSpecification<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
