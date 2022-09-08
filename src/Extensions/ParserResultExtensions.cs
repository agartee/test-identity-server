using CommandLine;
using CommandLine.Text;

namespace TestIdentityServer.Extensions
{
    public static class ParserResultExtensions
    {
        public static string GetHelpText(this ParserResult<object> parserResult)
        {
            // work-around for CommandLineParser bug.
            var verbAttr = Attribute.GetCustomAttribute(parserResult.TypeInfo.Current,
                    typeof(VerbAttribute)) as VerbAttribute;

            return HelpText.AutoBuild(parserResult, h =>
            {
                if (verbAttr != null)
                    h.AddPreOptionsText(verbAttr.HelpText);

                h.AdditionalNewLineAfterOption = false;
                return h;
            }, e => e);
        }
    }
}
