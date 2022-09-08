#!/usr/bin/env bash

ROOT_DIR="$(cd -P "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

dotnet publish $ROOT_DIR/src/TestIdentityServer.csproj \
    --configuration Release \
    --output $ROOT_DIR/.publish

docker image build --tag test-identity-server $ROOT_DIR
