namespace Spec4Net.Criterion
{
    public abstract class AbstractCriterion : ICriterion
    {
        public abstract ISpecification<T> ToSpecification<T>();
    }
}
