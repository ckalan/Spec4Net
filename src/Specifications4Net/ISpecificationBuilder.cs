namespace Specifications4Net
{
    public interface ISpecificationBuilder<T>
    {
        ISpecificationBuilder<T> Property(string property,string filterOperator,object value);

    }
}
