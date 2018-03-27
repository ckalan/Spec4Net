namespace Spec4Net.Criterion
{
    public interface IQuery
    {
        IQuery Add(ICriterion expression);
        
        IQuery AddOrder((string Property, string OrderType) order);
    }
}
