using CommandLine;
using CommandLine.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TestIdentityServer.Exceptions;
using TestIdentityServer.Extensions;

namespace TestIdentityServer.Controllers
{
    public class CliController : ControllerBase
    {
        private readonly IMediator mediator;

        public CliController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> Index([FromBody]string commandText)
        {
            try
            {
                var request = BuildRequest(commandText);
                var response = await mediator.Send(request);

                return Ok(response);
            }
            catch(CommandLineHelpRequestedException e)
            {
                return Ok(e.ParserResult.GetHelpText());
            }
            catch (CommandLineParsingException e)
            {
                return BadRequest(e.Message);
            }
        }

        private static IBaseRequest BuildRequest(string commandText)
        {
            var commands = Assembly.GetExecutingAssembly()
                .ExportedTypes.Where(t => typeof(IBaseRequest).IsAssignableFrom(t.GetTypeInfo()))
                .ToArray();

            var parserResult = Parser.Default.ParseArguments(
                commandText.ParseCommandText(), 
                commands);

            if(parserResult.Errors.Any(e => e is HelpRequestedError))
                throw new CommandLineHelpRequestedException(parserResult);

            if (parserResult is not Parsed<object> parser
                || parser.Value is not IBaseRequest request)
            {
                throw new CommandLineParsingException(parserResult);
            }

            return request;
        }
    }
}
