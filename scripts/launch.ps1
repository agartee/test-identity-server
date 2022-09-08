docker container rm test-identity-server --force 2>&1 | Out-Null
docker container run --detach --name test-identity-server --publish 5000:80 `
    test-identity-server
