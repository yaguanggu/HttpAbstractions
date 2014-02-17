using Microsoft.AspNet.Abstractions;

namespace Microsoft.AspNet.PipelineCore.Internal
{
    public static partial class ParsingHelpers
    {
        public static T GetItem<T>(HttpRequest request, string key)
        {
            object value;
            return request.HttpContext.Items.TryGetValue(key, out value) ? (T)value : default(T);
        }

        public static void SetItem<T>(HttpRequest request, string key, T value)
        {
            request.HttpContext.Items[key] = value;
        }
    }
}
