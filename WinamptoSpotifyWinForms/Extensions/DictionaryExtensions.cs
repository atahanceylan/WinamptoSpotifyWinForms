using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace winamptospotifyforms.Extensions
{
    public static class DictionaryExtensions
    {
        public static string BuildQueryString(this Dictionary<string, string> dictionary)
        {
            return string.Join("&", dictionary.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }
    }
}
