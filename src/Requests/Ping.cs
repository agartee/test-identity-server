using CommandLine;
using MediatR;

namespace TestIdentityServer.Requests
{
    [Verb("ping", HelpText = "ping: Ping stuff.")]
    public class Ping : IRequest<string>
    {
        [Option(Required = false, HelpText = "property 1?")]
        public string? Property1 { get; set; }
    }

    public class PingHandler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong");
        }
    }
}
