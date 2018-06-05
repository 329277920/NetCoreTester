FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 8008:80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY NetCoreTester.Docker/NetCoreTester.Docker.csproj NetCoreTester.Docker/
RUN dotnet restore NetCoreTester.Docker/NetCoreTester.Docker.csproj
COPY . .
WORKDIR /src/NetCoreTester.Docker
RUN dotnet build NetCoreTester.Docker.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish NetCoreTester.Docker.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NetCoreTester.Docker.dll"]
