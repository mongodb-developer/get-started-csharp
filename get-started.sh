#!/bin/bash
MONGODB_URI=${1}
if [ -z ${MONGODB_URI} ]
then
    read -p "MONGODB URI (Required): " MONGODB_URI
fi 

DRIVER_VERSION=${2:-2.11.5}
echo "Executing ... "
docker run --rm -e MONGODB_URI=${MONGODB_URI} \
    -v "$(pwd)":/workspace \
    -w /workspace/dotnet start-dotnet \
    "sed -i 's/\"MongoDB.Driver\" Version=\"[x0-9]\+\.[x0-9]\+\.[x0-9]\+\"/\"MongoDB.Driver\" Version=\"${DRIVER_VERSION}\"/g' /workspace/dotnet/getstarted.csproj; \
    dotnet run"
