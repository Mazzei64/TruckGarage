#! /bin/sh

dotnet build ;

dotnet ef migrations add --project src/ first-migration ;

dotnet ef database update --project src/ first-migration