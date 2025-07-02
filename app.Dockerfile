FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY ./Cut-Roll-News/src/Cut-Roll-News.Api/*.csproj .Cut-Roll-News/src/Cut-Roll-News.Api/
COPY ./Cut-Roll-News/src/Cut-Roll-News.Infrastructure/*.csproj .Cut-Roll-News/src/Cut-Roll-News.Infrastructure/
COPY ./Cut-Roll-News/src/Cut-Roll-News.Core/*.csproj .Cut-Roll-News/src/Cut-Roll-News.Core/

COPY . .

RUN dotnet publish Cut-Roll-News/src/Cut-Roll-News.Api/Cut-Roll-News.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "Cut-Roll-News.Api.dll" ]