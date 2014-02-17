using System;
using System.Collections.Generic;
using Microsoft.AspNet.Abstractions;

namespace Microsoft.AspNet.PipelineCore.Internal
{
    public static partial class ParsingHelpers
    {
        private static readonly char[] AmpersandAndSemicolon = new[] { '&', ';' };

        private static readonly Action<string, string, object> AppendItemCallback = (name, value, state) =>
        {
            var dictionary = (IDictionary<string, List<String>>)state;

            List<string> existing;
            if (!dictionary.TryGetValue(name, out existing))
            {
                dictionary.Add(name, new List<string>(1) { value });
            }
            else
            {
                existing.Add(value);
            }
        };

        public static IDictionary<string, string[]> GetQuery(HttpRequest request)
        {
            var query = GetItem<IDictionary<string, string[]>>(request, "Microsoft.Owin.Query#dictionary");
            if (query == null)
            {
                query = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
                SetItem(request, "Microsoft.Owin.Query#dictionary", query);
            }

            string text = request.QueryString.Value;
            if (GetItem<string>(request, "Microsoft.Owin.Query#text") != text)
            {
                query.Clear();
                var accumulator = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
                ParseDelimited(text, AmpersandAndSemicolon, AppendItemCallback, accumulator);
                foreach (var kv in accumulator)
                {
                    query.Add(kv.Key, kv.Value.ToArray());
                }
                SetItem(request, "Microsoft.Owin.Query#text", text);
            }
            return query;
        }

        public static string GetJoinedValue(IDictionary<string, string[]> store, string key)
        {
            string[] values = GetUnmodifiedValues(store, key);
            return values == null ? null : string.Join(",", values);
        }

        public static string[] GetUnmodifiedValues(IDictionary<string, string[]> store, string key)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            string[] values;
            return store.TryGetValue(key, out values) ? values : null;
        }
    }
}
