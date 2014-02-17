using System;
using System.Collections.Generic;
using Microsoft.AspNet.Abstractions;

namespace Microsoft.AspNet.PipelineCore.Internal
{
    public static partial class ParsingHelpers
    {
        private static readonly char[] SemicolonAndComma = new[] { ';', ',' };

        public static IDictionary<string, string> GetCookies(HttpRequest request)
        {
            var cookies = GetItem<IDictionary<string, string>>(request, "Microsoft.Owin.Cookies#dictionary");
            if (cookies == null)
            {
                cookies = new Dictionary<string, string>(StringComparer.Ordinal);
                SetItem(request, "Microsoft.Owin.Cookies#dictionary", cookies);
            }

            string text = GetHeader(request.Headers, "Cookie");
            if (GetItem<string>(request, "Microsoft.Owin.Cookies#text") != text)
            {
                cookies.Clear();
                ParseDelimited(text, SemicolonAndComma, AddCookieCallback, cookies);
                SetItem(request, "Microsoft.Owin.Cookies#text", text);
            }
            return cookies;
        }

        public static void ParseCookies(string cookiesHeader, IDictionary<string, string> cookiesCollection)
        {
            ParseDelimited(cookiesHeader, SemicolonAndComma, AddCookieCallback, cookiesCollection);
        }

        private static readonly Action<string, string, object> AddCookieCallback = (name, value, state) =>
        {
            var dictionary = (IDictionary<string, string>)state;
            if (!dictionary.ContainsKey(name))
            {
                dictionary.Add(name, value);
            }
        };

        internal static void ParseDelimited(string text, char[] delimiters, Action<string, string, object> callback, object state)
        {
            int textLength = text.Length;
            int equalIndex = text.IndexOf('=');
            if (equalIndex == -1)
            {
                equalIndex = textLength;
            }
            int scanIndex = 0;
            while (scanIndex < textLength)
            {
                int delimiterIndex = text.IndexOfAny(delimiters, scanIndex);
                if (delimiterIndex == -1)
                {
                    delimiterIndex = textLength;
                }
                if (equalIndex < delimiterIndex)
                {
                    while (scanIndex != equalIndex && char.IsWhiteSpace(text[scanIndex]))
                    {
                        ++scanIndex;
                    }
                    string name = text.Substring(scanIndex, equalIndex - scanIndex);
                    string value = text.Substring(equalIndex + 1, delimiterIndex - equalIndex - 1);
                    callback(
                        Uri.UnescapeDataString(name.Replace('+', ' ')),
                        Uri.UnescapeDataString(value.Replace('+', ' ')),
                        state);
                    equalIndex = text.IndexOf('=', delimiterIndex);
                    if (equalIndex == -1)
                    {
                        equalIndex = textLength;
                    }
                }
                scanIndex = delimiterIndex + 1;
            }
        }
    }
}
