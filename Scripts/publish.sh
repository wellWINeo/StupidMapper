#!/bin/sh

# prelude
PROJECT_NAME=StupidMapper
PROJECT_DIR=./$PROJECT_NAME

cd $PROJECT_DIR

# clean all
dotnet clean
rm -rf bin
rm -rf obj

# pack .nupkg
dotnet pack --configuration Release
NUPKG_FILE=$(find . -name "$PROJECT_NAME.**.nupkg")

# publish
dotnet nuget push $NUPKG_FILE --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json