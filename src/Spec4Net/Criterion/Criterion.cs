namespace Spec4Net.Criterion
{
    public interface ICriterion
    {
        ISpecification<T> ToSpecification<T>();
    }
}
