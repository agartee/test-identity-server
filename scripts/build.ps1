$rootDir = (get-item $PSScriptRoot).Parent.FullName

dotnet publish "$($rootDir)\src\TestIdentityServer.csproj" `
    --configuration Release `
    --output "$($rootDir)\.publish"

docker image build --tag test-identity-server "$($rootDir)"
