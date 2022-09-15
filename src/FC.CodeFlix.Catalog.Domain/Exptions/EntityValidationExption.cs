namespace FC.CodeFlix.Catalog.Domain.Exeptions
{
    public class EntityValidationExption : Exception
    {
        public EntityValidationExption(string? message) : base(message)
        {
        }
    }
}
