using CommandLine;
using MediatR;

namespace TestIdentityServer.Requests
{
    [Verb("create-user", HelpText = "create user: Create a new user.")]
    public class CreatePerson : IRequest<string>
    {
        [Option(Required = true, HelpText = "name")]
        public string? Name { get; set; }
    }

    public class CreatePersonHandler : IRequestHandler<CreatePerson, string>
    {
        public Task<string> Handle(CreatePerson request, CancellationToken cancellationToken)
        {
            return Task.FromResult("User created!");
        }
    }
}
