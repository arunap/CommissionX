namespace CommissionX.Core.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() { }
        public ItemNotFoundException(string message) : base(message) { }
        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public ItemNotFoundException(string itemName, Guid id) : base($"{itemName} with ID {id} was not found.") { }
    }
}