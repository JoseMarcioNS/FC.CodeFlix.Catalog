using System.Diagnostics.CodeAnalysis;

namespace FC.CodeFlix.Catalog.Application.Excepitons
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string? message) : base(message)
        {}
        public static void IfNull(object? @object,string message)
        {
            if(@object is null)
                throw new NotFoundException(message);
        }
    }
}
