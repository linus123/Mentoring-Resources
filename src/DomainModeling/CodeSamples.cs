public class Customer
{
    public Customer(
        Guid id,
        string firstName,
        string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public Id { get; private set; }
    public FirstName { get; private set; }
    public LastName { get; private set; }

    public string GetLastFirstName()
    {
        return string.Format("{0}, {1}", LastName, FirstName);
    }
}

