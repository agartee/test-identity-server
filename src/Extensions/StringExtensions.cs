using System.Text.RegularExpressions;

namespace TestIdentityServer.Extensions
{
    public static class StringExtensions
    {
        public static string[] ParseCommandText(this string str)
        {
            return new Regex(@"[^\s""]+|""[^""\\]*(?:\\.[^""\\]*)*""")
                .Matches(str).OfType<Match>()
                .Select(m => m.Value.Trim(new[] { '"' }))
                .Select(s => s.Replace("\\\"", "\""))
                .ToArray();
        }
    }
}
