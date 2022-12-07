
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG BUILDPLATFORM=linux/arm64
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 as build
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