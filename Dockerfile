FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
EXPOSE 80

COPY .publish/ .

ENTRYPOINT ["dotnet", "TestIdentityServer.dll"]
