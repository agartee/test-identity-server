#!/usr/bin/env bash

ROOT_DIR="$(cd -P "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

GRAY="\033[0;30m"
RED="\033[0;31m"
GREEN="\033[0;32m"
YELLOW="\033[0;33m"
BLUE="\033[0;34m"
MAGENTA="\033[0;35m"
CYAN="\033[0;36m"
NC="\033[0m" # No Color

# **************************************************************************************
# Check Dotnet Installation
# **************************************************************************************
MIN_DOTNET_VERSION="6.0.0"
CURRENT_DOTNET_VERSION=$(dotnet --version 2> /dev/null)

if [ -z "$CURRENT_DOTNET_VERSION" ]
    then echo -e "${RED}.NET installation not found" \
        "(minimum: ${MIN_DOTNET_VERSION}).${NC}"
    exit 1
fi

CURRENT_DOTNET_VERSION="${CURRENT_DOTNET_VERSION/.NET /""}"

if $(dpkg --compare-versions "${CURRENT_DOTNET_VERSION}" "lt" "${MIN_DOTNET_VERSION}")
    then echo -e "${RED}Current .NET version not supported" \
        "(found: ${CURRENT_DOTNET_VERSION}; " \
        "minimum: ${MIN_DOTNET_VERSION}).${NC}"
    exit 1
fi

echo -e "${GREEN}.NET installation found: ${CURRENT_DOTNET_VERSION}" \
    "(minimum: ${MIN_DOTNET_VERSION}).${NC}" 

# **************************************************************************************
# Check Docker Installation
# **************************************************************************************
CURRENT_DOCKER_VERSION=$(docker --version 2> /dev/null)
if [ -z "$CURRENT_DOCKER_VERSION" ]
    then echo -e "${RED}Docker installation not found.${NC}"
    exit 1
fi

echo -e "${GREEN}Docker installation found: ${CURRENT_DOCKER_VERSION}.${NC}"
