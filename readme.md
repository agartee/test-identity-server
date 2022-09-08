# Test Identity Server

This is a containerized Duende Identity Server 5 server intended to be used as a local 
substitute for an OAuth2/OpenID Connect server for end-to-end testing.

# Verifying Docker-Network Endpoints

The `/.well-known/openid-configuration` addresses can be verified by simply creating a
docker-compose file with another container that will execute a `curl` command.

Verification `docker-compose.yml`:

```yml
version: "3"

services:
  identity-server1:
    image: test-identity-server:latest
    hostname: identity-server1
    container_name: identity-server1
    ports:
      - "5000:80"

  identity-server2:
    image: test-identity-server:latest
    hostname: identity-server2
    container_name: identity-server2
    ports:
      - "5001:80"
```

Start the containers:

```bash
docker-compose -f ./docker-compose.yml up -d
```

Example `curl` command (from a second container):

```bash
apt-get update
apt-get -y install curl
curl --location --request GET 'http://identity-server1/.well-known/openid-configuration'
```
