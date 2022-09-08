using System.Reflection;
using MediatR;
using TestIdentityServer.Formatters;
using TestIdentityServer.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(configure: options =>
{
    options.InputFormatters.Add(new PlainTextInputFormatter());
});
builder.Services.AddIdentityServer(setupAction: options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    // Note: When using the scope-only model, no aud (audience) claim will be added to the token
    // since this concept does not apply. If you need an aud claim, you can enable the
    // EmitStaticAudienceClaim setting on the options. This will emit an aud claim in the
    // issuer_name/resources format. If you need more control of the aud claim, use API resources.
    // https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/api_scopes
    options.EmitStaticAudienceClaim = true;
})
  .AddClientStore<MyClientStore>()
  .AddResourceStore<MyResourceStore>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseHttpsRedirection();
app.UseIdentityServer();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
