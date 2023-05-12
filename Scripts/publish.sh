#!/bin/sh

PROJECT_DIR=./StupidMapper

dotnet clean $PROJECT_DIR
dotnet pack --configuration Release $PROJECT_DIR

NUPKG_FILE=$(cd $PROJECT_DIR && find . -name "*.nupkg" && cd ../)

dotnet nuget push $NUPKG_FILE --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json