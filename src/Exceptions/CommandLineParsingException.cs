using CommandLine;

namespace TestIdentityServer.Exceptions
{
    public class CommandLineParsingException : Exception
    {
        private const string MESSAGE = "Unable to parse command.";

        public ParserResult<object> ParserResult { get; }

        public CommandLineParsingException(ParserResult<object> parserResult)
            : base(MESSAGE)
        {
            ParserResult = parserResult;
        }
    }
}
