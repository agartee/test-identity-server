using CommandLine;

namespace TestIdentityServer.Exceptions
{
    public class CommandLineHelpRequestedException : Exception
    {
        public ParserResult<object> ParserResult { get; }

        public CommandLineHelpRequestedException(ParserResult<object> parserResult)
        {
            ParserResult = parserResult;
        }
    }
}
