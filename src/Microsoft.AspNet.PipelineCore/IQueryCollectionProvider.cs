using Microsoft.AspNet.Abstractions;

namespace Microsoft.AspNet.PipelineCore
{
    /// <summary>
    /// Provides a query value collection parsed from a query string
    /// </summary>
    public interface IQueryCollectionProvider
    {
        IReadableStringCollection Query { get; }
    }
}
