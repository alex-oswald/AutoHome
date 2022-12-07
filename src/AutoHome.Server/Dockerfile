
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0.100 AS build
WORKDIR /source
COPY src .
RUN dotnet restore "AutoHome.Server/AutoHome.Server.csproj"
RUN dotnet build "AutoHome.Server/AutoHome.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoHome.Server/AutoHome.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoHome.Server.dll"]