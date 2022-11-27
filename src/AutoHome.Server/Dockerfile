#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/AutoHome.Server/AutoHome.Server.csproj", "src/AutoHome.Server/"]
RUN dotnet restore "src/AutoHome.Server/AutoHome.Server.csproj"
COPY . .
WORKDIR "/src/src/AutoHome.Server"
RUN dotnet build "AutoHome.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoHome.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoHome.Server.dll"]