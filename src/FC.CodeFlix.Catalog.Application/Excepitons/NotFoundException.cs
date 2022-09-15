namespace FC.CodeFlix.Catalog.Application.Excepitons
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string? message) : base(message)
        {
        }
    }
}
