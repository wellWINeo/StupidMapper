#!/bin/sh


dotnet clean
dotnet pack --configuration Release

NUPKG_FILE=$(find . -name "*.nupkg")

dotnet nuget push $NUPKG_FILE --api-key $NUGET_API_KEY --source https://api.nuget.org/v3/index.json