FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ClientConsoleService/ClientConsoleService.csproj", "ClientConsoleService/"]
RUN dotnet restore "ClientConsoleService/ClientConsoleService.csproj"
COPY . .
WORKDIR "/src/ClientConsoleService"
RUN dotnet build "ClientConsoleService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientConsoleService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientConsoleService.dll"]
