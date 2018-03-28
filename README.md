# SpecificationsForNet
Specification Pattern Implementation for .Net Core using LinqKit with additional NHibernate like dynamic Criteria features

## Specifications

```c#
public class User
{
    public int Id { get; set; }

    public int Age { get; set; }

    public bool Deleted { get; set; }
}

/// <summary>
/// Example specification
/// </summary>
public class FindUsersOverAgeSpec : Specification<User>
{
    public FindUsersOverAgeSpec(int minAge) : base(p => p.Age > minAge)
    {
    }
}

/// <summary>
/// Some method that filters the IQueryable with specifications and applies dynamic order
/// </summary>
public IEnumerable<User> GetUsers()
{

    ISpecification<User> spec = new FindUsersOverAgeSpec(18);

    // Create an inline specification
    var userNotDeletedSpec = new Specification<User>(u => !u.Deleted);

    // Combine specifications. You can also use Or() and Not()
    spec = spec.And(userNotDeletedSpec);

    // Get some queryable object possibly from your EF DbSet
    IQueryable<User> queryable = GetUserQueryableOrDbSet();

    // C# 7 tuple array. Each sort param is a string pair in ( PropertyName, Order ) format
    // Empty string or null for the Order defaults to Ascending order
    var sortParams = new[] {("Id", "Asc"), ("Age", "Desc")};
    return queryable.Filter(spec).OrderBy(sortParams).ToList();

}
    
```

**IQueryable.Filter()** method uses **Specification<T>.Expression** property which returns **Expression<Func<T,bool>**. 

You can also use the **Specification<T>.Predicate** property to run the specification on an object instead of an IQueryable.
    
```c#
bool isOver18 = new FindUsersOverAgeSpec(18).Predicate(user);
```




