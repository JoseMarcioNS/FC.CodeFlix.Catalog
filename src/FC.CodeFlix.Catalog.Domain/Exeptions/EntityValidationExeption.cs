namespace FC.CodeFlix.Catalog.Domain.Exeptions
{
    public class EntityValidationExeption : Exception
    {
        public EntityValidationExeption(string? message) : base(message)
        {
        }
    }
}
