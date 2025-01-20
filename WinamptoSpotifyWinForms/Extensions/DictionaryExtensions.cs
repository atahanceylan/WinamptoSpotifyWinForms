using System.Collections.Generic;
using System.Linq;

namespace WinamptoSpotifyWinForms.Extensions
{
    public static class DictionaryExtensions
    {
        public static string BuildQueryString(this Dictionary<string, string> dictionary)
        {
            return string.Join("&", dictionary.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }
    }
}
