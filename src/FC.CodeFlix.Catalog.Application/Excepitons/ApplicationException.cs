namespace FC.CodeFlix.Catalog.Application.Excepitons
{
    public abstract class ApplicationException : Exception
    {
        protected ApplicationException(string? message) : base(message)
        {
        }
    }
}
